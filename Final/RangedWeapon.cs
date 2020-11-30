using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class RangedWeapon : Weapon
    {
        public enum Types {Rifle, Longbow};
        public override int Range { get => base.Range; set => base.Range = value; }


        public RangedWeapon(Types R, int x = 0, int y = 0) : base('r', x, y)
        {
            switch (R)
            {
                case Types.Rifle:
                    Type = "Rifle";
                    Durability = 3;
                    Range = 3;
                    Damage = 5;
                    Cost = 7;

                    break;
                case Types.Longbow:
                    Type = "Longbow";
                    Durability = 4;
                    Range = 2;
                    Damage = 4;
                    Cost = 6;
                    break;
            }
        }

        public RangedWeapon(Types R, int durable, int x = 0, int y = 0) : base('r', x, y)
        {
            switch (R)
            {
                case Types.Rifle:
                    Type = "Rifle";
                    Durability = durable;
                    Range = 3;
                    Damage = 5;
                    Cost = 7;

                    break;
                case Types.Longbow:
                    Type = "Longbow";
                    Durability = durable;
                    Range = 2;
                    Damage = 4;
                    Cost = 6;
                    break;
            }
        }
    }
}
