﻿using System; 
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PacMan
{
    public class MapSquares
    {
        int size = 24;
        Boolean[,] dead = new Boolean[28,36];
        public MapSquares()
        {
            for (int r = 0; r < 28; r++)
            {
                for(int c = 0; c < 36; c++)
                {
                    dead[r,c] = false;
                }
            }
            
        }
        public MapSquares(Boolean ded, Vector2 loc)
        {
            dead[(int)loc.X,(int)loc.Y] = ded;
        }
    }
}