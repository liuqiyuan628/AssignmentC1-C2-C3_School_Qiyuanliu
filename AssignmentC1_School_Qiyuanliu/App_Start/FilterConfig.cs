using System.Web;
using System.Web.Mvc;

namespace AssignmentC1_School_Qiyuanliu
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
