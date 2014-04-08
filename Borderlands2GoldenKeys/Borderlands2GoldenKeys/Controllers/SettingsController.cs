using Borderlands2GoldenKeys.Helpers;
using Borderlands2GoldenKeys.Models;
using PoliteCaptcha;
using Raven.Abstractions.Data;
using Raven.Client;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Borderlands2GoldenKeys.Controllers
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

            return View(GetSettingsViewModel());
        }

        private SettingsViewModel GetSettingsViewModel(Settings settings = null)
        {
            var anyDocs = _documentSession.Query<ShiftCode>().Any();
            var viewModel = new SettingsViewModel();
            if (settings == null)
            {
                viewModel.Settings = _documentSession.Load<Settings>(Settings.UniqueId);
            }
            else
            {
                viewModel.Settings = settings;
            }
            viewModel.DisableFillDatabaseButton = viewModel.Settings == null || viewModel.Settings.Twitter == null || !viewModel.Settings.Twitter.IsComplete || anyDocs;
            viewModel.DisableLaunchUpdateProcessButton = !viewModel.DisableFillDatabaseButton || _updateProcess.IsRunning || viewModel.Settings == null || viewModel.Settings.Twitter == null || !viewModel.Settings.Twitter.IsComplete;
            viewModel.DisableDeleteAllButton = !anyDocs;
            viewModel.UpdateRunning = _updateProcess.IsRunning;
            return viewModel;
        }

        //
        // POST: /Settings/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Settings settings)
        {
            if (ModelState.IsValid)
            {
                _documentSession.Store(settings);
                _documentSession.SaveChanges();
            }
            return View(GetSettingsViewModel(settings));
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
        [ValidateAntiForgeryToken]
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

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
        [ValidateAntiForgeryToken]
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
                    message = new ResultMessage
                    {
                        Success = false,
                        Message = "Update process already launched. That is not supposed to be possible."
                    };
                }
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

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
        [ValidateAntiForgeryToken]
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

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
