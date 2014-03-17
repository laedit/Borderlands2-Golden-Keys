using Borderlands2GoldendKeys.Helpers;
using Borderlands2GoldendKeys.Models;
using Raven.Abstractions.Data;
using Raven.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Borderlands2GoldendKeys.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class SettingsController : Controller
    {
        /// <summary>
        /// RavenDB document session
        /// </summary>
        private IDocumentSession _documentSession;
        private IDocumentStore _documentStore;
        private ShiftCodeUpdateProcess _updateProcess;

        public SettingsController(IDocumentSession documentSession, IDocumentStore documentStore, ShiftCodeUpdateProcess updateProcess)
        {
            _documentSession = documentSession;
            _documentStore = documentStore;
            _updateProcess = updateProcess;
        }

        //
        // GET: /Settings/
        public ActionResult Index()
        {
            var anyDocs = _documentSession.Query<ShiftCode>().Any();
            var viewModel = new SettingsViewModel();
            viewModel.Settings = _documentSession.Load<Settings>(Settings.UniqueId);
            viewModel.DisableFillDatabaseButton = viewModel.Settings == null || viewModel.Settings.Twitter == null || !viewModel.Settings.Twitter.IsComplete || anyDocs;
            viewModel.DisableLaunchUpdateProcessButton = !viewModel.DisableFillDatabaseButton || _updateProcess.IsRunning || viewModel.Settings == null || viewModel.Settings.Twitter == null || !viewModel.Settings.Twitter.IsComplete;
            viewModel.DisableDeleteAllButton = !anyDocs;
            viewModel.UpdateRunning = _updateProcess.IsRunning;
            return View(viewModel);
        }

        //
        // POST: /Settings/
        [HttpPost]
        public ActionResult Index(Settings settings)
        {
            _documentSession.Store(settings);
            _documentSession.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: Settings/Test
        public async Task<ActionResult> Test()
        {
            var twitterSettings = _documentSession.Query<Settings>().FirstOrDefault();

            var shiftCodeRecuperator = new ShiftCodeRecuperator(twitterSettings.Twitter.APIKey, twitterSettings.Twitter.APISecret);
            var tweets = await shiftCodeRecuperator.GetBaseRawTweetsAsync();
            tweets = await shiftCodeRecuperator.GetUpdateRawTweetsAsync(tweets.Max(t => t.StatusID));
            var shiftCodes = ShiftCodeRecuperator.ParseTweets(tweets);

            return View(shiftCodes);
        }

        //
        // POST: Settings/GetBaseRawTweets
        [HttpPost]
        public async Task<JsonResult> GetBaseRawTweets()
        {
            ResultMessage message = null;
            try
            {
                var twitterSettings = _documentSession.Query<Settings>().FirstOrDefault();

                var shiftCodeRecuperator = new ShiftCodeRecuperator(twitterSettings.Twitter.APIKey, twitterSettings.Twitter.APISecret);
                var tweets = await shiftCodeRecuperator.GetBaseRawTweetsAsync();
                var shiftCodes = ShiftCodeRecuperator.ParseTweets(tweets);

                shiftCodes.ForEach(s => _documentSession.Store(s));
                _documentSession.SaveChanges();
                message = new ResultMessage
                {
                    Success = true,
                    Message = string.Format("{0} shift codes in database.", shiftCodes.Count)
                };
            }
            catch (Exception ex)
            {
                // TODO store in ELMAH
                message = new ResultMessage
                {
                    Success = false,
                    Message = string.Format("Exception: {0}.", ex.Message)
                };
            }
            return Json(message, JsonRequestBehavior.DenyGet);
        }

        //
        // POST: Settings/LaunchUpdateProcess
        [HttpPost]
        public JsonResult LaunchUpdateProcess()
        {
            ResultMessage message = null;

            try
            {
                if (_updateProcess.Start())
                {
                    message = new ResultMessage
                    {
                        Success = true,
                        Message = "Update process launched"
                    };
                }
                else
                {
                    // TODO store in ELMAH?
                    message = new ResultMessage
                    {
                        Success = false,
                        Message = "Update process already launched"
                    };
                }
            }
            catch (Exception ex)
            {
                // TODO store in ELMAH
                message = new ResultMessage
                {
                    Success = false,
                    Message = string.Format("Exception: {0}.", ex.Message)
                };
            }
            return Json(message, JsonRequestBehavior.DenyGet);
        }

        //
        // POST: Settings/DeleteAll
        [HttpPost]
        public JsonResult DeleteAll()
        {
            ResultMessage message = null;

            try
            {
                _updateProcess.Stop();

                _documentStore.DatabaseCommands.DeleteByIndex("ShiftCodesIndex", new IndexQuery(), true);
                message = new ResultMessage
                {
                    Success = true,
                    Message = string.Format("All shifts codes have been deleted.")
                };
            }
            catch (Exception ex)
            {
                // TODO store in ELMAH
                message = new ResultMessage
                {
                    Success = false,
                    Message = string.Format("Exception: {0}.", ex.Message)
                };
            }
            return Json(message, JsonRequestBehavior.DenyGet);
        }

        //
        // GET: Settings/RestardUpdateProcess
        [AllowAnonymous]
        public ActionResult RestardUpdateProcess()
        {
            return Content("Ok");
        }

        // TODO data import / export? => not a priority, tweets are perishable datas and there are all loaded when filling database
    }
}
