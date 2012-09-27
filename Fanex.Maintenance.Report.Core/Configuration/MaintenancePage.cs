using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Globalization;
using System.Web.Script.Serialization;

namespace IBCMaintenance.Configuration
{
    public class MaintenancePage : Page
    {
        public CultureInfo ci = CultureInfo.CurrentCulture;
        public HttpContext hc = HttpContext.Current;
        public string nl = Environment.NewLine;
        public JavaScriptSerializer js = new JavaScriptSerializer();
        protected override void OnPreInit(EventArgs e)
        {            
            if (AppSession.CustID == -1) Root.Authorization.SignOut(HttpContext.Current);
            base.OnPreInit(e);
        }
        //protected override void Render(HtmlTextWriter writer)
        //{
        //    base.Render(writer);
        //}
    }

    public class MaintenanceHandler
    {
        public static HttpContext InitializeContext(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (AppSession.CustID == -1) Root.Authorization.SignOut(context);
            return context;
        }
        
    }

    public class MaintenanceClass
    {
        public static CultureInfo ci = CultureInfo.CurrentCulture;
        public static HttpContext hc = HttpContext.Current;
        public static string nl = Environment.NewLine;
        public static JavaScriptSerializer js = new JavaScriptSerializer();        
    }
}
