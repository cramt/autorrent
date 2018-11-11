using Leak.Common;
using System;

namespace autorrent.Torrent {
    public class PieceCompletedEventArgs : EventArgs {
        public FileHash Hash;
        public PieceInfo Piece;
    }
}