using au.Torrent;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
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
                dynamic d = (dynamic)o;
                Console.WriteLine("line: " + d.line);
                Console.WriteLine(d.msg);
            }
            catch (Exception) { }
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
            chromeForm.Invoke(new Action(() => { chromeForm.WindowState = chromeForm.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal; }));
        }
        public void WindowMinimize() {
            chromeForm.Invoke(new Action(() => {
                chromeForm.WindowState = FormWindowState.Minimized;
            }));
        }
        private List<GCHandle> TorrentSessions = new List<GCHandle>();
        public string TorrentInitFromMagnetLink(string magnetLink) {
            TorrentSession session = Program.TorrentClient.InitFromMagnetLink(magnetLink).GetAwaiter().GetResult();
            GCHandle handle = GCHandle.Alloc(session);
            TorrentSessions.Add(handle);
            return handle.ToIntPtr() + "";
        }
        public string TorrentGetProgress(string sptr) {
            object target = TorrentSessions.Single(x => x.ToIntPtr() + "" == sptr).Target;
            if (!(target is TorrentSession ses)) return "";
            ses.Metainfo.ContinueWith(x => {
                Console.WriteLine(x.Result);
            });
            Console.WriteLine(target.GetType());
            return JsonConvert.SerializeObject(ses.Metainfo.GetAwaiter().GetResult());

        }
    }
}
