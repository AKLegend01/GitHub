using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    [Serializable]
    class Goblin : Enemy
    {
        
        public Goblin(int x, int y) : base(x, y, 1, 10, 'G')
        {
            this.TileEnum = TileType.Goblin;
            this.Gold = 1;
            this.PickedUpWeapon = new MeleeWeapon(MeleeWeapon.Types.Dagger);
        }

        public override Movements ReturnMove(Movements Move)
        {
            if (r == null)
            {
                r = new Random();
            }
            int move = r.Next(0, 5);
            while (Vision[move].TileEnum != TileType.Empty)
            {
                move = r.Next(0, 5);
            }
            return (Movements)move;
        }
    }
}
