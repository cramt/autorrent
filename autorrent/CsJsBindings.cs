using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autorrent {
    //in the js, all these functions are promises
    public class CsJsBindings {
        public void log(object o) {
            try {
                dynamic d = (dynamic)o;
                Console.WriteLine("line: " + d.line);
                Console.WriteLine(d.msg);
            }
            catch (Exception) { }
        }
    }
}
