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
        public Boolean isPowerPellet;
        int x, y;
        int pelletNum;
        public Rectangle rect;

        /*Notes:
         * pellet size is 6x6
         * powerpellet size is 12x12
         * 
         */
        /// kelby's pellet constructer that works with the board class.. not the best but it functions. do work on it a bit to be able to edit it a bit.
        public Pellet(int x, int y, Boolean isPower)
        {
            if(isPower == true)
                rect = new Rectangle(x * 24, y * 24, 24, 24);
            else
                rect = new Rectangle(x * 24 + 9, y * 24 + 9, 6, 6);
            isPowerPellet = isPower;
        }

        public Pellet(int x, int y, int n, Boolean isPower)
        {
            this.x = x;
            this.y = y;
            pelletNum = n;
            isPowerPellet = isPower;
            rect = new Rectangle((int)x, (int)y, 6, 6);

        }

        //public void placePellet()
        //{
        //    rect.X = (int)x;
        //    rect.Y = (int)y;
        //}

        //public void removePellet()
        //{
        //    rect.X = (int)-1000;
        //    rect.Y = (int)-1000;
        //}


        //getters
        public Boolean getIsPowerPellet()
        {
            return isPowerPellet;
        }
        
        //public Boolean getIsEaten()
        //{
        //    return isEaten;
        //}

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

        //public Texture2D getTexture()
        //{
        //    return texture;
        //}

    }
}
