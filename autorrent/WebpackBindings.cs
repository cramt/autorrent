using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autorrent {
    class MyWebpackBindingsStandarts {
        private static string _frontendPath = (new Func<string>(() => {
            List<string> a = Directory.GetCurrentDirectory().Split('\\').ToList();
            a.RemoveAt(a.Count - 1);
            a.RemoveAt(a.Count - 1);
            a.RemoveAt(a.Count - 1);
            a.RemoveAt(a.Count - 1);
            return Path.Join(String.Join('\\', a.ToArray()), "frontend");
        }))();
        public static string FrontendPath {
            get {
                return _frontendPath;
            }
        }
        public static string WebpackStartCommand {
            get => "\"" + Environment.GetEnvironmentVariable("npm") + " run start:json\"";
        }
    }
    class WebpackBindings {
        
        private static readonly string OldWebpackIdFileName = "oldWebpackId";
        private void CleanProcesses() {
            if (!File.Exists(OldWebpackIdFileName)) {
                return;
            }
            if(!int.TryParse(File.ReadAllText(OldWebpackIdFileName), out int pid)) {
                return;
            }
            Process.GetProcessById(pid).Kill();
            return;
        }
        public event EventHandler HotReload;
        public event EventHandler FirstBuild;
        public Task WaitForFirstBuild;
        private bool first = true;
        public WebpackBindings(string dir, string command) {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            WaitForFirstBuild = tcs.Task;
            CleanProcesses();
            Process webpackProcess = Process.Start(new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = "/c npm start",
                WorkingDirectory = dir,
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
            File.CreateText(OldWebpackIdFileName).Write(webpackProcess.Id + "");
            webpackProcess.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
                if(e.Data.Contains("i ´¢óatl´¢ú: Time:")) {
                    if (first) {
                        FirstBuild?.Invoke(this, EventArgs.Empty);
                        tcs.SetResult(true);
                        first = false;
                    }
                    else {
                        HotReload?.Invoke(this, EventArgs.Empty);
                    }
                }
            };
            webpackProcess.BeginOutputReadLine();
        }
    }
}
