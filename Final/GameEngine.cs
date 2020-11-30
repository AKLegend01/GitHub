using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class GameEngine
    {
        private Map playerMap;
        private Hero player;
        private Tile empty;
        private Shop shop;

        private static readonly char Hero = 'H';
        private static readonly char Obsticle = 'X';
        private static readonly char Empty = '.';
        private static readonly char Goblin = 'G';

        public Map PlayerMap { get => playerMap; set => playerMap = value; }
        public Hero Player { get => player; set => player = value; }
        public Tile EmptyTile { get => empty; set => empty = value; }



        public GameEngine()
        {
            playerMap = new Map(12, 22, 12, 22, 10, 5, 5);
            playerMap.UpdateVision();
            Player = playerMap.h;
        }


        public bool MovePlayer(Character.Movements direction)
        {

            bool value = false;
            switch (direction)
            {
                case Character.Movements.Up:
                    if (Player.Vision[0].TileEnum == Tile.TileType.Gold || Player.Vision[0].TileEnum == Tile.TileType.Weapon)
                    {
                        for (int i = 0; i < playerMap.items.Length; i++)
                        {
                            if (playerMap.items[i].x == player.x && playerMap.items[i].y == player.y - 1)
                            {
                                player.PickUp(playerMap.items[i]);
                            }
                        }

                        Player.Move(Character.Movements.Up);
                        PlayerMap.mapArray[player.y, player.x] = player;
                        PlayerMap.mapArray[player.y + 1, player.x] = new EmptyTile(player.y + 1, player.x);
                        PlayerMap.UpdateVision();
                        value = true;
                    }
                    else
                    {
                        if (Player.Vision[0].TileEnum == Tile.TileType.Empty)
                        {
                            Player.Move(Character.Movements.Up);
                            PlayerMap.mapArray[player.y, player.x] = player;
                            PlayerMap.mapArray[player.y + 1, player.x] = new EmptyTile(player.y + 1, player.x);
                            PlayerMap.UpdateVision();
                            value = true;
                        }
                    }
                    break;



                case Character.Movements.Down:                    
                    if (Player.Vision[1].TileEnum == Tile.TileType.Gold || Player.Vision[1].TileEnum == Tile.TileType.Weapon)
                    {
                        for (int i = 0; i < playerMap.items.Length; i++)
                        {
                            if (playerMap.items[i].x == player.x && playerMap.items[i].y == player.y + 1)
                            {
                                player.PickUp(playerMap.items[i]);
                            }
                        }

                        Player.Move(Character.Movements.Down);
                        PlayerMap.mapArray[player.y, player.x] = player;
                        PlayerMap.mapArray[player.y - 1, player.x] = new EmptyTile(player.y - 1, player.x);
                        PlayerMap.UpdateVision();
                    }
                    else
                    {
                        if (Player.Vision[1].TileEnum == Tile.TileType.Empty)
                        {
                            Player.Move(Character.Movements.Down);
                            PlayerMap.mapArray[player.y, player.x] = player;
                            PlayerMap.mapArray[player.y - 1, player.x] = new EmptyTile(player.y - 1, player.x);
                            PlayerMap.UpdateVision();
                            value = true;
                        }
                    }
                    break;



                case Character.Movements.Left:
                    if (Player.Vision[2].TileEnum == Tile.TileType.Gold || Player.Vision[2].TileEnum == Tile.TileType.Weapon)
                    {
                        for (int i = 0; i < playerMap.items.Length; i++)
                        {
                            if (playerMap.items[i].x == player.x - 1 && playerMap.items[i].y == player.y)
                            {
                                player.PickUp(playerMap.items[i]);
                            }
                        }

                        Player.Move(Character.Movements.Left);
                        PlayerMap.mapArray[player.y, player.x] = player;
                        PlayerMap.mapArray[player.y, player.x + 1] = new EmptyTile(player.y, player.x + 1);
                        PlayerMap.UpdateVision();
                        value = true;
                    }
                    else
                    {
                        if (Player.Vision[2].TileEnum == Tile.TileType.Empty)
                        {
                            Player.Move(Character.Movements.Left);
                            PlayerMap.mapArray[player.y, player.x] = player;
                            PlayerMap.mapArray[player.y, player.x + 1] = new EmptyTile(player.y, player.x + 1);
                            PlayerMap.UpdateVision();
                            value = true;
                        }
                    }
                    break;



                case Character.Movements.Right:
                    if (Player.Vision[3].TileEnum == Tile.TileType.Gold || Player.Vision[3].TileEnum == Tile.TileType.Weapon)
                    {
                        for (int i = 0; i < playerMap.items.Length; i++)
                        {
                            if (playerMap.items[i].x == player.x + 1 && playerMap.items[i].y == player.y)
                            {
                                player.PickUp(playerMap.items[i]);
                            }
                        }

                        Player.Move(Character.Movements.Right);
                        PlayerMap.mapArray[player.y, player.x] = player;
                        PlayerMap.mapArray[player.y, player.x - 1] = new EmptyTile(player.y, player.x - 1);
                        PlayerMap.UpdateVision();
                        value = true;
                    }
                    else
                    {
                        if (Player.Vision[3].TileEnum == Tile.TileType.Empty)
                        {
                            Player.Move(Character.Movements.Right);
                            PlayerMap.mapArray[player.y, player.x] = player;
                            PlayerMap.mapArray[player.y, player.x - 1] = new EmptyTile(player.y, player.x - 1);
                            PlayerMap.UpdateVision();
                            value = true;
                        }
                    }
                    break;

            }

            if (direction == Character.Movements.None) return false;

            if (value == true) return true;
            else return false;

        }


        public override string ToString()
        {
            string MapLine = "";

            for (int y = 0; y < this.PlayerMap.mapHeight; y++)
            {
                for (int x = 0; x < this.PlayerMap.mapWidth; x++)
                {
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Empty)
                    {
                        MapLine += Convert.ToString(Empty);
                    }
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Goblin)
                    {
                        MapLine += Convert.ToString(Goblin);
                    }
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Mage)
                    {
                        MapLine += "M";
                    }
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Gold)
                    {
                        MapLine += "$";
                    }
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Hero)
                    {
                        MapLine += Convert.ToString(Hero);
                    }
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Obsticle)
                    {
                        MapLine += Convert.ToString(Obsticle);
                    }
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Weapon)
                    {
                        MapLine += this.PlayerMap.mapArray[y, x].ToString();
                    }
                    if (this.PlayerMap.mapArray[y, x].TileEnum == Tile.TileType.Leader)
                    {
                        MapLine += "L";
                    }
                }

                MapLine += "\n";

            }

            // Console.WriteLine(MapLine);
            // Console.WriteLine(this.Player.ToString());
            return MapLine;
        }





        public void EnemyAttack()
        {
            for (int i = 0; i < PlayerMap.enemy.Length; i++)
            {
                if (PlayerMap.enemy[i].TileEnum == Tile.TileType.Goblin)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        if (playerMap.enemy[i].Vision[m].TileEnum == Tile.TileType.Hero)
                        {
                            PlayerMap.enemy[i].Attack(player);
                        }
                    }
                }


                if (PlayerMap.enemy[i].TileEnum == Tile.TileType.Mage)
                {
                    for (int m = 0; m < 8; m++)
                    {
                        if (playerMap.enemy[i].Vision[m].TileEnum == Tile.TileType.Hero)
                        {
                            PlayerMap.enemy[i].Attack(player);
                        }

                        if (playerMap.enemy[i].Vision[m].TileEnum == Tile.TileType.Goblin)
                        {
                            for (int j = 0; j < playerMap.enemy.Length; j++)
                            {
                                if (PlayerMap.enemy[j].x == playerMap.enemy[i].Vision[m].x && PlayerMap.enemy[j].y == playerMap.enemy[i].Vision[m].y)
                                {
                                    playerMap.enemy[i].Attack(playerMap.enemy[j]);
                                }
                            }

                        }
                    }


                }


                if (PlayerMap.enemy[i].TileEnum == Tile.TileType.Leader)
                {
                    for (int m = 0; m < 4; m++)
                    {
                        if (playerMap.enemy[i].Vision[m].TileEnum == Tile.TileType.Hero)
                        {
                            PlayerMap.enemy[i].Attack(player);
                        }
                    }
                }
            }
        }





        public void MoveEnemies()
        {
            
            for (int i = 0; i < playerMap.enemy.Length; i++)
            {
                var enemy = PlayerMap.enemy[i];
                int X = enemy.x;
                int Y = enemy.y;
                if (enemy.TileEnum == Tile.TileType.Goblin || enemy.TileEnum == Tile.TileType.Leader)
                {
                    Character.Movements moves = enemy.ReturnMove();
                    switch (moves)
                    {
                        case Character.Movements.None:
                            break;
                        case Character.Movements.Up:
                            if (enemy.Vision[0].TileEnum == Tile.TileType.Gold || enemy.Vision[0].TileEnum == Tile.TileType.Weapon)
                            {
                                for (int j = 0; j < playerMap.items.Length; j++)
                                {
                                    if (playerMap.items[j].x == enemy.x && playerMap.items[j].y - 1 == enemy.y)
                                    {
                                        enemy.PickUp(playerMap.items[i]);
                                    }
                                }
                                enemy.y = Y - 1;
                                PlayerMap.mapArray[Y - 1, X] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            else
                            {
                                enemy.y = Y - 1;
                                PlayerMap.mapArray[Y - 1, X] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            break;
                        case Character.Movements.Down:
                            if (enemy.Vision[1].TileEnum == Tile.TileType.Gold || enemy.Vision[1].TileEnum == Tile.TileType.Weapon)
                            {
                                for (int j = 0; j < playerMap.items.Length; j++)
                                {
                                    if (playerMap.items[j].x == enemy.x && playerMap.items[j].y + 1 == enemy.y)
                                    {
                                        enemy.PickUp(playerMap.items[i]);
                                    }
                                }
                                enemy.y = Y + 1;
                                PlayerMap.mapArray[Y + 1, X] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            else
                            {
                                enemy.y = Y + 1;
                                PlayerMap.mapArray[Y + 1, X] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            break;
                        case Character.Movements.Left:
                            if (enemy.Vision[2].TileEnum == Tile.TileType.Gold || enemy.Vision[2].TileEnum == Tile.TileType.Weapon)
                            {
                                for (int j = 0; j < playerMap.items.Length; j++)
                                {
                                    if (playerMap.items[j].x == enemy.x - 1 && playerMap.items[j].y == enemy.y)
                                    {
                                        enemy.PickUp(playerMap.items[i]);
                                    }
                                }
                                enemy.x = X - 1;
                                PlayerMap.mapArray[Y, X - 1] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            else
                            {
                                enemy.x = X - 1;
                                PlayerMap.mapArray[Y, X - 1] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            break;
                        case Character.Movements.Right:
                            if (enemy.Vision[3].TileEnum == Tile.TileType.Gold || enemy.Vision[3].TileEnum == Tile.TileType.Weapon)
                            {
                                for (int j = 0; j < playerMap.items.Length; j++)
                                {
                                    if (playerMap.items[j].x == enemy.x + 1 && playerMap.items[j].y == enemy.y)
                                    {
                                        enemy.PickUp(playerMap.items[i]);
                                    }
                                }

                                enemy.x = X + 1;
                                PlayerMap.mapArray[Y, X + 1] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            else
                            {
                                enemy.x = X + 1;
                                PlayerMap.mapArray[Y, X + 1] = enemy;
                                PlayerMap.mapArray[Y, X] = new EmptyTile(Y, X);
                            }
                            break;
                    }
                    
                }
                
            }
            PlayerMap.UpdateVision();
        }

    }
}
