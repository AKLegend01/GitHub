using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    [Serializable]
    class EmptyTile : Tile
    {
        public EmptyTile(int x, int y) : base(x, y)
        {
            this.TileEnum = TileType.Empty;
        }
    }
}
