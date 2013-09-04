// --------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.8.30">Created</change>
// </history>

namespace Fanex.Maintenance.Site
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Fanex.Maintenance.Site.App_Start;
    using Microsoft.AspNet.SignalR;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode,
    // visit http://go.microsoft.com/?LinkId=9394801
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            RouteTable.Routes.MapHubs();

            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Fanex.Data.Configuration.DbSettingProviderManager.Current.Start();

            //if (AppSetting.NotifiJob)
            //{
            //    NotificationWorker notificationWorker = new NotificationWorker(AppSetting.GetDataNotifyInterval);
            //    notificationWorker.Start();
            //}

            //if (AppSetting.NotifiServer)
            //{
            //    CpuUtilizationWorker cpuUtilizationWorker = new CpuUtilizationWorker(AppSetting.GetCpuUtilizationInterval);
            //    cpuUtilizationWorker.Start();

            //    MemoryUtilizationWorker memoryUtilizationWorker = new MemoryUtilizationWorker(AppSetting.GetMemoryUtilizationInterval);
            //    memoryUtilizationWorker.Start();
            //}
        }
    }
}