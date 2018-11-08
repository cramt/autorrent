using Chromely.CefGlue.Winapi;
using Chromely.CefGlue.Winapi.ChromeHost;
using Chromely.Core;
using Chromely.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WinApi.Windows;

namespace backend {
    class Program {
        static int Main(string[] args) {
            string startUrl = "local://dist/index.html";
            
            WebpackBindings webpackBindings = new WebpackBindings(MyWebpackBindingsStandards.FrontendPath, MyWebpackBindingsStandards.WebpackStartCommand);
            Console.WriteLine("starting webpack dev server");
            webpackBindings.WaitForFirstBuild.Wait();
            webpackBindings.HotReload += (object sender, EventArgs e) => {
                Console.WriteLine("hot reload");
            };
            Console.WriteLine("webpack dev server");

            ChromelyConfiguration config = ChromelyConfiguration
                                          .Create()
                                          .WithAppArgs(args)
                                          .WithHostSize(1000, 600)
                                          .UseDefaultResourceSchemeHandler("local", string.Empty)
                                          .WithCustomSetting(CefSettingKeys.SingleProcess, true)
                                          .WithStartUrl(startUrl);

            var factory = WinapiHostFactory.Init();
            using (var window = factory.CreateWindow(() => new CefGlueBrowserHost(config),
                  "chromely", constructionParams: new FrameWindowConstructionParams())) {
                window.SetSize(config.HostWidth, config.HostHeight);
                window.CenterToScreen();
                window.Show();
                window.Destroyed += () => {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + Process.GetCurrentProcess().Id);

                };
                return new EventLoop().Run(window);
            }
        }
    }
}
