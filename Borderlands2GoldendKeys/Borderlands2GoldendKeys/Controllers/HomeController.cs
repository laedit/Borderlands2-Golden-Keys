using Borderlands2GoldendKeys.Models;
using Raven.Client;
using System.Linq;
using System.Web.Mvc;

namespace Borderlands2GoldendKeys.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// RavenDB document session
        /// </summary>
        private IDocumentSession _documentSession;

        public HomeController(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public ActionResult Index()
        {
            // Get datas
            var homeViewModel = new HomeViewModel();

            homeViewModel.ClapTrapQuote = _documentSession.Query<ClapTrapQuote>().Customize(q => q.RandomOrdering()).First();

            // TODO get golden keys
            homeViewModel.GoldenKeys = GoldenKey.GetDummyData().Take(5).ToList();

            return View(homeViewModel);
        }

        public ActionResult GetRemainingGoldenKeys()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_GoldenKeyRowPartial", GoldenKey.GetDummyData().Skip(5).ToList());
            }
            else
            {
                return Content("Nothing here");
            }
        }
    }
}