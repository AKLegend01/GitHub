using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class MeleeWeapon : Weapon
    {
        public enum Types {Dagger, LongSword}
        public override int Range { get => base.Range; set => base.Range = 1; }


        public MeleeWeapon(Types M, int x = 0, int y = 0) : base('m', x, y)
        {
            switch(M)
            {
                case Types.Dagger :
                    Type = "Dagger";
                    Durability = 10;
                    Damage = 3;
                    Cost = 3;

                    break;
                case Types.LongSword :
                    Type = "Longsword";
                    Durability = 6;
                    Damage = 4;
                    Cost = 5;
                    break;
            }
        }
    }
}
