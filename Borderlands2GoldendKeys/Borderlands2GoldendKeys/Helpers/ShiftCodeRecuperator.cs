using Borderlands2GoldendKeys.Models;
using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Borderlands2GoldendKeys.Helpers
{
    public class ShiftCodeRecuperator
    {
        private static Regex _shiftCodeRegex = new Regex(@"(\w{5}-\w{5}-\w{5}-\w{5}-\w{5})", RegexOptions.Compiled);
        private static Regex _playstationRegex = new Regex(@"(?i)playstation|PS3", RegexOptions.Compiled);
        private static Regex _xboxRegex = new Regex(@"(?i)xbox", RegexOptions.Compiled);
        private static Regex _computerRegex = new Regex(@"(?i)pc[ ]{0,1}/[ ]{0,1}mac", RegexOptions.Compiled);
        private static Regex _expirationDateRegex = new Regex(@"\[.*?(\d){1,2}/(\d){1,2}\]", RegexOptions.Compiled);

        private string _apiKey;
        private string _apiSecret;

        public ShiftCodeRecuperator(string apiKey, string apiSecret)
        {
            _apiKey = apiKey;
            _apiSecret = apiSecret;
        }

        private async Task<TwitterContext> InitializeTwitterContextAsync()
        {
            var twitterContext = new TwitterContext(new ApplicationOnlyAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = _apiKey,
                    ConsumerSecret = _apiSecret,
                },
                AccessType = AuthAccessType.Read
            });

            await twitterContext.Authorizer.AuthorizeAsync();

            return twitterContext;
        }

        public async Task<List<Status>> GetBaseRawTweetsAsync()
        {
            var twitterContext = await InitializeTwitterContextAsync();

            var searchResponse = await (from search in twitterContext.Status
                                        where search.Type == StatusType.User &&
                                              search.ScreenName == "GearboxSoftware" &&
                                              search.Count == 3200
                                        select search)
                                        .ToListAsync();

            if (searchResponse != null && searchResponse.Count > 0)
            {
                searchResponse = searchResponse.Where(status => status.Text.Contains("Golden") && status.Text.Contains("Borderlands")).ToList();

                return searchResponse;
            }

            return null;
        }

        public async Task<List<Status>> GetUpdateRawTweetsAsync(ulong lastId)
        {
            var twitterContext = await InitializeTwitterContextAsync();

            var searchResponse = await (from search in twitterContext.Search
                                        where search.Type == SearchType.Search &&
                                              search.SinceID == lastId &&
                                              search.IncludeEntities == false &&
                                              search.Query == "from:GearboxSoftware Golden Borderlands"
                                        select search)
                                        .SingleOrDefaultAsync();

            if (searchResponse != null && searchResponse.Statuses != null)
            {
                return searchResponse.Statuses;
            }

            return null;
        }

        public static List<ShiftCode> ParseTweets(List<Status> statuses)
        {
            List<ShiftCode> shiftCodes = new List<ShiftCode>();

            foreach (var status in statuses)
            {

                var shiftCodeMatch = _shiftCodeRegex.Match(status.Text);
                if (shiftCodeMatch.Success)
                {
                    var shiftCode = new ShiftCode();
                    shiftCode.Code = shiftCodeMatch.Groups[1].Value;

                    if (_computerRegex.IsMatch(status.Text))
                    {
                        shiftCode.Platform = Platform.PC_MAC;
                    }
                    else if (_xboxRegex.IsMatch(status.Text))
                    {
                        shiftCode.Platform = Platform.XBOX;
                    }
                    else if (_playstationRegex.IsMatch(status.Text))
                    {
                        shiftCode.Platform = Platform.PS3;
                    }
                    else
                    {
                        // TODO error reporting
                        // => new Exception(...).Report() => méthode d'extension utilisant ELMAH
                    }

                    var rawExpirationDate = _expirationDateRegex.Match(status.Text);
                    if (rawExpirationDate.Success)
                    {
                        shiftCode.ExpirationDate = new DateTime(status.CreatedAt.Year, GetIntFromCaptureCollection(rawExpirationDate.Groups[1].Captures), GetIntFromCaptureCollection(rawExpirationDate.Groups[2].Captures));
                    }

                    shiftCode.CreationDate = status.CreatedAt;

                    shiftCode.SourceStatusId = status.StatusID;

                    shiftCodes.Add(shiftCode);
                }
            }
            return shiftCodes;
        }

        private static int GetIntFromCaptureCollection(CaptureCollection captureCollection)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Capture capture in captureCollection)
            {
                sb.Append(capture.Value);
            }
            return int.Parse(sb.ToString());
        }
    }
}