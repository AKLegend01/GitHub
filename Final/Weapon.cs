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
        private char what;

        public int Damage { get => damage; set => damage = value; }
        public virtual int Range { get; }
        public int Durability { get => durability; set => durability = value; }
        public int Cost { get => cost; set => cost = value; }
        public string Type { get => type; set => type = value; }


        public Weapon(char what, int x = 0, int y = 0) : base(x, y)
        {
            this.what = what;
            this.TileEnum = TileType.Weapon;
        }

        public override string ToString()
        {
            return Convert.ToString(what);
        }
    }
}
