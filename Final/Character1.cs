﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    abstract class Character : Tile
    {
        private int hP;
        private int maxHP;
        private int damage;
        private Tile[] vision = new Tile[8];
        private int goldPocket;
        public enum Movements { None, Up, Down, Left, Right };

        public int HP { get => hP; set => hP = value; }
        public int MaxHP { get => maxHP; set => maxHP = value; }
        public int Damage { get => damage; set => damage = value; }
        public Tile[] Vision { get => vision; set => vision = value; }
        public int Gold { get => goldPocket; set => goldPocket = value; }



        public Character(int x, int y, char Who) : base(x, y)
        {

        }

        public virtual void Attack(Character Target)
        {
            Target.HP -= this.Damage;
        }

        public bool IsDead()
        {
            if (this.HP <= 0) return true;
            else return false;
        }

        public virtual bool CheckRange(Character Target)
        {
            int Distance = DistanceTo(Target);
            if (Distance == 1) return true;
            else return false;
        }

        private int DistanceTo(Character target)
        {
            int DistanceX = (x - target.x);
            int DistanceY = (y - target.y);

            if (DistanceX < 0) DistanceX *= -1;
            if (DistanceY < 0) DistanceY *= -1;

            int Total = DistanceX + DistanceY;
            return Total;
        }

        public void Move(Movements Move)
        {
            switch (Move)
            {
                case Movements.Up:
                    y -= 1;
                    break;
                case Movements.Down:
                    y += 1;
                    break;
                case Movements.Right:
                    x += 1;
                    break;
                case Movements.Left:
                    x -= 1;
                    break;
                case Movements.None:
                    x += 0;
                    break;
            }
        }

        public abstract Movements ReturnMove(Movements Move = 0);

        public abstract override string ToString();

        public void PickUp(Item I)
        {
            if (I.TileEnum == TileType.Gold)
            {
                var gold = (Gold)I;
                this.goldPocket += gold.goldAmount;
            }
        }
    }


}