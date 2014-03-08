using Borderlands2GoldendKeys.Helpers;
using Borderlands2GoldendKeys.Models;
using Raven.Client;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Borderlands2GoldendKeys.Controllers
{
    [Authorize(Roles=RoleNames.Admin)]
    public class SettingsController : Controller
    {
        /// <summary>
        /// RavenDB document session
        /// </summary>
        private IDocumentSession _documentSession;

        public SettingsController(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        //
        // GET: /Settings/
        public ActionResult Index()
        {
            return View(_documentSession.Query<TwitterSettings>().FirstOrDefault());
        }

        //
        // POST: /Settings/
        [HttpPost]
        public ActionResult Index(TwitterSettings twitterSettings)
        {
            try
            {
                _documentSession.Store(twitterSettings);
                _documentSession.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: Settings/Test
        public async Task<ActionResult> Test()
        {
            var twitterSettings = _documentSession.Query<TwitterSettings>().FirstOrDefault();

            // TODO use ShiftCodeRecuperator
            // TODO get real lastId
            var shiftCodeRecuperator = new ShiftCodeRecuperator(twitterSettings.APIKey, twitterSettings.APISecret);
            var tweets = await shiftCodeRecuperator.GetBaseRawTweetsAsync();
            //var tweets = await shiftCodeRecuperator.GetUpdateRawTweetsAsync(440879761649180673);
            var shiftCodes = ShiftCodeRecuperator.ParseTweets(tweets);

            return View(shiftCodes);
        }

        // TODO start parse base tweets si base vide + lance cache sinon juste lance cache
        // => bouton disponible si cache pas déjà en cours
    }
}
