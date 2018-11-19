using au.Torrent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autorrent {
    //in the js, all these functions are promises
    public static class Util {
        public static Dictionary<string, object> Dyn2Dict(dynamic dynObj) {
            var dictionary = new Dictionary<string, object>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(dynObj)) {
                object obj = propertyDescriptor.GetValue(dynObj);
                dictionary.Add(propertyDescriptor.Name, obj);
            }

            return dictionary;
        }
    }

    public class CsJsBindings {
        private ChromeForm chromeForm;

        public CsJsBindings(ChromeForm chromeForm) {
            this.chromeForm = chromeForm;
        }

        public void Log(object o) {
            try {
                dynamic d = (dynamic) o;
                Console.WriteLine("line: " + d.line);
                Console.WriteLine(d.msg);
            }
            catch (Exception) {
                // ignored
            }
        }

        public void WindowCaptureBorder() {
            chromeForm.CaptureBorder();
        }

        public void WindowReleaseBorder() {
            chromeForm.ReleaseBorder();
        }

        public void WindowClose() {
            Application.Exit();
        }

        public void WindowToggleMaximize() {
            chromeForm.Invoke(new Action(() => {
                if (chromeForm.WindowState == FormWindowState.Normal) {
                    chromeForm.WindowState = FormWindowState.Maximized;
                }
                else {
                    chromeForm.WindowState = FormWindowState.Normal;
                }
            }));
        }

        public void WindowMinimize() {
            chromeForm.Invoke(new Action(() => { chromeForm.WindowState = FormWindowState.Minimized; }));
        }

        public int TorrentInitFromMagnetLink(string magnetLink) {
            TorrentSession torrent = new TorrentClient(@"C:\Users\madsc\Downloads")
                .InitFromMagnetLink(MagnetLink.Parse(magnetLink)).GetAwaiter().GetResult();
            return 0;
        }
    }
}