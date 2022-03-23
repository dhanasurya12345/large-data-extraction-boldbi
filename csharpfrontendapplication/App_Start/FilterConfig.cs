using System.Web;
using System.Web.Mvc;

namespace Syncfusion.Server.EmbedApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
