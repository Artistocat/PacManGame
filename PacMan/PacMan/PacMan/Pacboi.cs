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
            rec.X += (int) velocities.X;
            rec.Y += (int) velocities.Y;

        }
    }
}
