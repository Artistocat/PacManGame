﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PacMan
{
    public class Inky : Ghost
    {
        public Inky(int x, int y, Name n, Rectangle source) : base(x, y, n, source)
        {
        }

        protected override void UpdateTarget(Pacboi pacman, Ghost blinky, Board board)
        {
            int xFrontPac = pacman.rec.X / 24;
            int yFrontPac = pacman.rec.Y / 24;
            //if ()
        }

    }
}
