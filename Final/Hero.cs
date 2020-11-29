using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Hero : Character
    {
        public Hero(int X, int Y, int Hp) : base(X, Y, 'H')
        {
            this.HP = Hp;
            this.MaxHP = Hp;
            this.Damage = 2;
            this.TileEnum = TileType.Hero;
        }

        public override string ToString()
        {
            string Output = "Player Stats : \n";
            Output += "HP : " + HP + "/" + MaxHP + "\n";           
            Output += "[" + y + ", " + x + "]\n";
            Output += "Gold : " + Gold + "\n";
            if (PickedUpWeapon == null)
            {
                Output += "Weapon : Bare Hand\n";
                Output += "Weapon Range : 1\n";
                Output += "Damage : 2";
            }
            else
            {
                Output += "Weapon : " + PickedUpWeapon.Type + "\n";
                Output += "Weapon Range : " + PickedUpWeapon.Range + "\n";
                Output += "Damage : " + PickedUpWeapon.Damage;
            }


            return Output;
        }

        public override Movements ReturnMove(Movements Move)
        {

            var canMove = Vision[Convert.ToInt32(Move)].TileEnum;
            if (canMove == TileType.Empty) return Move;
            else return Movements.None;
        }
    }
}
