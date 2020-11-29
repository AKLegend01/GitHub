using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Goblin : Enemy
    {
        
        public Goblin(int x, int y) : base(x, y, 1, 10, 'G')
        {
            this.TileEnum = TileType.Goblin;
            //this.PickedUpWeapon;
        }

        public override Movements ReturnMove(Movements Move)
        {
            int move = r.Next(0, 5);
            while (Vision[move].TileEnum != TileType.Empty) move = r.Next(0, 5);
            return (Movements)move;
        }
    }
}
