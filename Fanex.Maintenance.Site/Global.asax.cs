namespace Fanex.Maintenance.Site
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Fanex.Maintenance.Controller.Worker;
    using Fanex.Maintenance.Core.Web;

    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
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