using Leak.Client;
using Leak.Client.Notifications;
using Leak.Client.Swarm;
using Leak.Common;
using System;
using System.Threading.Tasks;

namespace au.Torrent {
    public class TorrentSession {
        public Task<Metainfo> Metainfo { get; private set; }
        public Task DataCompletion { get; private set; }
        public event EventHandler<PieceCompletedEventArgs> PieceCompleted = null;
        internal TorrentSession(SwarmSession session) {
            TaskCompletionSource<Metainfo> metainfoCompletionSource = new TaskCompletionSource<Metainfo>();
            Metainfo = metainfoCompletionSource.Task;
            TaskCompletionSource<bool> dataCompletionSource = new TaskCompletionSource<bool>();
            DataCompletion = dataCompletionSource.Task;
            Task.Factory.StartNew(async () => {
                bool j = true;
                while (j) {
                    Notification notification = await session.NextAsync();
                    switch (notification.Type) {
                        case NotificationType.MetafileCompleted:
                            metainfoCompletionSource.SetResult(((MetafileCompletedNotification)notification).Metainfo);
                            break;
                        case NotificationType.PieceCompleted:
                            var pcNot = (PieceCompletedNotification)notification;
                            PieceCompleted?.Invoke(this, new PieceCompletedEventArgs() {
                                Hash = pcNot.Hash,
                                Piece = pcNot.Piece
                            });
                            break;
                        case NotificationType.DataCompleted:
                            dataCompletionSource.SetResult(true);
                            break;
                    }
                }
            });

        }
    }
}