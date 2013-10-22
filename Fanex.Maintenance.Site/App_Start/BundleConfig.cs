// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.8.30">Created</change>
// </history>

namespace Fanex.Maintenance.Site.App_Start
{
    using System.Web;
    using System.Web.Optimization;
    using BundleTransformer.Core.Orderers;
    using BundleTransformer.Core.Transformers;

    /// <summary>
    /// Bundle config.
    /// </summary>
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // css
            bundles.AddAndInclude(
                "~/css/common",
                "~/themes/reset.css",
                "~/themes/fonts.css",
                "~/scripts/Plugin/jquery-ui-1.9.1.custom/css/custom-theme/jquery-ui-1.9.1.custom.min.css",
                "~/themes/assets/css/prettify.css",
                "~/themes/assets/css/site.css",
                "~/scripts/lib/tipsy/tipsy-1.0.0.min.css",
                "~/scripts/lib/msgbox/msgbox-0.0.1.min.css",
                "~/scripts/Plugin/notification/jquery.toastmessage.css",
                "~/scripts/lib/chosen/chosen.css");

            bundles.AddAndInclude(
                "~/css/mail-list",
                "~/themes/job/user/manage.less",
                "~/themes/job/RefreshCountDown.less",
                "~/scripts/Plugin/Fanex.Grid/Fanex.Grid.css",
                "~/scripts/Plugin/Fanex.Grid/Fanex.Pagination.css");

            bundles.AddAndInclude(
                "~/css/manage",
                "~/themes/job/user/manage.less");

            bundles.AddAndInclude(
                "~/css/log",
                "~/themes/job/log/manage.less",
                "~/scripts/Plugin/Fanex.Grid/Fanex.Pagination.css",
                "~/scripts/Plugin/Select2/select2.css",
                "~/scripts/Plugin/Select2/select2-custom.css");

            // js
            bundles.Add(new ScriptBundle("~/scripts/common").Include(
                "~/scripts/lib/jquery-1.8.0.min.js",
                "~/scripts/lib/modernizr-2.6.1.js",
                "~/themes/assets/js/prettify.min.js",
                "~/themes/assets/js/underscore.min.js",
                "~/scripts/lib/jquery-ui-1.8.23.min.js",
                "~/scripts/lib/jquery.validate.min.js",
                "~/scripts/lib/jquery.validate.unobtrusive.min.js",
                "~/scripts/lib/tipsy/tipsy-1.0.0.min.js",
                "~/scripts/lib/msgbox/msgbox-0.0.1.min.js",
                "~/scripts/Maintenance.js",
                "~/scripts/_Layout.js",
                "~/scripts/lib/chosen/chosen.jquery.js",
                "~/scripts/Plugin/Fanex.Grid/Fanex.Grid.js",
                "~/scripts/Plugin/Fanex.Grid/Fanex.Pagination.js",
                "~/scripts/Plugin/jquery.cookie.js",
                "~/scripts/Plugin/RefreshCountDown.js",
                "~/scripts/lib/jsrender.js"));

            bundles.Add(new ScriptBundle("~/scripts/monitor").Include(
                "~/scripts/Plugin/notification/jquery.toastmessage.js",
                "~/scripts/lib/jquery.signalR-1.0.0-rc1.min.js",
                "~/signalr/hubs",
                "~/scripts/NotificationHub.js"));

            bundles.Add(new ScriptBundle("~/scripts/add-mail-config").Include(
                "~/scripts/Mail/AddMailConfig.js"));

            bundles.Add(new ScriptBundle("~/scripts/add-mail-config-popup").Include(
                "~/scripts/Mail/AddMailConfigPopUp.js"));

            bundles.Add(new ScriptBundle("~/scripts/add-mail-setting-popup").Include(
                "~/scripts/Mail/AddMailSettingPopUp.js"));

            bundles.Add(new ScriptBundle("~/scripts/add-mail-function").Include(
                "~/scripts/Mail/AddMailFunction.js"));

            bundles.Add(new ScriptBundle("~/scripts/log").Include(
                "~/scripts/Logs/Index.js",
                "~/scripts/Plugin/Select2/select2.js",
                "~/scripts/lib/custom-datepicker.js",
                "~/scripts/Plugin/Fanex.Grid/Fanex.Pagination.js"));

            bundles.Add(new ScriptBundle("~/scripts/recheckin").Include(
              "~/Areas/UserLogin/scripts/ReCheckIn.js"));

            bundles.FileSetOrderList.Clear();
        }
    }

    /// <summary>
    /// Bundle extensions.
    /// </summary>
    internal static class BundleExtensions
    {
        public static void AddAndInclude(this BundleCollection bundles, string bundleVirtualPath, params string[] virtualPaths)
        {
            bundles.Add(new Bundle(bundleVirtualPath)
            {
                Transforms = { new CssTransformer(), new CssMinify() },
                Orderer = new NullOrderer()
            }.Include(virtualPaths));
        }

        public static string RelativeFromAbsolutePath(this HttpContextBase context, string path)
        {
            var request = context.Request;
            var applicationPath = request.PhysicalApplicationPath;
            var virtualDir = request.ApplicationPath;
            virtualDir = virtualDir == "/" ? virtualDir : (virtualDir + "/");

            return path.Replace(applicationPath, virtualDir).Replace(@"\", "/");
        }
    }
}