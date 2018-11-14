using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace autorrent {
    class ChromeForm : Form {
        public ChromiumWebBrowser chromeBrowser;
        public ChromeForm(string path) {
            Console.WriteLine(path);
            CefSettings settings = new CefSettings();
            Cef.EnableHighDPISupport();
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser(path);
            Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            chromeBrowser.Location = new System.Drawing.Point(0, 0);
            chromeBrowser.Margin = new Padding(0, 0, 0, 0);
            FormClosing += (object o, FormClosingEventArgs e) => {
                Cef.Shutdown();
            };
            chromeBrowser.IsBrowserInitializedChanged += (object sender, IsBrowserInitializedChangedEventArgs e) => {
                if (e.IsBrowserInitialized) {
                    chromeBrowser.ShowDevTools();
                }
            };
            WindowState = FormWindowState.Maximized;
            CsJsBindings bindingObject = new CsJsBindings();
            chromeBrowser.JavascriptObjectRepository.Register("CsJsObject", bindingObject, true);
        }


        public void Reload() {
            chromeBrowser.Reload();
        }
    }
}
