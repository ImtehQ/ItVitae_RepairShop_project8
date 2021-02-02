using System.Web;
using System.Web.Mvc;

namespace ItVitae_RepairShop_project8
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
