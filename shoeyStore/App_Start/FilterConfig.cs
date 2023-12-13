using System.Web;
using System.Web.Mvc;

namespace shoeyStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new shoeyStore.Filters.VerificarSesion());
        }
    }
}
