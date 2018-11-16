using Leak.Common;
using System;

namespace au.Torrent {
    public class PieceCompletedEventArgs : EventArgs {
        public FileHash Hash;
        public PieceInfo Piece;
    }
}