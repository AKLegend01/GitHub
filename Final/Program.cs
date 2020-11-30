using System;
using System.IO;

namespace Final
{
    class Program
    {
        
        static void Main(string[] args)
        {
            GameEngine game = new GameEngine();

            
            //if (File.Exists("saves/save.game"))
            //{
            //    Console.WriteLine("would you like to load previous game? (Y/N)");
            //    string ans = Console.ReadLine();
            //    if (ans.ToUpper() == "Y")
            //    {
            //        game.Load();
            //    }
            //}
            
            bool exit = false;
            while (exit == false) // exit game
            {
                string Playkey = DrawMap(game);
                Console.Beep();
                bool valid = false;
                bool canMove = true;

                switch (Playkey.ToLower())
                {
                    case "m":

                        string movekey = DrawMapMove(game);
                        while (valid == false && canMove == true)  //movement logic
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
                                    game.Save();
                                    Environment.Exit(0);
                                    break;
                                default:
                                    Console.Beep();
                                    Console.WriteLine("Invalid Move. Please move Hero with W/A/S/D. (Up, Left, Down, Right)\nPress x to exit game.");
                                    movekey = Console.ReadLine();
                                    break;
                            }

                        }

                        canMove = false;
                        valid = false;
                        bool Enemy_In_Range = false;
                        string afterMoveKey;

                        for (int i = 0; i < 4; i++)
                        {
                            if (game.Player.Vision[i].TileEnum == Tile.TileType.Goblin || game.Player.Vision[i].TileEnum == Tile.TileType.Mage || game.Player.Vision[i].TileEnum == Tile.TileType.Leader)
                            {
                                Enemy_In_Range = true;
                            }
                        }

                        if (Enemy_In_Range == true)
                        {
                            afterMoveKey = DrawMapAfterMove(game);
                            if (afterMoveKey.ToLower() == "o")   // SHOP
                            {
                                game.shop = new Shop(game.Player);
                                while (valid == false)
                                {
                                    string chose = "";
                                    chose = DrawMapShop(game);

                                    if (chose == "c")
                                    {
                                        valid = true;
                                        break;
                                    }
                                    else
                                    {
                                        int chosen = -1;
                                        int.TryParse(chose, out chosen);
                                        if (chosen == -1)
                                        {
                                            Console.WriteLine("Please enter a valid numerical key. Press Enter to continue or letter c to close shop.");
                                            Console.ReadLine();
                                        }
                                        else if (chosen > 3)
                                        {
                                            Console.WriteLine("Please enter a valid numerical key (1 - 3). Press Enter to continue or letter c to close shop");
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            if (game.shop.CanBuy(chosen - 1) == true)
                                            {
                                                game.shop.Buy(chosen - 1);
                                                valid = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cant offrd that weapon. Press Enter to continue.");
                                                Console.ReadLine();
                                                valid = true;
                                            }
                                        }
                                    }
                                }
                            }
                            else  // Attack
                            {
                                while (valid == false)
                                {
                                    int key = -1;
                                    int.TryParse(afterMoveKey, out key);
                                    if (key > 0 && key <= game.PlayerMap.enemy.Length)
                                    {
                                        var enemy = game.PlayerMap.enemy[key - 1];
                                        if(enemy != null)
                                        {
                                            bool inRange = game.Player.CheckRange(enemy);
                                            if(inRange == true)
                                            {
                                                game.Player.Attack(enemy);

                                                Console.WriteLine("Successful Attack");
                                                Console.WriteLine("Press Enter to continue");
                                                canMove = true;
                                                if(enemy.IsDead() == true)
                                                {
                                                    game.PlayerMap.mapArray[enemy.y, enemy.x] = new EmptyTile(enemy.y, enemy.x);
                                                    game.PlayerMap.enemy[key - 1] = null;
                                                    game.PlayerMap.UpdateVision();
                                                }
                                                Console.ReadLine();
                                                valid = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Enemy is out of range. Please select a valid target.");
                                                afterMoveKey = Console.ReadLine();
                                            }
                                        }
                                    }
                                    else if (key == 0)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The enemy yov have selected does not exsist or is dead. Please select a valid target");
                                        afterMoveKey = Console.ReadLine();
                                    }
                                }

                            }
                            
                           
                        }
                        else
                        {
                            canMove = true;
                        }

