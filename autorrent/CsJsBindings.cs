using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autorrent {
    class CsJsBindings {
        public void log(object o) {
            switch (o) {
                case ExpandoObject expando:
                    dynamic d = (dynamic)expando;
                    Console.WriteLine(d.a);
                    break;
                default:
                    Console.WriteLine(o);
                    break;
            }
        }
    }
}
