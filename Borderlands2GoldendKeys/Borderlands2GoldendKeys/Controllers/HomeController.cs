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

        public ActionResult Index(string id)
        {
            // Get datas
            var homeViewModel = new HomeViewModel();

            homeViewModel.ClapTrapQuote = _documentSession.Query<ClapTrapQuote>().Customize(q => q.RandomOrdering()).First();

            if (string.Equals("showall", id, System.StringComparison.OrdinalIgnoreCase))
            {
                homeViewModel.ShiftCodes = GetShiftCodesBaseQuery().ToList();
                homeViewModel.DisableShallAllButton = true;
            }
            else
            {
                homeViewModel.ShiftCodes = GetShiftCodesBaseQuery().Take(5).ToList();
            }

            return View(homeViewModel);
        }

        private IQueryable<ShiftCode> GetShiftCodesBaseQuery()
        {
            return _documentSession.Query<ShiftCode>().OrderByDescending(s => s.CreationDate);
        }

        public ActionResult GetRemainingShiftCodes()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_ShiftCodeRowPartial", GetShiftCodesBaseQuery().Skip(5).ToList());
            }
            else
            {
                return Content("Nothing here");
            }
        }
    }
}