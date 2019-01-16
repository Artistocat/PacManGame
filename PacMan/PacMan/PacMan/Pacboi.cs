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
            //switch(counter)
            //{
            //    case 0:
            //        source.X = 35;
            //        source.Y = 0;
            //        break;
            //    case counter >= 4:
            //        source.X = 19;
            //        break;
            //    case 8:
            //        source.X = 3;
            //        counter = 0;
            //        break;
            //}
            if (counter > 4 && counter <= 8)
                source.X = 19;
            else if (counter > 8 && counter <= 12)
                source.X = 3;
            else if (counter > 12)
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
    }
}
