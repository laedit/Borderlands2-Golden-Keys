using Borderlands2GoldendKeys.Models;
using Raven.Client;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System;

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
            // if eTag cache needed
            // http://stackoverflow.com/questions/937668/how-do-i-support-etags-in-asp-net-mvc

            // Get datas
            var homeViewModel = new HomeViewModel();

            homeViewModel.ClapTrapQuote = _documentSession.Query<ClapTrapQuote>().Customize(q => q.RandomOrdering()).First();

            // TODO get golden keys
            homeViewModel.GoldenKeys = GoldenKey.GetDummyData().Take(5).ToList();
            
            return View(homeViewModel);
        }

        public ActionResult GetRemainingGoldenKeys()
        {
            return PartialView("_GoldenKeyRowPartial", GoldenKey.GetDummyData().Skip(5).ToList());
        }
    }
}