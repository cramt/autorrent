using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace autorrent {
    public class ChromeForm : Form {
        #region capture and release border stuff

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion


        public void CaptureBorder() {
            Invoke(new Action(() => {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }));
        }

        public void ReleaseBorder() {
            Invoke(new Action(() => {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }));
        }

        public ChromiumWebBrowser chromeBrowser;

        public ChromeForm(string path) {
            /*
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.ControlBox = false;
            this.Text = String.Empty;
            */
            Text = "autorrent";
            Icon = new Icon("autorrent_logo.ico");

            chromeBrowser = new ChromiumWebBrowser(path);
            Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            chromeBrowser.Location = new Point(0, 0);
            chromeBrowser.Margin = new Padding(0, 0, 0, 0);
            FormClosing += (object o, FormClosingEventArgs e) => { };
            chromeBrowser.IsBrowserInitializedChanged += (object sender, IsBrowserInitializedChangedEventArgs e) => {
                chromeBrowser.ShowDevTools();
            };
            WindowState = FormWindowState.Maximized;

            CsJsBindings bindingObject = new CsJsBindings(this);
            chromeBrowser.JavascriptObjectRepository.Register("CsJsObject", bindingObject, true);
        }


        public void Reload() {
            chromeBrowser.Reload();
        }
    }
}