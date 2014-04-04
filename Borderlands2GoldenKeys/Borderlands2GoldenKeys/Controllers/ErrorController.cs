using Borderlands2GoldenKeys.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Borderlands2GoldenKeys.Controllers
{
    public class ErrorController : AsyncController
    {
        //
        // GET: /Error/
        public async Task<ActionResult> IndexAsync()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View("Error", await GetErrorInfoWithImageAsync());
        }

        //
        // GET: /Error/NotFound
        public async Task<ActionResult> NotFoundAsync(string aspxerrorpath)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            var errorInfo = await GetErrorInfoWithImageAsync();
            errorInfo.Is404 = true;
            errorInfo.ErrorPath = aspxerrorpath;

            return View("Error", errorInfo);
        }

        private async Task<ErrorInfo> GetErrorInfoWithImageAsync()
        {
            var errorInfo = new ErrorInfo();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "borderlands2goldenkeys.laedit.net/Error/NotFound");
            var response = await httpClient.GetAsync("http://backend.deviantart.com/rss.xml?q=gallery:jessemayberry/24147815&type=deviation", HttpCompletionOption.ResponseHeadersRead);

            var feed = SyndicationFeed.Load(XmlReader.Create(await response.Content.ReadAsStreamAsync()));

            var items = feed.Items.ToList();

            var item = items[new Random().Next(items.Count)];

            if (item.Links.Count > 0)
            {
                errorInfo.ImageLink = item.Links[0].Uri.AbsoluteUri;
            }

            var contentElementExtensions = item.ElementExtensions.Where(p => p.OuterName == "content");
            if (contentElementExtensions.Any())
            {
                errorInfo.ImagePath = contentElementExtensions.First().GetObject<XElement>().Attribute("url").Value;
                if(string.IsNullOrEmpty(errorInfo.ImagePath))
                {
                    return new ErrorInfo();
                }
            }

            var creditElementExtensions = item.ElementExtensions.Where(p => p.OuterName == "credit");
            if (creditElementExtensions.Any())
            {
                errorInfo.ImageAuthorName = creditElementExtensions.First().GetObject<XElement>().Value;
            }

            var copyrightElementExtensions = item.ElementExtensions.Where(p => p.OuterName == "copyright");
            if (copyrightElementExtensions.Any())
            {
                errorInfo.ImageAuthorLink = copyrightElementExtensions.First().GetObject<XElement>().Attribute("url").Value;
            }

            return errorInfo;
        }

    }
}