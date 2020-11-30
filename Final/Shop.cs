using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    [Serializable]
    class Shop
    {
        private Weapon[] weaponArray = new Weapon[3];
        [NonSerialized]
        private Random r = new Random();
        private Character buyer;

        public Shop(Character whoBuy)
        {
            buyer = whoBuy;
            for (int i = 0; i < weaponArray.Length; i++)
            {
                weaponArray[i] = RandomWeapon();
            }
        }


        private Weapon RandomWeapon()
        {
            int weaponType = r.Next(0, 4);
            Weapon weapon = null;
            switch (weaponType)
            {
                case 0:
                    weapon = new MeleeWeapon(MeleeWeapon.Types.Dagger);
                    break;
                case 1:
                    weapon = new MeleeWeapon(MeleeWeapon.Types.LongSword);
                    break;
                case 2:
                    weapon = new RangedWeapon(RangedWeapon.Types.Longbow);
                    break;
                case 3:
                    weapon = new RangedWeapon(RangedWeapon.Types.Rifle);
                    break;

            }

            return weapon;
        }


        public bool CanBuy(int num)
        {
            if (weaponArray[num].Cost < buyer.Gold)
            {
                return true;
            }
            else return false;
        }


        public void Buy(int num)
        {
            buyer.Gold -= weaponArray[num].Cost;
            buyer.PickUp(weaponArray[num]);
            weaponArray[num] = RandomWeapon();
        }


        public string DisplayWeapon(int num)
        {
            return String.Format("{0}. Buy {1} ({2} Gold)", num + 1 ,weaponArray[num].Type, weaponArray[num].Cost);
        }
    }
}
