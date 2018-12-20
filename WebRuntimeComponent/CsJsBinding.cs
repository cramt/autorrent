using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace WebRuntimeComponent {
    [AllowForWeb]
    public sealed class CsJsBinding {
        public void Log(object o) {
            Debug.WriteLine(o);
        }
    }
}
