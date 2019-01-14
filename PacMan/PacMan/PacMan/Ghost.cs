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
    public class Ghost
    {
        private Rectangle rect;
        private int name;
        private Vector2 velocity;
        const int Inky = 0, Blinky = 1, Pinky = 2, Clyde = 3;

        public Ghost(int x, int y, int name)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = 
            this.name = name;
            velocity = new Vector2(0, 0);
        }
        
    }
}
