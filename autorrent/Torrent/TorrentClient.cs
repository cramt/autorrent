using Leak.Client.Swarm;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace autorrent.Torrent {
    class TorrentClient {
        public Task<TorrentSession> InitFromMagnetLink(string magnetLink) {
            return InitFromMagnetLink(MagnetLink.Parse(magnetLink));
        }
        public async Task<TorrentSession> InitFromMagnetLink(MagnetLink magnetLink) {
            SwarmSession session = await swarm.ConnectAsync(magnetLink.Hash, magnetLink.Trackers);
            session.Download(DownloadsFolder);
            return new TorrentSession(session);
        }
        public TorrentClient(string downloadsFolder) {
            swarm = new SwarmClient();
            DownloadsFolder = downloadsFolder;
        }
        public string DownloadsFolder { get; private set; }
        private SwarmClient swarm;
    }
}
