using System;
using System.Collections.Generic;
using System.Text;

namespace Final
{
    class Leader : Enemy
    {
        private Tile leaderTarget;
        public Tile LeaderTarget { get => leaderTarget; set => leaderTarget = value; }


        public Leader(int x, int y) : base(x, y, 2, 20, 'L')
        {
            this.TileEnum = TileType.Leader;
            this.Gold = 2;
            this.PickedUpWeapon = new MeleeWeapon(MeleeWeapon.Types.LongSword);
        }

        public override Movements ReturnMove(Movements Move)
        {
            int X = leaderTarget.x;
            int Y = leaderTarget.y;
            int diffX, diffY;
            int move = 0;
            bool foundMove = false;

            diffX = X - this.x;
            if (diffX < 0) diffX *= -1;
            diffY = Y - this.y;
            if (diffY < 0) diffY *= -1;

            if (diffX < diffY)
            {
                if (Y < this.y)
                {
                    if (this.Vision[0].TileEnum == TileType.Empty)
                    {
                        move = 0;
                        foundMove = true;
                    }
                }
                else
                {
                    if (this.Vision[1].TileEnum == TileType.Empty)
                    {
                        move = 1;
                        foundMove = true;
                    }
                }
            }
            else
            {
                if (X < this.x)
                {
                    if (this.Vision[2].TileEnum == TileType.Empty)
                    {
                        move = 2;
                        foundMove = true;
                    }
                }
                else
                {
                    if (this.Vision[3].TileEnum == TileType.Empty)
                    {
                        move = 3;
                        foundMove = true;
                    }
                }
            }


            while(foundMove == false)
            {
                move = this.r.Next(1, 5);
                if (this.Vision[move].TileEnum != TileType.Empty) move = this.r.Next(1, 5);
                else foundMove = true;
            }

            return (Movements)move;
            
        }


    }
}
