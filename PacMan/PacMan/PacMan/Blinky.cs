using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PacMan;

namespace PacMan
{
    public class Blinky : Ghost
    {
        public Blinky(int x, int y, Name n, Rectangle source) : base(x, y, n, source)
        {
        }

        protected override void UpdateTarget(Pacboi pacman, Ghost Blinky, Board board)
        {
            targetSquareLoc.X = pacman.rec.X / 24;
            targetSquareLoc.Y = pacman.rec.Y / 24;

            double xDistOff = targetSquareLoc.X - x / 24;
            double yDistOff = targetSquareLoc.Y - y / 24;

            double?[] distsOff = new double?[4];
            Direction closestDir = Direction.Up;

            //Up
            distsOff[0] = getDistOff(xDistOff, yDistOff - 1, board);

            //Left
            distsOff[1] = getDistOff(xDistOff - 1, yDistOff, board);

            //Down
            distsOff[2] = getDistOff(xDistOff, yDistOff + 1, board);

            //Right
            distsOff[3] = getDistOff(xDistOff + 1, yDistOff, board);
            
            for (int i = 1; i < distsOff.Length; i++)
            {
                if (distsOff[i] != null && distsOff[i] > distsOff[(int)closestDir])
                {
                    closestDir = (Direction)i;
                }
            }
            dir = closestDir;
            Console.WriteLine("We got here");
            UpdateVelocity();
        }

        protected new void Scatter()
        {
            scatter = true;
            targetSquareLoc.X = 26; //NOT A TYPO!! YES IT IS ACTUALLY 1 OFF THE EDGE. IDK HOW IT WORKS BUT IT DOES
            targetSquareLoc.Y = 0;
        }

    }
}
