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
        public int counter = 0;
        public int lives = 26;

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

        public void respawn()
        {
            rec.X = 312;
            rec.Y = 615;
        }

        public void death()
        {
            if (counter < 10)
                source.X = 35;
            else if (counter < 20)
                source.X = 35 + 16;
            else if (counter < 30)
                source.X = 35 + (16 * 2);
            else if (counter < 40)
                source.X = 35 + (16 * 3);
            else if (counter < 50)
                source.X = 35 + (16 * 4);
            else if (counter < 60)
                source.X = 35 + (16 * 5);
            else if (counter < 70)
                source.X = 35 + (16 * 6);
            else if (counter < 80)
                source.X = 35 + (16 * 7);
            else if (counter < 90)
                source.X = 35 + (16 * 8);
            else if (counter < 100)
                source.X = 35 + (16 * 9);
            else if (counter < 110)
                source.X = 35 + (16 * 10);
            else if (counter < 120)
            {
                source.X = 35 + (16 * 11);
                source.Y = 3;
            }
            counter++;
        }
    }
}
