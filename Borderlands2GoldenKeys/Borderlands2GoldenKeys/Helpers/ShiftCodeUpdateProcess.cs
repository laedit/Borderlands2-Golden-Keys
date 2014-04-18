using Borderlands2GoldenKeys.Models;
using Raven.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Borderlands2GoldenKeys.Helpers
{
    public class ShiftCodeUpdateProcess
    {
        private const string CacheKey = "ShiftCodeUpdateProcess";

        private IDocumentSession _documentSession;

        public ShiftCodeUpdateProcess(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public bool IsRunning
        {
            get { return HttpContext.Current.Cache[CacheKey] != null; }
        }

        public bool Start()
        {
            if (HttpContext.Current.Cache[CacheKey] != null)
            {
                return false;
            }

            HttpContext.Current.Cache.Add(CacheKey, "Start", null,
                DateTime.MaxValue,
#if DEBUG
 TimeSpan.FromMinutes(1),
#else
                TimeSpan.FromMinutes(30), 
#endif
 CacheItemPriority.Normal,
                new CacheItemRemovedCallback(CacheItemRemovedCallback));

            return true;
        }

        public void Stop()
        {
            if (HttpContext.Current.Cache[CacheKey] != null)
            {
                HttpContext.Current.Cache.Remove(CacheKey);
            }
        }

        private async Task UpdateShiftCodesAsync()
        {
            Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("UpdateShiftCodesAsync")));
            var settings = _documentSession.Load<Settings>(Settings.UniqueId);

            var shiftCodeRecuperator = new ShiftCodeRecuperator(settings.Twitter.APIKey, settings.Twitter.APISecret);
            var lastId = _documentSession.Advanced.LuceneQuery<ShiftCode>().Select(s => s.SourceStatusId).Max();

            Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("Last id: " + lastId)));
            var statuses = await shiftCodeRecuperator.GetUpdateRawTweetsAsync(lastId);
            Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("Statuses count: " + statuses.Count)));
            var shiftCodes = ShiftCodeRecuperator.ParseTweets(statuses);
            Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("Shift codes count: " + shiftCodes.Count)));
            if (shiftCodes != null && shiftCodes.Count > 0)
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("Add shift codes")));
                shiftCodes.ForEach(s => _documentSession.Store(s));
                _documentSession.SaveChanges();
            }
        }

        public async void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            await UpdateShiftCodesAsync();

            HttpClient httpClient = new HttpClient();

            Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("RestardUpdateProcess")));
            await httpClient.GetStringAsync(string.Format("{0}/Settings/RestardUpdateProcess", MvcApplication.BaseUrl));
            // TODO secure call?
        }
    }
}