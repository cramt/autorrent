using au.Torrent;
using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#pragma warning disable CS4014 

namespace autorrent {
    class Program {
        static async Task Test() {
            MagnetLink m = MagnetLink.Parse("magnet:?xt=urn:btih:b88c7d3fb38c305350d064a83517d18d564a609e&dn=The.Flash.2014.S05E04.News.Flash.1080p.AMZN.WEBRip.DDP5.1.x264-NTb%5Brartv%5D&tr=http%3A%2F%2Ftracker.trackerfix.com%3A80%2Fannounce&tr=udp%3A%2F%2F9.rarbg.me%3A2710&tr=udp%3A%2F%2F9.rarbg.to%3A2710");
            var session = await new TorrentClient(@"C:\Users\madsc\Downloads").InitFromMagnetLink(m);
            float? piecesLength = null;
            session.Metainfo.ContinueWith(x => {
                piecesLength = x.Result.Pieces.Length;
            });
            float pieceIndex = 0;
            session.PieceCompleted += (object sender, PieceCompletedEventArgs e) => {
                pieceIndex++;
                if (piecesLength != null) {
                    Console.WriteLine(pieceIndex/piecesLength);
                }
            };
            await session.DataCompletion;
            Console.WriteLine("done");
        }

        const bool doTheWebpack = false;
        static void Main(string[] args) {
            CefSettings settings = new CefSettings();
            Cef.EnableHighDPISupport();
            Cef.Initialize(settings);
            //Test();
            //Thread.Sleep(-1);
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
            Cef.Shutdown();
        }
    }
}
