using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autorrent {
    class Program {
        const bool doTheWebpack = true;
        static void Main(string[] args) {
            ChromeForm mainForm = new ChromeForm(MyWebpackBindingsStandards.DistIndexHtml);
            if (doTheWebpack) {
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
        }
    }
}
