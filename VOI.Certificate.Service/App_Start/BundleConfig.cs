//------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="AACOSTA">
//     Copyright (c) AACOSTA. All rights reserved.
// </copyright>
//------------------------------------------------------------------------

namespace VOI.Certificate.Service
{
    using System.Web;
    using System.Web.Optimization;

    /// <summary>
    /// BundleConfig Class
    /// </summary>
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/sweetalert.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sweetalert.css"));
        }
    }
}
