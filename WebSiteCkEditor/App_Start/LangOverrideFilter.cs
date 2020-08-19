using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WebSiteCkEditor
{
    public class LangOverrideFilter : IActionFilter
    {
        public string DefaultCulture { get; set; } = "kz";
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values["Controller"].ToString().ToLower() != "admin")
            {
                string culture2 = filterContext.RouteData.Values["controller"]?.ToString();
                if(culture2 == "kz" || culture2 == "en" || culture2 == "ru")
                {
                    var ci = CultureInfo.GetCultureInfo(culture2.ToString());
                    if (ci != Thread.CurrentThread.CurrentCulture && ci != Thread.CurrentThread.CurrentUICulture)
                    {
                        Thread.CurrentThread.CurrentCulture = ci;
                        Thread.CurrentThread.CurrentUICulture = ci;
                    }
                }
                 
            }

        }
    }
}