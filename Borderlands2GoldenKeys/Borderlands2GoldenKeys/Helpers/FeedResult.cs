using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Borderlands2GoldenKeys.Helpers
{
    public class RssResult : FileResult
    {
        private readonly SyndicationFeed _feed;

        public RssResult(SyndicationFeed feed)
            : base("application/rss+xml")
        {
            this._feed = feed;
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            using (XmlWriter writer = XmlWriter.Create(response.OutputStream))
            {
                this._feed.GetRss20Formatter().WriteTo(writer);
            }
        }
    }
}