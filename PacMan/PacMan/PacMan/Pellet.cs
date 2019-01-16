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
   public class Pellet
    {
        Boolean isPowerPellet;
        Boolean isEaten;
        double x, y;
        Pellet p;
        int pelletNum;
        public Rectangle rect;
        public Texture2D texture;


        /*Notes:
         * pellet size is 6x6
         * powerpellet size is 12x12
         * 
         */


        public Pellet(double a, double b, int n, Boolean isBig)
        {
            x = a;
            y = b;
            pelletNum = n;
            isPowerPellet = isBig;
            rect = new Rectangle((int)a, (int)b, 6, 6);

        }

        public void placePellet()
        {
            rect.X = (int)x;
            rect.Y = (int)y;
        }

        public void removePellet()
        {
            rect.X = (int)-1000;
            rect.Y = (int)-1000;
        }

        //getters
        public Boolean getIsPowerPellet()
        {
            return isPowerPellet;
        }
        
        public Boolean getIsEaten()
        {
            return isEaten;
        }

        public double getX()
        {
            return x;
        }

        public double getY()
        {
            return y;
        }

        public int getPelletNum()
        {
            return pelletNum;
        }

        public Rectangle getRect()
        {
            return rect;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

    }
}
