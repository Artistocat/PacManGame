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

namespace PacMan
{
    public class Board
    {
        public Texture2D screen;
        public int tile;
        public Boolean powerPellet;
        public Boolean pellet;
        public Boolean dead;
        public MapSquares[,] space = new MapSquares[28, 36];
        public Board()
        {

        }
        public Board(int t, Texture2D s)
        {
            tile = t;
            screen = s;
            switch(t)
            {
                case 0:
                    dead = true;
                    pellet = false;
                    powerPellet = false;
                    break;
                case 1:
                    dead = false;
                    pellet = true;
                    powerPellet = false;
                    break;
                case 2:
                    dead = false;
                    pellet = false;
                    powerPellet = true;
                    break;
                case 3:
                    dead = false;
                    pellet = false;
                    powerPellet = false;
                    break;
            }
        }
    }
}

