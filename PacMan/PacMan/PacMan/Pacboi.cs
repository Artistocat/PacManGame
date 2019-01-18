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
    public class Pacboi
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle source;
        public Vector2 velocities = new Vector2(0, 0);
        public Color colour;
        int counter = 0;
        public Pacboi()
        {

        }
        public Pacboi(Texture2D t, Rectangle r, Rectangle s, Vector2 vel)
        {
            tex = t;
            rec = r;
            source = s;
            velocities = vel;
            colour = Color.White;
        }
        public void Update()
        {

            if (velocities.Y > 0)
            {
                source.Y = 48;
            }
            if (velocities.Y < 0)
            {
                source.Y = 32;
            }
            if (velocities.X > 0)
            {
                source.Y = 0;
            }
            if (velocities.X < 0)
            {
                source.Y = 16;
            }

            if (counter > 5 && counter <= 10)
                source.X = 19;
            else if (counter > 10 && counter <= 15)
                source.X = 3;
            else if (counter > 15)
                counter = 0;
            else
            {
                source.X = 35;
                source.Y = 0;
            }

            rec.X += (int) velocities.X;
            rec.Y += (int) velocities.Y;
            counter++;
        }

        public void death()
        {
            counter = 0;
            source.Y = 0;

            if (counter > 2 && counter <= 4)
                source.X = 51;
            else if (counter > 4 && counter <= 6)
                source.X = 67;
            else if (counter > 6 && counter <= 8)
                source.X = 83;
            else if (counter > 8 && counter <= 10)
                source.X = 99;
        }
    }
}
