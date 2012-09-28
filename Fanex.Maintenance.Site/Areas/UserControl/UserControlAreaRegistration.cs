// --------------------------------------------------------------------------
// <copyright file="UserControlAreaRegistration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
// </history>

namespace Fanex.NAP.Admin.Areas.UserControl
{
    using System.Web.Http;
    using System.Web.Mvc;

    public class UserControlAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UserControl";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.Routes.MapHttpRoute(
            //   name: "UserControl_DefaultApi",
            //   routeTemplate: "Admin/api/{controller}/{id}",
            //   defaults: new { id = RouteParameter.Optional }
            //);

            context.MapRoute(
                "UserControl_Default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}