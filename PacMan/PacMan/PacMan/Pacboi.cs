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
    class Pacboi
    {
        public Texture2D puck;
        public Rectangle rec;
        public Rectangle source;
        public Vector2 velocities = new Vector2(0, 0);
        public Color colour;
        public Pacboi()
        {

        }
        public Pacboi(Texture2D t, Rectangle r, Rectangle s, Vector2 vel)
        {
            puck = t;
            rec = r;
            source = s;
            velocities = vel;
            colour = Color.White;
        }
    }
}
