using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend {
    class MyWebpackBindingsStandards {
        private static string _frontendPath = (new Func<string>(() => {
            string debugDir(string dir) {
                var d = Directory.GetParent(dir);
                if(d.Name == "autorrent") {
                    return d.FullName;
                }
                return debugDir(d.FullName);
            }
            return Path.Combine(debugDir(Directory.GetCurrentDirectory()), "frontend");
        }))();
        public static string FrontendPath {
            get {
                return _frontendPath;
            }
        }
        public static string WebpackStartCommand {
            get => "npm start";
        }
    }
    class WebpackBindings {

        private static readonly string OldWebpackIdFileName = "oldWebpackId";
        private void CleanProcesses() {
            try {
                if (!File.Exists(OldWebpackIdFileName)) {
                    return;
                }
                if (!int.TryParse(File.ReadAllText(OldWebpackIdFileName), out int pid)) {
                    return;
                }
                Process.GetProcessById(pid).Kill();
                return;
            }
            catch (Exception) { };
        }
        public event EventHandler HotReload;
        public event EventHandler FirstBuild;
        public Task WaitForFirstBuild;
        private bool first = true;
        private Process webpackProcess;
        public WebpackBindings(string dir, string command) {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            WaitForFirstBuild = tcs.Task;
            /*
            {
                string path = Path.Combine(dir, "outputPath");
                if (File.Exists(path)) {
                    File.Delete(path);
                }
                var outputFile = File.CreateText(path);
                outputFile.Write(Path.Combine(Directory.GetCurrentDirectory(), "dist"));
                outputFile.Close();
            }
            */
            CleanProcesses();
            webpackProcess = Process.Start(new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = "/c " + command,
                WorkingDirectory = dir,
                RedirectStandardOutput = true,
                UseShellExecute = false
            });
            File.CreateText(OldWebpackIdFileName).Write(webpackProcess.Id + "");
            webpackProcess.OutputDataReceived += (object sender, DataReceivedEventArgs e) => {
                try {
                    if (e.Data.Contains("i ´¢óatl´¢ú: Time:")) {
                        if (first) {
                            FirstBuild?.Invoke(this, EventArgs.Empty);
                            tcs.SetResult(true);
                            first = false;
                        }
                        else {
                            HotReload?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
                catch (Exception) { };
            };
            webpackProcess.BeginOutputReadLine();
        }
        public void Close() {
            webpackProcess.Close();
        }
    }
}
