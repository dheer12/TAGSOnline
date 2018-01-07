using System.Web;
using System.Web.Mvc;

namespace TAGS_BARCODE_WEBAPP_V4
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
