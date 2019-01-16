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
        private bool scatter;

        //88 * 3 pixels per second
        //each second is 60 fps
        //4.4 pixels per frame
        //each square is 
        //28 x 36
        const float speed = (float)4.4;
        const int INKY = 0, BLINKY = 1, PINKY = 2, CLYDE = 3;
        

        public Ghost(int x, int y, int name)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = rect.Height = 45;
            this.name = name;
            velocity = new Vector2(0, 0);
            scatter = false;
        }

        public void Update(Pacboi pacman)
        {
            counter++;
            x += velocity.X;
            y += velocity.Y;
            rect.X = (int)x;
            rect.Y = (int)y;
            if (counter == 24)
            {
                counter = 0;
                UpdateTarget(pacman);
            }
        }

        private void UpdateTarget(Pacboi pacman)
        {
            int pacX = pacman.rec.X;
            int pacY = pacman.rec.Y;
            int squareX = pacX / 24;
            int squareY = pacY / 24;
            Vector2 pacV = pacman.velocities;

            if (name == Ghost.INKY)
            {

            }

            if (name == Ghost.BLINKY)
            {
                targetSquareLoc.X = squareX;
                targetSquareLoc.Y = squareY;
            }

            if (name == Ghost.PINKY)
            {
                int distFrontPac = 4;
                if (pacV.X > 0) squareX += distFrontPac;
                if (pacV.X < 0) squareX -= distFrontPac;
                if (pacV.Y > 0) squareY += distFrontPac;
                if (pacV.Y < 0) squareY -= distFrontPac;
            }

            if (name == Ghost.CLYDE)
            {
                int xGrid = (int) (x / 24);
                int yGrid = (int) (y / 24);
                double xDistFromPac = squareX - xGrid;
                double yDistFromPac = squareY - yGrid;
                double distFromPac = Math.Sqrt(xDistFromPac * xDistFromPac + yDistFromPac * yDistFromPac);
                if (distFromPac <= 8)
                {
                    Scatter();
                }
                else
                {
                    targetSquareLoc.X = squareX;
                    targetSquareLoc.Y = squareY;
                }
            }
        }

        private void Scatter()
        {
            scatter = true;
            if (name == Ghost.INKY)
            {
                targetSquareLoc.X = 27;
                targetSquareLoc.Y = 35;
            }

            if (name == Ghost.BLINKY)
            {
                targetSquareLoc.X = 26; //NOT A TYPO!! YES IT IS ACTUALLY 1 OFF THE EDGE. IDK HOW IT WORKS BUT IT DOES
                targetSquareLoc.Y = 0;
            }

            if (name == Ghost.PINKY)
            {
                targetSquareLoc.X = 1; //NOT A TYPO!!! YES IT IS ACTUALLY 1, NOT 0
                targetSquareLoc.Y = 0;
            }

            if (name == Ghost.CLYDE)
            {
                targetSquareLoc.X = 0;
                targetSquareLoc.Y = 35;//height;
            }
        }


        
    }
}
