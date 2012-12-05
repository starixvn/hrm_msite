namespace Fanex.Maintenance.Site
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Data;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Controller.Worker;
    using System.Configuration;
    using Fanex.Common;
    using Fanex.Maintenance.Core.Web;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();

            NotificationWorker notificationWorker = new NotificationWorker(AppSetting.GetDataNotifyInterval);
            notificationWorker.Start();

            
            ServerInfoWorker serverInfoWorker = new ServerInfoWorker(AppSetting.GetServerResourcesInfoInterval);
            serverInfoWorker.Start();
        }
    }
}