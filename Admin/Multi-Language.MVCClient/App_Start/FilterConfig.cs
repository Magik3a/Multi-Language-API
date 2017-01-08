using System.Web;
using System.Web.Mvc;
using Multi_Language.MVCClient.Attributes;

namespace Multi_Language.MVCClient
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CatchErrorAttribute());
            filters.Add(new ValidationActionFilterAttribute());
        }
    }
}
