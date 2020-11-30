using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    public abstract class Tile
    {
        private int X;
        private int Y;
        public enum TileType { Hero, Mage, Goblin, Gold, Weapon, Empty, Obsticle, Leader};
        private TileType tileEnum;

        public int x { get => X; set => X = value; }
        public int y { get => Y; set => Y = value; }
        public TileType TileEnum { get => tileEnum; set => tileEnum = value; }


        public Tile(int x, int y)
        {
            this.Y = y;
            this.X = x;
        }
    }
}
