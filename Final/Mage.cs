using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Mage : Enemy
    {
        public Mage(int x, int y) : base(x, y, 5, 5, 'M')
        {
            this.TileEnum = TileType.Mage;
            this.Gold = 3;
        }

        public override Movements ReturnMove(Movements Move)
        {
            return Movements.None;
        }

        public override bool CheckRange(Character Target)
        {
            if ((Target.x == x + 1) || (Target.x == x - 1) || (Target.y == y + 1) || (Target.y == y - 1)) return true; //x and y +- 1 seperately
            if ((Target.x == x + 1 && Target.y == y + 1) || (Target.x == x + 1 && Target.y == y - 1)) return true;  // x + 1 and y together
            if ((Target.x == x - 1 && Target.y == y + 1) || (Target.x == x - 1 && Target.y == y - 1)) return true;  // x - 1 and y together
            else return false;
        }
    }
}
