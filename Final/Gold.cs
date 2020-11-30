using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    [Serializable]
    class Gold : Item
    {
        private int GoldAmount;
        [NonSerialized]
        private Random R = new Random();

        public int goldAmount { get => GoldAmount; set => GoldAmount = value; }

        public Gold(int x, int y) : base(x, y)
        {
            GoldAmount = R.Next(1,6);
            this.TileEnum = TileType.Gold;
        }

        
    }
}
