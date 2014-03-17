using Borderlands2GoldendKeys.Models;
using Raven.Client;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            homeViewModel.EnableMail = _documentSession.Load<Settings>(Settings.UniqueId).Mail.IsComplete;

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

        [HttpPost]
        public JsonResult SendMail(Mail mail)
        {
            ResultMessage resultMessage = new ResultMessage { Success = false };
            if(ModelState.IsValid)
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

            return Json(resultMessage);
        }
    }
}