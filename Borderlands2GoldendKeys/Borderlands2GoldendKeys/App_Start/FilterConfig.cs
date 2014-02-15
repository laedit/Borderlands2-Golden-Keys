using System.Web;
using System.Web.Mvc;

namespace Borderlands2GoldendKeys
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
