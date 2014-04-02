using Borderlands2GoldenKeys.Helpers;
using Borderlands2GoldenKeys.Models;
using PoliteCaptcha;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Syndication;
using System.Web.Mvc;

namespace Borderlands2GoldenKeys.Controllers
{
    public class HomeController : Controller
    {
        private const int NumberToShow = 5;

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
            return View(GetViewModel());
        }

        public ActionResult ShowAll()
        {
            return View("Index", GetViewModel(true));
        }

        private HomeViewModel GetViewModel(bool showAll = false)
        {
            var homeViewModel = new HomeViewModel();

            homeViewModel.ClapTrapQuote = _documentSession.Query<ClapTrapQuote>().Customize(q => q.RandomOrdering()).First();
            var settings = _documentSession.Load<Settings>(Settings.UniqueId);
            homeViewModel.EnableMail = settings != null && settings.Mail != null && settings.Mail.IsComplete;
            homeViewModel.Rows.StartIndex = 1;

            if (showAll)
            {
                homeViewModel.Rows.ShiftCodes = GetShiftCodesBaseQuery().ToList();
                homeViewModel.DisableShallAllButton = true;
            }
            else
            {
                homeViewModel.Rows.ShiftCodes = GetShiftCodesBaseQuery().Take(NumberToShow).ToList();
            }
            return homeViewModel;
        }

        private IQueryable<ShiftCode> GetShiftCodesBaseQuery()
        {
            return _documentSession.Query<ShiftCode>().OrderByDescending(s => s.CreationDate);
        }

        public ActionResult GetRemainingShiftCodes()
        {
            if (Request.IsAjaxRequest())
            {
                var rows = new RowsViewModel();
                rows.ShiftCodes = GetShiftCodesBaseQuery().Skip(NumberToShow).ToList();
                rows.StartIndex = NumberToShow + 1;
                return PartialView("_ShiftCodeRowPartial", rows);
            }
            else
            {
                return Content("Nothing here");
            }
        }

        [HttpPost, ValidateSpamPrevention]
        public JsonResult SendMail(Mail mail)
        {
            ResultMessage resultMessage = new ResultMessage { Success = false };
            if (ModelState.IsValid)
            {
                // Verification of the mail address
                MailAddress mailAddress = new MailAddress(mail.MailFrom);

                var mailSettings = _documentSession.Load<Settings>(Settings.UniqueId).Mail;
                using (var client = new SmtpClient(mailSettings.SmtpHost, mailSettings.SmtpPort))
                {
                    string mailId = mailSettings.DestinationMail;
                    string mailPassword = mailSettings.Password;

                    client.Credentials = new NetworkCredential(mailId, mailPassword);
                    client.EnableSsl = true;

                    client.Send(
                        @from: mailId,
                        recipients: mailId,
                        subject: "A word from Borderlands 2: Golden Keys",
                        body: string.Format("From: {0}.\r\n\r\n{1}", mailAddress.Address, mail.Message));
                }
                resultMessage.Success = true;
            }
            else
            {
                if (ModelState.ContainsKey("PoliteCaptcha"))
                {
                    resultMessage.ErrorSource = "PoliteCaptcha";
                }
            }

            return Json(resultMessage);
        }

        const string RssItemContentTemplate = @"SHiFT Code: {0}<br />Expiration date: {1}<br /><a href=""http://twitter.com/GearboxSoftware/status/{2}"">Source</a>";

        [HttpGet]
        public ActionResult Rss()
        {
            var shiftCodesQuery = _documentSession.Query<ShiftCode>().OrderByDescending(s => s.CreationDate);

            DateTime modifiedSince;
            if (DateTime.TryParse(Request.Headers["If-Modified-Since"], out modifiedSince))
            {
                modifiedSince = modifiedSince.AddMinutes(-5); // Just in case
                shiftCodesQuery = (IOrderedQueryable<ShiftCode>)shiftCodesQuery.Where(s => s.CreationDate >= modifiedSince);
            }

            var shiftCodes = shiftCodesQuery.Take(10).ToList();

            if (shiftCodes.Count == 0)
            {
                return new HttpStatusCodeResult(304, "Not Modified");
            }

            Response.AddHeader("Last-Modified", shiftCodes[0].CreationDate.ToUniversalTime().ToString("R"));

            List<SyndicationItem> rssItems = new List<SyndicationItem>();
            SyndicationFeed feed =
                new SyndicationFeed("Borderlands 2: Golden Keys",
                                    "List af all ShiftCodes getting golden keys provided by Gearbox on Twitter",
                                    new Uri(Request.Url.ToString()),
                                    Url.Action("Rss", "Home", null, "http"),
                                    DateTime.Now);

            foreach (var shiftCode in shiftCodes)
            {
                var rssItem = new SyndicationItem(shiftCode.Platform.DisplayName(),
                    string.Format(RssItemContentTemplate, shiftCode.Code, shiftCode.ExpirationDate.Value.ToString("d MMM yyyy", new System.Globalization.CultureInfo("en-us")), shiftCode.SourceStatusId),
                    new Uri(Url.Action("Index", "Home", null, "http")),
                    shiftCode.Code,
                    shiftCode.CreationDate);
                rssItems.Add(rssItem);
            }

            feed.Items = rssItems;

            return new RssResult(feed);
        }
    }
}