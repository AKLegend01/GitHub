using System;

namespace Final
{
    class Program
    {
        static void Main(string[] args)
        {
            
            GameEngine game = new GameEngine();
            bool exit = false;           
            while (exit == false) // exit game
            {
                string movekey = DrawMap(game);
                Console.Beep();
                bool valid = false;
                while (valid == false)  //movement logic
                {
                    switch (movekey.ToLower())
                    {
                        case "w":
                            game.MovePlayer(Character.Movements.Up);
                            valid = true;
                            break;
                        case "a":
                            game.MovePlayer(Character.Movements.Left);
                            valid = true;
                            break;
                        case "s":
                            game.MovePlayer(Character.Movements.Down);
                            valid = true;
                            break;
                        case "d":
                            game.MovePlayer(Character.Movements.Right);
                            valid = true;
                            break;
                        case "x":
                            exit = true;
                            valid = true;
                            break;
                        default:
                            Console.Beep();
                            Console.WriteLine("Invalid Move. Please move Hero with W/A/S/D. (Up, Left, Down, Right)\nPress x to exit game.");
                            movekey = Console.ReadLine();

                            break;

                           
                    }

                   
                }

                bool EnemyInRange = false;
                
                for (int i = 0; i < 4; i++)
                {
                    if (game.Player.Vision[i].TileEnum == Tile.TileType.Goblin || game.Player.Vision[i].TileEnum == Tile.TileType.Mage || game.Player.Vision[i].TileEnum == Tile.TileType.Leader)
                    {
                        EnemyInRange = true;
                    }
                }

                if (EnemyInRange == true)
                {
                    movekey = DrawMapAgain(game);
                    valid = false;

                    while (valid == false)  // attack logic
                    {
                        int key = -1;
                        int.TryParse(movekey, out key);
                        if (key > 0 && key <= game.PlayerMap.enemy.Length)
                        {
                            var enemy = game.PlayerMap.enemy[key - 1];
                            if (enemy != null)
                            {
                                bool inRange = game.Player.CheckRange(enemy);
                                if (inRange == true)
                                {
                                    game.Player.Attack(enemy);

                                    Console.WriteLine("Successful Attack");
                                    Console.WriteLine("Press enter to continue");
                                    if (enemy.IsDead())
                                    {
                                        game.PlayerMap.mapArray[enemy.y, enemy.x] = new EmptyTile(enemy.x, enemy.y);
                                        game.PlayerMap.enemy[key - 1] = null;
                                        game.PlayerMap.UpdateVision();
                                    }
                                    Console.ReadLine();
                                    valid = true;
                                }
                                else
                                {
                                    Console.WriteLine("Enemy is out of range. please select a valid target");
                                    movekey = Console.ReadLine();
                                }
                            }

                        }
                        else if (key == 0)
                        {

                        }
                        else
                        {
                            Console.WriteLine("the enemy you selected does not exsist or is dead. Please select a valid target.");
                            movekey = Console.ReadLine();
                        }
                    }
                }
                game.MoveEnemies();
                game.EnemyAttack();
            }


            static string DrawMap(GameEngine game) // Movement
            {            
                Console.Clear();
                Console.WriteLine(game.Player.ToString());
                Console.WriteLine();
                Console.WriteLine(game.ToString());
                Console.WriteLine();

                Console.WriteLine("Please move Hero with W/A/S/D. (Up, Left, Down, Right).\nPress x to exit game.");
                //move or attack                
                for (int i = 0; i < game.PlayerMap.enemy.Length; i++)
                {
                    if (game.PlayerMap.enemy[i] == null) Console.WriteLine((i+1) + ". Dead");
                    else Console.WriteLine(String.Format("{0}. {1}", i+1, game.PlayerMap.enemy[i].ToString()));
                }
                return Console.ReadLine();
            }


            static string DrawMapAgain(GameEngine game)  // Attack action
            {
                Console.Clear();
                Console.WriteLine(game.Player.ToString());
                Console.WriteLine();
                Console.WriteLine(game.ToString());
                Console.WriteLine();

                Console.WriteLine("There is an enemy in your attack range. Press the corresponding numeric key to select the enemy you wish to attack!\n Press 0 if you do not wish to attack.");              
                for (int i = 0; i < game.PlayerMap.enemy.Length; i++)
                {
                    if (game.PlayerMap.enemy[i] == null) Console.WriteLine((i + 1) + ". Dead");
                    else Console.WriteLine(String.Format("{0}. {1}", i + 1, game.PlayerMap.enemy[i].ToString()));
                }
                return Console.ReadLine();
            }

        }
    }
}
