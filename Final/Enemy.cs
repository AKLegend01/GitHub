using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    abstract class Enemy : Character
    {
        protected Random r = new Random();

        public Enemy(int x, int y, int Damage, int hp, char who) : base(x, y, who)
        {
            this.Damage = Damage;
            this.MaxHP = hp;
            this.HP = hp;
        }

        public override string ToString()
        {
            string Output;
            if (this.PickedUpWeapon == null)
            {
                Output = String.Format("{0}: {1} ({2}, {3}) at [{4}, {5}] ({6} DMG)", "Barehanded", nameof(Enemy), this.HP, this.MaxHP, y, x, this.Damage);
            }
            else Output = String.Format("{0}: {1} ({2}, {3}) at [{4}, {5}] with {6} (Durability: {7}, {8} DMG)", "Equipped", nameof(Enemy), this.HP, this.MaxHP, y, x, this.PickedUpWeapon.Type, this.PickedUpWeapon.Durability, this.PickedUpWeapon.Damage);
            return Output;


        }
    }
}
