using System;
using Microsoft.Xna.Framework;

namespace PacMan
{
    public class Blinky : Ghost
    {
        public Blinky(int x, int y) : base(x, y, Name.Blinky, new Rectangle(4, 65, 14, 14))
        {
            dir = Direction.Left;
        }

        protected override void UpdateTarget(Pacboi pacman, Ghost Blinky, Board board)
        {
            if (!scatter)
            {
                targetSquareLoc.X = pacman.rec.X / 24;
                targetSquareLoc.Y = pacman.rec.Y / 24;
            }
            UpdateDirection(board);
        }

        public override void Scatter()
        {
            scatter = true;
            targetSquareLoc.X = 26; //NOT A TYPO!! YES IT IS ACTUALLY 1 OFF THE EDGE. IDK HOW IT WORKS BUT IT DOES
            targetSquareLoc.Y = 0;
        }

    }
}
