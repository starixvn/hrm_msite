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

    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region css

            bundles.AddAndInclude(
                "~/css/commond",
                "~/themes/reset.css",
                "~/themes/fonts.css",
                "~/scripts/Plugin/jquery-ui-1.9.1.custom/css/custom-theme/jquery-ui-1.9.1.custom.min.css",
                "~/themes/assets/css/prettify.css",
                "~/themes/assets/css/site.css",
                "~/scripts/lib/tipsy/tipsy-1.0.0.min.css",
                "~/scripts/lib/msgbox/msgbox-0.0.1.min.css",
                "~/scripts/Plugin/notification/jquery.toastmessage.css");

            #endregion css

            #region js

            bundles.Add(new ScriptBundle("~/scripts/topbarjs").Include(
                "~/_globalresources/js/head.js",
                "~/_globalresources/js/jquery.js",
                "~/_globalresources/js/core.js",
                "~/views/account/resources/header.js"));
            #endregion js

            bundles.FileSetOrderList.Clear();
        }
    }

    internal static class BundleExtensions
    {
        public static void AddAndInclude(
            this BundleCollection bundles,
            string bundleVirtualPath, params string[] virtualPaths)
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