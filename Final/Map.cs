﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    [Serializable]
    class Map
    {
        private Tile[,] MapArray;
        private Hero H;
        private Enemy[] Enemy;
        private int MapWidth, MapHeight;
        [NonSerialized]
        private Random r = new Random();
        private Item[] Items;
        int count = 0;

        public Tile[,] mapArray { get => MapArray; set => MapArray = value; }
        public int mapWidth { get => MapWidth; set => MapWidth = value; }
        public int mapHeight { get => MapHeight; set => MapHeight = value; }
        public Hero h { get => H; set => H = value; }
        public Enemy[] enemy { get => Enemy; set => Enemy = value; }
        internal Item[] items { get => Items; set => Items = value; }

        public Map(int minW, int maxW, int minH, int maxH, int numEnemies, int goldDrop, int weaponDrop)
        {
            MapWidth = r.Next(minW, maxW + 1);
            MapHeight = r.Next(minH, maxH + 1);
            this.MapArray = new Tile[MapHeight, MapWidth];
            this.Enemy = new Enemy[numEnemies];

            this.Items = new Item[goldDrop+weaponDrop];



            for (int x = 0; x != MapWidth; x++)
            {
                for (int y = 0; y != MapHeight; y++)
                {
                    if (x == 0 || y == 0 || x == MapWidth - 1 || y == MapHeight - 1)
                    {
                        MapArray[y, x] = new Obstacle(y, x);
                        MapArray[y, x].TileEnum = Tile.TileType.Obsticle;
                    }
                    else
                    {
                        MapArray[y, x] = new EmptyTile(y, x);
                        MapArray[y, x].TileEnum = Tile.TileType.Empty;
                    }

                }
            }




            H = (Hero)Create(Tile.TileType.Hero);

            count = 1;
            int rand;
            enemy[0] = (Enemy)Create(Tile.TileType.Leader);
            while (count != numEnemies)
            {
                rand = r.Next(1, 12);
                if (rand == 1)
                {
                    enemy[count] = (Enemy)Create(Tile.TileType.Leader);
                }
                else if (rand > 1 && rand <= 4)
                {
                    enemy[count] = (Enemy)Create(Tile.TileType.Mage);
                }
                else 
                {
                    enemy[count] = (Enemy)Create(Tile.TileType.Goblin);
                }
                count++;
            }

            count = 0;
            while (count < goldDrop)
            {               
                items[count] = (Item)Create(Tile.TileType.Gold);
                count++;
            }

            while (count < weaponDrop + goldDrop)
            {
                int weapon = r.Next(0, 4);
                switch(weapon)
                {
                    case 0:
                        items[count] = (MeleeWeapon)Create(Tile.TileType.Weapon, 0);
                        break;
                    case 1:
                        items[count] = (MeleeWeapon)Create(Tile.TileType.Weapon, 1);
                        break;
                    case 2:
                        items[count] = (RangedWeapon)Create(Tile.TileType.Weapon, 2);
                        break;
                    case 3:
                        items[count] = (RangedWeapon)Create(Tile.TileType.Weapon, 3);
                        break;
                }
                
                count++;
            }

        }





        private Tile Create(Tile.TileType type, int weaponType = 0)
        {
            int PosX = r.Next(1, MapWidth);
            int PosY = r.Next(1, MapHeight);
            Tile tile = null;

            while (MapArray[PosY, PosX].TileEnum != Tile.TileType.Empty)
            {
                PosX = r.Next(1, mapWidth);
                PosY = r.Next(1, MapHeight);
            }


            switch (type)  // "type" ---> Enter
            {
                case Tile.TileType.Hero:
                    tile = new Hero(PosX, PosY, 100);
                    break;
                case Tile.TileType.Mage:
                    tile = new Mage(PosX, PosY);
                    break;
                case Tile.TileType.Goblin:
                    tile = new Goblin(PosX, PosY);
                    break;
                case Tile.TileType.Gold:
                    tile = new Gold(PosX, PosY);
                    break;   
                case Tile.TileType.Weapon:
                    switch (weaponType)
                    {
                        case 0: 
                            tile = new MeleeWeapon(MeleeWeapon.Types.Dagger, PosX, PosY);
                            break; 
                        case 1:
                            tile = new MeleeWeapon(MeleeWeapon.Types.LongSword, PosX, PosY);
                            break;
                        case 2:
                            tile = new RangedWeapon(RangedWeapon.Types.Rifle, PosX, PosY);
                            break;
                        case 3:
                            tile = new RangedWeapon(RangedWeapon.Types.Longbow, PosX, PosY);
                            break;

                    }
                    break;
                case Tile.TileType.Leader:
                    Leader leader = new Leader(PosX, PosY);
                    leader.LeaderTarget = H;
                    tile = leader;
                    break;
                case Tile.TileType.Empty:
                    throw new NotImplementedException();
                default: return null;
            }

            MapArray[tile.y, tile.x] = tile;
            return tile;
        }





        public void UpdateVision()
        {
            // up, down, left, righ,,, up-left, up-right, down-right, down-left
            H.Vision[0] = MapArray[H.y - 1, H.x];
            H.Vision[1] = MapArray[H.y + 1, H.x];
            H.Vision[2] = MapArray[H.y, H.x - 1];
            H.Vision[3] = MapArray[H.y, H.x + 1];

            H.Vision[4] = MapArray[H.y - 1, H.x - 1];
            H.Vision[5] = MapArray[H.y - 1, H.x + 1];
            H.Vision[6] = MapArray[H.y + 1, H.x + 1];
            H.Vision[7] = MapArray[H.y + 1, H.x - 1];

            int count = 0;
            while (count != enemy.Length)
            {
                // up, down, left, righ,,, up-left, up-right, down-right, down-left
                if (enemy[count] != null)
                {
                    enemy[count].Vision[0] = MapArray[enemy[count].y - 1, enemy[count].x];
                    enemy[count].Vision[1] = MapArray[enemy[count].y + 1, enemy[count].x];
                    enemy[count].Vision[2] = MapArray[enemy[count].y, enemy[count].x - 1];
                    enemy[count].Vision[3] = MapArray[enemy[count].y, enemy[count].x + 1];

                    enemy[count].Vision[4] = MapArray[enemy[count].y - 1, enemy[count].x - 1];
                    enemy[count].Vision[5] = MapArray[enemy[count].y - 1, enemy[count].x + 1];
                    enemy[count].Vision[6] = MapArray[enemy[count].y + 1, enemy[count].x + 1];
                    enemy[count].Vision[7] = MapArray[enemy[count].y + 1, enemy[count].x - 1];                   
                }
                count++;
            }
        }




        public Item GetItemPosition(int x, int y)
        {
            Item array;
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].x == x && Items[i].y == y)
                {
                    array = Items[i];
                    Items[i] = null;
                    return array;
                }
            }
            return null;
        }
    }
}
