﻿using Borderlands2GoldendKeys.Models;
using Raven.Client;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;

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
            // if eTag cache neede
            // http://stackoverflow.com/questions/937668/how-do-i-support-etags-in-asp-net-mvc

            // Store some ClapTrap's quotes if necessary
            if (!_documentSession.Query<ClapTrapQuote>().Any())
            {
                ClapTrapQuote.GetBaseQuotes().ForEach(q => _documentSession.Store(q));
                _documentSession.SaveChanges();
            }

            // Get datas
            var homeViewModel = new HomeViewModel();

            homeViewModel.ClapTrapQuote = _documentSession.Query<ClapTrapQuote>().Customize(q => q.RandomOrdering()).First();

            return View(homeViewModel);
        }
    }
}