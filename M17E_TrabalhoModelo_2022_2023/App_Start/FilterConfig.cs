using System.Web;
using System.Web.Mvc;

namespace M17E_TrabalhoModelo_2022_2023
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
