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
    class Pellet
    {
        Boolean isPowerPellet;
        Boolean isEaten;
        double x, y;
        int pelletNum;
        Rectangle rect;
        Texture2D texture;


        /*Notes:
         * pellet size is 6x6
         * powerpellet size is 12x12
         * 
         */


        public Pellet(double a, double b, int n)
        {
            x = a;
            y = b;
            pelletNum = n;
            rect = new Rectangle((int)-1000, (int)-1000, 6, 6);

        }

        public void placePellet()
        {
            rect.X = (int)x;
            rect.Y = (int)y;
        }

        public int MethodExample()
        {
            p = new Pellet(this.x, this.y);
            return -1;
        }


    }
}
