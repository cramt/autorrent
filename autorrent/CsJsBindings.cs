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
        public static Dictionary<String, Object> Dyn2Dict(dynamic dynObj) {
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
        public void log(object o) {
            try {
                dynamic d = (dynamic)o;
                Console.WriteLine("line: " + d.line);
                Console.WriteLine(d.msg);
            }
            catch (Exception) { }
        }
        public void captureBorder() {
            chromeForm.CaptureBorder();
        }
        public void releaseBorder() {
            chromeForm.ReleaseBorder();
        }
        public void close() {
            Application.Exit();
        }
        public void toggleMaximize() {
            chromeForm.Invoke(new Action(() => {
                if (chromeForm.WindowState == FormWindowState.Normal) {
                    chromeForm.WindowState = FormWindowState.Maximized;
                }
                else {
                    chromeForm.WindowState = FormWindowState.Normal;
                }
            }));
        }
        public void minimize() {
            chromeForm.Invoke(new Action(() => {
                chromeForm.WindowState = FormWindowState.Minimized;
            }));
        }
    }
}
