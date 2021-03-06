﻿using System.Web;
using System.Web.Optimization;

namespace AspnetIdentityFromScratch
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-1.12.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                "~/Content/themes/smoothness/jquery-ui-1.8.23.custom.css",
                "~/Content/themes/smoothness/jquery-ui.theme.css"));

            bundles.Add(new StyleBundle("~/Content/jqGrid").Include("~/Content/jquery.jqGrid/ui.jqgrid.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqGrid").Include("~/Scripts/jquery.jqGrid.min.js",
                "~/Scripts/i18n/grid.locale-ru.js"));

        }
    }
}
