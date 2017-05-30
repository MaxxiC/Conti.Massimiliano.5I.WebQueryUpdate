using System.Web;
using System.Web.Mvc;

namespace Conti.Massimiliano._5I.WebQueryUpdate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
