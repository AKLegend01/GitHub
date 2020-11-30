using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    [Serializable]
    abstract class Character : Tile
    {
        private int hP;
        private int maxHP;
        private int damage;
        private Tile[] vision = new Tile[8];
        private int goldPocket;
        protected Weapon pickedUpWeapon;

        public enum Movements {Up, Down, Left, Right, None };

        public int HP { get => hP; set => hP = value; }
        public int MaxHP { get => maxHP; set => maxHP = value; }
        public int Damage { get => damage; set => damage = value; }
        public Tile[] Vision { get => vision; set => vision = value; }
        public int Gold { get => goldPocket; set => goldPocket = value; }
        public Weapon PickedUpWeapon { get => pickedUpWeapon; set => pickedUpWeapon = value; }

        public Character(int x, int y, char Who) : base(x, y)
        {

        }

        public virtual void Attack(Character Target)
        {
            Target.HP -= this.Damage;
            if (Target.HP <= 0)
            {
                Loot(Target);
            }
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

            if (I.TileEnum == TileType.Weapon)
            {
                Equip((Weapon)I);
            }
        }

        private void Equip(Weapon W)
        {
            this.pickedUpWeapon = W;
        }

        public void Loot(Character target)
        {
            this.goldPocket += target.goldPocket;

            if (target.pickedUpWeapon != null && this.pickedUpWeapon == null)
            {
                this.pickedUpWeapon = target.pickedUpWeapon;
            }


        }
    }


}
