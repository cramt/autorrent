using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void log(object o) {
            try {
                dynamic d = (dynamic)o;
                Console.WriteLine("line: " + d.line);
                Console.WriteLine(d.msg);
            }
            catch (Exception) { }
        }
        public void func(dynamic o) {
            Dictionary<string, object> a = Util.Dyn2Dict(o);
            foreach (var b in a) {
                Console.WriteLine(b);
            }
        }
    }
}
