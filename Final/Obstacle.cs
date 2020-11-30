using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    [Serializable]
    class Obstacle : Tile
    {       
        public Obstacle(int x, int y) : base(x, y)
        {
            this.TileEnum = TileType.Obsticle;
        }
    }
}
