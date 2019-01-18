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
    public class Board
    {
        public Texture2D screen;
        //public int tile;
        //public Boolean powerPellet;
        //public Boolean pellet;
        //public Boolean dead;
        public MapSquares[,] space = new MapSquares[28, 36];
        public Board()
        {

        }
        public Board(Texture2D s, MapSquares[,] b)
        {
            screen = s;
            for (int r = 0; r < 28; r++)
            {
                for (int c = 0; c < 36; c++)
                {
                    space[r, c] = b[r,c];
                }
            }
        }
    }
}