                        break;









                    case "a":
                        string AttackKey = DrawMapAttack(game);
                        valid = false;

                        while(valid == false)
                        {
                            int key = -1;
                            int.TryParse(AttackKey, out key);
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
                                        Console.WriteLine("Press Enter to continue");
                                        canMove = false;
                                        if (enemy.IsDead() == true)
                                        {
                                            game.PlayerMap.mapArray[enemy.y, enemy.x] = new EmptyTile(enemy.y, enemy.x);
                                            game.PlayerMap.enemy[key - 1] = null;
                                            game.PlayerMap.UpdateVision();
                                        }
                                        Console.ReadLine();
                                        valid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Enemy is out of range. Please select a valid target.");
                                        AttackKey = Console.ReadLine();
                                    }
                                }
                            }
                            else if (key == 0)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("The enemy yov have selected does not exsist or is dead. Please select a valid target");
                                AttackKey = Console.ReadLine();
                            }
                        }
                        break;









                    case "s":

                        game.shop = new Shop(game.Player);
                        while (valid == false)
                        {                          
                            string chose = "";
                            chose = DrawMapShop(game);

                            if (chose == "c")
                            {
                                valid = true;
                                break;
                            }
                            else
                            {
                                int chosen = -1;
                                int.TryParse(chose, out chosen);
                                if (chosen == -1)
                                {
                                    Console.WriteLine("Please enter a valid numerical key. Press Enter to continue or letter c to close shop.");
                                    Console.ReadLine();
                                }
                                else if (chosen > 3)
                                {
                                    Console.WriteLine("Please enter a valid numerical key (1 - 3). Press Enter to continue or letter c to close shop");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    if (game.shop.CanBuy(chosen - 1) == true)
                                    {
                                        game.shop.Buy(chosen - 1);
                                        valid = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cant offrd that weapon. Press Enter to continue.");
                                        Console.ReadLine();
                                        valid = true;
                                    }
                                }
                            }

                            
                        }
                        break;



                    default:
                        Console.WriteLine("Please select between Move(M), Shop(S) or Attack(A) if the option is avaliable. Press enter to continue");
                        Console.ReadLine();
                        break;
             
                }
                game.MoveEnemies();
                game.EnemyAttack();
                game.PlayerMap.UpdateVision();
            }


                //bool EnemyInRange = false;
                
                //for (int i = 0; i < 4; i++)
                //{
                //    if (game.Player.Vision[i].TileEnum == Tile.TileType.Goblin || game.Player.Vision[i].TileEnum == Tile.TileType.Mage || game.Player.Vision[i].TileEnum == Tile.TileType.Leader)
                //    {
                //        EnemyInRange = true;
                //    }
                //}

                //if (EnemyInRange == true)
                //{
                //    movekey = DrawMapAgain(game);
                //    valid = false;

                //    while (valid == false)  // attack logic
                //    {
                //        int key = -1;
                //        int.TryParse(movekey, out key);
                //        if (key > 0 && key <= game.PlayerMap.enemy.Length)
                //        {
                //            movekey = null;
                //            var enemy = game.PlayerMap.enemy[key - 1];
                //            if (enemy != null)
                //            {
                //                bool inRange = game.Player.CheckRange(enemy);
                //                if (inRange == true)
                //                {
                //                    game.Player.Attack(enemy);

                //                    Console.WriteLine("Successful Attack");
                //                    Console.WriteLine("Press enter to continue");
                //                    if (enemy.IsDead())
                //                    {
                //                        game.PlayerMap.mapArray[enemy.y, enemy.x] = new EmptyTile(enemy.x, enemy.y);
                //                        game.PlayerMap.enemy[key - 1] = null;
                //                        game.PlayerMap.UpdateVision();
                //                    }
                //                    Console.ReadLine();
                //                    valid = true;
                //                }
                //                else
                //                {
                //                    Console.WriteLine("Enemy is out of range. please select a valid target");
                //                    movekey = Console.ReadLine();
                //                }
                //            }

                //        }
                //        else if (key == 0)
                //        {
                //            break;
                //        }
                //        else
                //        {
                //            Console.WriteLine("the enemy you selected does not exsist or is dead. Please select a valid target.");
                //            movekey = Console.ReadLine();
                //        }

                //        if (movekey != null)
                //        {
                //            Console.WriteLine("Invalid command. Please use select a valid enemy");
                //            movekey = Console.ReadLine();
                //        }
                //    }
                //}
                //game.MoveEnemies();
                //game.EnemyAttack();
                //game.PlayerMap.UpdateVision();
            


            static string DrawMap(GameEngine game) // Movement
            {
                Console.Clear();
                Console.WriteLine(game.Player.ToString());
                Console.WriteLine();
                Console.WriteLine(game.ToString());
                Console.WriteLine();

                for (int i = 0; i < game.PlayerMap.enemy.Length; i++)
                {
                    if (game.PlayerMap.enemy[i] == null) Console.WriteLine((i + 1) + ". Dead");
                    else Console.WriteLine(String.Format("{0}. {1}", i + 1, game.PlayerMap.enemy[i].ToString()));
                }
                Console.WriteLine();

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
                    Console.WriteLine("Would you like to Move(M), Attack(A) or go to Shop(S)");
                }
                Console.WriteLine("Would you like to Move(M) or go to Shop(S)");
                //move or attack                

                return Console.ReadLine();
            }



            static string DrawMapMove(GameEngine game) // Movement
            {            
                Console.Clear();
                Console.WriteLine(game.Player.ToString());
                Console.WriteLine();
                Console.WriteLine(game.ToString());
                Console.WriteLine();

                for (int i = 0; i < game.PlayerMap.enemy.Length; i++)
                {
                    if (game.PlayerMap.enemy[i] == null) Console.WriteLine((i + 1) + ". Dead");
                    else Console.WriteLine(String.Format("{0}. {1}", i + 1, game.PlayerMap.enemy[i].ToString()));
                }
                Console.WriteLine();
                Console.WriteLine("Please move Hero with W/A/S/D. (Up, Left, Down, Right).\nPress X to save and exit game.");
                //move or attack                
                
                return Console.ReadLine();
            }



            static string DrawMapAfterMove(GameEngine game) // Movement
            {
                Console.Clear();
                Console.WriteLine(game.Player.ToString());
                Console.WriteLine();
                Console.WriteLine(game.ToString());
                Console.WriteLine();

                for (int i = 0; i < game.PlayerMap.enemy.Length; i++)
                {
                    if (game.PlayerMap.enemy[i] == null) Console.WriteLine((i + 1) + ". Dead");
                    else Console.WriteLine(String.Format("{0}. {1}", i + 1, game.PlayerMap.enemy[i].ToString()));
                }
                Console.WriteLine();
                Console.WriteLine("There is an enemy in your attack range. Press the corresponding numeric key to select the enemy you wish to attack!\nPress 0 if you do not wish to attack.\nPress letter O to open shop");
                //move or attack                

                return Console.ReadLine();
            }



            static string DrawMapAttack(GameEngine game)  // Attack action
            {
                Console.Clear();
                Console.WriteLine(game.Player.ToString());
                Console.WriteLine();
                Console.WriteLine(game.ToString());
                Console.WriteLine();

                for (int i = 0; i < game.PlayerMap.enemy.Length; i++)
                {
                    if (game.PlayerMap.enemy[i] == null) Console.WriteLine((i + 1) + ". Dead");
                    else Console.WriteLine(String.Format("{0}. {1}", i + 1, game.PlayerMap.enemy[i].ToString()));
                }

                Console.WriteLine();
                Console.WriteLine("There is an enemy in your attack range. Press the corresponding numeric key to select the enemy you wish to attack!\n Press 0 if you do not wish to attack.");              
                
                return Console.ReadLine();
            }



            static string DrawMapShop(GameEngine game)  // Attack action
            {
                Console.Clear();

                string weaponlist = "";

                for (int i = 0; i < 3; i++)
                {
                    weaponlist += game.shop.DisplayWeapon(i) + "\n";
                }

                Console.WriteLine(game.Player.ToString());
                Console.WriteLine();
                Console.WriteLine("Select a weapon youd like to buy buy entering the corresponding number, or press letter c to close shop");
                Console.WriteLine();
                Console.WriteLine(weaponlist);

                return Console.ReadLine();
            }
        }
    }
}
