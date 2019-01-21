using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacMan
{
    public class Clyde : Ghost
    {
        public Clyde(int x, int y) : base(x, y, Name.Clyde, new Rectangle(4, 113, 14, 14))
        {
        }

        protected override void UpdateTarget(Pacboi pacman, Ghost blinky, Board board)
        {
            int squareX = (int)(pacman.rec.X) / 24;
            int squareY = (int)(pacman.rec.Y) / 24;
            double xDistFromPac = squareX - getSquareX();
            double yDistFromPac = squareY - getSquareY();
            double distFromPac = Math.Sqrt(xDistFromPac * xDistFromPac + yDistFromPac * yDistFromPac);

            if (distFromPac <= 8) Scatter();
            else
            {
                targetSquareLoc.X = squareX;
                targetSquareLoc.Y = squareY;
            }

            UpdateDirection(board);
        }
    }


}
