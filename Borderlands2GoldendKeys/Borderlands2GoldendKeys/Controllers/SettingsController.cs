using Borderlands2GoldendKeys.Models;
using Raven.Client;
using System.Linq;
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
    }
}
