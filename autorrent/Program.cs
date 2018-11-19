using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;
#pragma warning disable CS4014 

namespace autorrent {
    class Program {

        const bool DoTheWebpack = false;

        private static void Main(string[] args) {
            CefSettings settings = new CefSettings();
            Cef.EnableHighDPISupport();
            Cef.Initialize(settings);
            ChromeForm mainForm = new ChromeForm(MyWebpackBindingsStandards.DistIndexHtml);
            if (DoTheWebpack) {
                WebpackBindings bindings = new WebpackBindings(MyWebpackBindingsStandards.FrontendPath, MyWebpackBindingsStandards.WebpackStartCommand);
                Console.WriteLine("starting webpack");
                bindings.WaitForFirstBuild.Wait();
                Console.WriteLine("webpack stated");
                bindings.HotReload += (object sender, EventArgs e) => {
                    Console.WriteLine("webpack hot reload");
                };

                bindings.HotReload += (object sender, EventArgs e) => {
                    mainForm.Reload();
                };
            }
            Application.Run(mainForm);
            Cef.Shutdown();
        }
    }
}
