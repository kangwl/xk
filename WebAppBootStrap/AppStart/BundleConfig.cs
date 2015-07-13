using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace WebAppBS.AppStart {
    public class BundleConfig {

        public static void BundleResources(BundleCollection bundleCollection) {
            BundleBootstrapMain(bundleCollection);
        }


        private static void BundleBootstrapMain(BundleCollection bundleCollection) {
            //site base
            bundleCollection.Add(
                new StyleBundle("~/css/main")
                    .Include("~/Content/bootstrap/main/dist/css/bootstrap.*")
                );

            bundleCollection.Add(
                new ScriptBundle("~/js/main")
                    .Include("~/Content/script/jquery-{version}.js",
                    "~/Content/bootstrap/main/dist/js/bootstrap.*", 
                    "~/Content/script/bootbox.js")
                );

            //bootstrap select
            bundleCollection.Add(new StyleBundle("~/bs_select_css").
                Include("~/Content/bootstrap/Select/dist/css/bootstrap-select.*"));

            bundleCollection.Add(new ScriptBundle("~/bs_select_js").
                Include("~/Content/bootstrap/Select/dist/js/bootstrap-select.*",
                        "~/Content/bootstrap/Select/dist/js/i18n/defaults-zh_CN.js"));

            //bootstrap table
            bundleCollection.Add(new StyleBundle("~/bs_table_css").Include("~/Content/bootstrap/table/dist/bootstrap-table.css"));
            bundleCollection.Add(new ScriptBundle("~/bs_table_js").Include("~/Content/bootstrap/table/dist/bootstrap-table.js", 
                        "~/Content/bootstrap/table/dist/locale/bootstrap-table-zh-CN.js"));

            BundleTable.EnableOptimizations = true;

        }


    }
}