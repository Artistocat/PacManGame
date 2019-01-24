using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacMan
{
    public class Pinky : Ghost
    {
        public Pinky(int x, int y) : base(x, y, Name.Pinky, new Rectangle(4, 81, 14, 14))
        {
            dir = Direction.Down;
        }

        protected override void UpdateTarget(Pacboi pacman, Ghost blinky, Board board)
        {
            int distFrontPac = 4;
            int squareX = pacman.rec.X / 24;
            int squareY = pacman.rec.Y / 24;
            Vector2 pacV = pacman.velocities;
            if (pacV.X > 0) squareX += distFrontPac;
            if (pacV.X < 0) squareX -= distFrontPac;
            if (pacV.Y > 0) squareY += distFrontPac;
            if (pacV.Y < 0) squareY -= distFrontPac;

            targetSquareLoc.X = squareX;
            targetSquareLoc.Y = squareY;

            UpdateDirection(board);
        }

        public override void Scatter()
        {
            scatter = true;
            targetSquareLoc.X = 1; //NOT A TYPO!!! YES IT IS ACTUALLY 1, NOT 0
            targetSquareLoc.Y = 0;
        }
    }
}
