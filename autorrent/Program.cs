using Chromely.CefGlue.Winapi;
using Chromely.CefGlue.Winapi.ChromeHost;
using Chromely.Core;
using Chromely.Core.Helpers;
using Chromely.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using WinApi.Windows;

namespace autorrent {
    class Program {
        static int Main(string[] args) {
           
            WebpackBindings webpackBindings = new WebpackBindings(MyWebpackBindingsStandarts.FrontendPath, MyWebpackBindingsStandarts.WebpackStartCommand);
            webpackBindings.WaitForFirstBuild.Wait();
            //string startUrl = "local://" + String.Join('/', Path.Join(MyWebpackBindingsStandarts.FrontendPath, "dist", "index.html").Split('\\'));
            string startUrl = "local://index.html";
            Temp t = new Temp();
            ChromelyConfiguration config = ChromelyConfiguration
                                          .Create()
                                          .WithAppArgs(args)
                                          .WithHostSize(1000, 600)
                                          // The single process should only be used for debugging purpose.
                                          // For production, this should not be needed when the app is published 

                                          // Alternate approach for multi-process, is to add a subprocess application
                                          //.WithCustomSetting(CefSettingKeys.BrowserSubprocessPath, path_to_sunprocess)
                                          .WithLogFile("logs\\chromely.cef_new.log")
                                          .WithLogSeverity(LogSeverity.Info)
                                              .UseDefaultLogger()
                                          //.WithCustomSetting(CefSettingKeys.SingleProcess, true)
                                          .RegisterJsHandler("temp", t, null, false)
                                          .UseDefaultResourceSchemeHandler("local", string.Empty)
                                          .WithStartUrl(startUrl);
            var factory = WinapiHostFactory.Init();
            using (var window = factory.CreateWindow(() => new CefGlueBrowserHost(config),
                  "autorrent", constructionParams: new FrameWindowConstructionParams())) {
                window.SetSize(config.HostWidth, config.HostHeight);
                window.CenterToScreen();
                window.Show();
                return new EventLoop().Run(window);
            }
        }
    }
}
