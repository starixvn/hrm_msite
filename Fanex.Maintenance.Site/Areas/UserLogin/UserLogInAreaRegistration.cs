using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Dispatcher;
using System.Web.Routing;

namespace Fanex.NAP.Hub.Areas.UserLogin
{
    public class UserLoginAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UserLogin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                AreaName + "_DefaultAreas",

                "UserLogin/{controller}/{action}/{id}",

                new { controller = "Login", action = "Index", id = UrlParameter.Optional }

            );

            context.MapRoute(
              AreaName + "_Themes",
              "UserLogin/Themes/{resourceName}"
          );

        }
        
    }
}