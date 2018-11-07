using Chromely.CefGlue.Winapi;
using Chromely.CefGlue.Winapi.ChromeHost;
using Chromely.Core;
using Chromely.Core.Helpers;
using System;
using WinApi.Windows;

namespace autorrent {
    class Program {
        static int Main(string[] args) {
            string startUrl = "https://www.google.com/";

            ChromelyConfiguration config = ChromelyConfiguration
                                          .Create()
                                          .WithAppArgs(args)
                                          .WithHostSize(1000, 600)
                                          // The single process should only be used for debugging purpose.
                                          // For production, this should not be needed when the app is published 

                                          // Alternate approach for multi-process, is to add a subprocess application
                                          //.WithCustomSetting(CefSettingKeys.BrowserSubprocessPath, path_to_sunprocess)
                                          .WithCustomSetting(CefSettingKeys.SingleProcess, true)
                                          .WithStartUrl(startUrl);

            var factory = WinapiHostFactory.Init();
            using (var window = factory.CreateWindow(() => new CefGlueBrowserHost(config),
                  "chromely", constructionParams: new FrameWindowConstructionParams())) {
                window.SetSize(config.HostWidth, config.HostHeight);
                window.CenterToScreen();
                window.Show();
                return new EventLoop().Run(window);
            }
        }
    }
}
