using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class MeleeWeapon : Weapon
    {
        public enum Types {Dagger, LongSword}
        private int range;
        public override int Range
        {
            get { return range; }
        }

        public void SetRange(int Range)
        {
            this.range = Range;
        }


        public MeleeWeapon(Types M, int x = 0, int y = 0) : base('m', x, y)
        {
            switch(M)
            {
                case Types.Dagger :
                    Type = "Dagger";
                    Durability = 10;
                    SetRange(1);
                    Damage = 3;
                    Cost = 3;

                    break;
                case Types.LongSword :
                    Type = "Longsword";
                    Durability = 6;
                    SetRange(1);
                    Damage = 4;
                    Cost = 5;
                    break;
            }
        }
    }
}
