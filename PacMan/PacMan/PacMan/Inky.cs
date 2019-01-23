using Microsoft.Xna.Framework;
using System;

namespace PacMan
{
    public class Inky : Ghost
    {
        public Inky(int x, int y) : base(x, y, Name.Inky, new Rectangle(4, 97, 14, 14))
        {
            dir = Direction.Up;
        }

        //TODO
        protected override void UpdateTarget(Pacboi pacman, Ghost blinky, Board board)
        {
            int xFrontPac = pacman.rec.X / 24;
            int yFrontPac = pacman.rec.Y / 24;
            Vector2 pacV = pacman.velocities;
            if (pacV.X > 0) xFrontPac += 2;
            if (pacV.X < 0) xFrontPac -= 2;
            if (pacV.Y > 0) yFrontPac += 2;
            if (pacV.Y < 0) yFrontPac -= 2;

            int xBlinkyDistFrontPac = xFrontPac - blinky.getRect().X / 24;
            int yBlinkyDistFrontPac = yFrontPac - blinky.getRect().Y / 24;
            targetSquareLoc.X = xFrontPac - xBlinkyDistFrontPac;
            targetSquareLoc.Y = yFrontPac - yBlinkyDistFrontPac;

            UpdateDirection(board);
        }

        protected new void Scatter()
        {
            //scatter = true;
            //targetSquareLoc.X = 27;
            //targetSquareLoc.Y = 35;
        }

    }
}
