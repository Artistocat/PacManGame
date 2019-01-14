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
        private float x;
        private float y;
        private Rectangle rect;
        private int name;
        private Vector2 velocity;
        private Vector2 targetSquareLoc;
        private int counter;

        //88 * 3 pixels per second
        //each second is 60 fps
        //4.4 pixels per frame
        const float speed = (float)4.4;
        const int Inky = 0, Blinky = 1, Pinky = 2, Clyde = 3;
        

        public Ghost(int x, int y, int name)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = rect.Height = 45;
            this.name = name;
            velocity = new Vector2(0, 0);
        }

        public void Update()
        {
            counter++;
            x += velocity.X;
            y += velocity.Y;
            rect.X = (int)x;
            rect.Y = (int)y;
            if (counter == 24)
            {
                counter = 0;
                UpdateTarget();
            }
        }

        private void UpdateTarget()
        {

        }


        
    }
}
