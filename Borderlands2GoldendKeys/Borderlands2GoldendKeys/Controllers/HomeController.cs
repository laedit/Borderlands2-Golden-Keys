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
            homeViewModel.GoldenKeys.Add(new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC });
            homeViewModel.GoldenKeys.Add(new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX });
            homeViewModel.GoldenKeys.Add(new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3 });

            return View(homeViewModel);
        }
    }
}