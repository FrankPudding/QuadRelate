using QuadRelate.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuadRelate.Players.Wilko
{
    class Move
    {
        public int Column { get; set; }
        public Counter Counter { get; set; }

        public override int GetHashCode()
        {
            return Column.GetHashCode() ^ Counter.GetHashCode();
        }
    }
}
