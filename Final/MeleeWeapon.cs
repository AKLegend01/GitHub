using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class MeleeWeapon : Weapon
    {
        public enum Types {Dagger, LongSword}
        public override int Range { get => base.Range; set => base.Range = 1; }


        public MeleeWeapon(Types M, int x, int y) : base(x, y, '!')
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
