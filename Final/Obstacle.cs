using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Obstacle : Tile
    {       
        public Obstacle(int x, int y) : base(x, y)
        {
            this.TileEnum = TileType.Obsticle;
        }
    }
}
