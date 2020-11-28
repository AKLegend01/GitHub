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
            string Output = String.Format("{0}[{1}, {2}], Damage: {3}, HP: {4}",nameof(Enemy) , y , x , this.Damage, this.HP);
            return Output;


        }
    }
}
