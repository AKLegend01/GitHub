using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class RangedWeapon : Weapon
    {
        public enum Types {Rifle, Longbow};
        public override int Range { get => base.Range; set => base.Range = value; }


        public RangedWeapon(Types R, int x, int y) : base(x, y, 'r')
        {

        }

        // public RangedWeapon() : base()
    }
}
