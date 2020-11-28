using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    abstract class Weapon : Item
    {
        private int damage;
        private int range;
        private int durability;
        private int cost;
        private string type;

        public int Damage { get => damage; set => damage = value; }
        public virtual int Range { get => range; set => range = value; }
        public int Durability { get => durability; set => durability = value; }
        public int Cost { get => cost; set => cost = value; }
        public string Type { get => type; set => type = value; }


        public Weapon(int x, int y, char what) : base(x, y)
        {

        }
    }
}
