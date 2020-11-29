using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Shop
    {
        private Weapon[] weaponArray = new Weapon[3];
        private Random r = new Random();
        private Character Buyer;
        Map M;
        Weapon W;
        Character C;

        public Shop(Character whoBuy)
        {
            for (int i = 0; i < weaponArray.Length; i++)
            {
                weaponArray[i] = RandomWeapon();
            }
        }


        private Weapon RandomWeapon()
        {
            int PosX = r.Next(1, M.mapWidth);
            int PosY = r.Next(1, M.mapHeight);
            while (M.mapArray[PosY, PosX].TileEnum != Tile.TileType.Empty)
            {
                PosX = r.Next(1, M.mapWidth);
                PosY = r.Next(1, M.mapHeight);
            }

            int which = r.Next(0, 4);

            switch (which)
            {
                case 0: 
                    W = new MeleeWeapon(MeleeWeapon.Types.Dagger, PosX, PosY);
                    break;
                case 1:
                    W = new MeleeWeapon(MeleeWeapon.Types.LongSword, PosX, PosY);
                    break;
                case 2:
                    W = new RangedWeapon(RangedWeapon.Types.Longbow, PosX, PosY);
                    break;
                case 3:
                    W = new RangedWeapon(RangedWeapon.Types.Rifle, PosX, PosY);
                    break;

            }

            return W;
        }


        public bool CanBuy(int num)
        {
            if (weaponArray[num].Cost < C.Gold)
            {
                return true;
            }
            else return false;
        }


        public void Buy(int num)
        {
            C.Gold -= weaponArray[num].Cost;
            C.PickUp(weaponArray[num]);
            weaponArray[num] = RandomWeapon();
        }


        public string DisplayWeapon(int num)
        {
            return String.Format("Buy {0} weapon for {1}", weaponArray[num].Type, weaponArray[num].Cost);
        }
    }
}
