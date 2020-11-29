using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Map
    {
        private Tile[,] MapArray;
        private Hero H;
        private Enemy[] Enemy;
        private int MapWidth, MapHeight;
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

            count = 0;
            int rand;
            while (count != numEnemies)
            {
                rand = r.Next(1, 4);
                if (rand == 1) enemy[count] = (Enemy)Create(Tile.TileType.Mage);
                if (rand == 2) enemy[count] = (Enemy)Create(Tile.TileType.Goblin);
                if (rand == 3) enemy[count] = (Enemy)Create(Tile.TileType.Leader);
                count++;
            }

            count = 0;
            while (count < goldDrop)
            {               
                items[count] = (Item)Create(Tile.TileType.Gold);
                count++;
            }

            while (count < weaponDrop)
            {
                items[count] = (Item)Create(Tile.TileType.Weapon);
                count++;
            }

        }





        private Tile Create(Tile.TileType type)
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
                    tile = new Weapon(PosX, PosY, 'y');
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
