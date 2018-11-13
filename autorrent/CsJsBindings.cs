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
            switch (o) {
                case ExpandoObject expando:
                    
                    break;
                default:
                    Console.WriteLine(o);
                    break;
            }
        }
    }
}
