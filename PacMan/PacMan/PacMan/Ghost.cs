﻿using System;
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
        protected float x;
        protected float y;
        private Rectangle rect;
        private Name name;
        protected Vector2 velocity;
        protected Vector2 targetSquareLoc;
        private int counter;
        protected bool scatter;
        private Rectangle sourceRect;
        protected Direction dir;

        //88 * 3 pixels per second
        //each second is 60 fps
        //4.4 pixels per frame
        //each square is 
        //28 x 36
        const float speed = (float)4.4;

        public Ghost(int x, int y, Name n, Rectangle source)
        {
            rect.X = x;
            rect.Y = y;
            rect.Width = rect.Height = 45;
            this.name = n;
            velocity = new Vector2(0, speed);
            dir = Direction.Down;
            scatter = false;
            sourceRect = source;
        }

        public void Update(Pacboi pacman, Ghost blinky, Board board)
        {
            counter++;
            x += velocity.X;
            y += velocity.Y;
            rect.X = (int)x;
            rect.Y = (int)y;
            if (scatter)
            {
                CheckScatter();
            }
            else if (counter == 24)
            {
                counter = 0;
                UpdateTarget(pacman, blinky, board);
            }
        }

        protected void UpdateTarget(Pacboi pacman, Ghost blinky, Board board)
        {
            int pacX = pacman.rec.X;
            int pacY = pacman.rec.Y;
            int squareX = pacX / 24;
            int squareY = pacY / 24;
            Vector2 pacV = pacman.velocities;

            if (name == Name.Inky)
            {
                int xFrontPac = squareX;
                int yFrontPac = squareY;
                if (pacV.X > 0) xFrontPac += 2;
                if (pacV.X < 0) xFrontPac -= 2;
                if (pacV.Y > 0) yFrontPac += 2;
                if (pacV.Y < 0) yFrontPac -= 2;

                int xBlinkyDistFrontPac = xFrontPac - blinky.getSquareX();
                int yBlinkyDistFrontPac = yFrontPac - blinky.getSquareY();
                squareX = xFrontPac - xBlinkyDistFrontPac;
                squareY = yFrontPac - yBlinkyDistFrontPac;

            }

            if (name == Name.Blinky)
            {
                //Target is just where pacboi is
            }

            if (name == Name.Pinky)
            {
                int distFrontPac = 4;
                if (pacV.X > 0) squareX += distFrontPac;
                if (pacV.X < 0) squareX -= distFrontPac;
                if (pacV.Y > 0) squareY += distFrontPac;
                if (pacV.Y < 0) squareY -= distFrontPac;
            }

            if (name == Name.Clyde)
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
            }

            targetSquareLoc.X = squareX;
            targetSquareLoc.Y = squareY;
        }

        protected void Scatter()
        {
            scatter = true;
            if (name == Name.Inky)
            {
                targetSquareLoc.X = 27;
                targetSquareLoc.Y = 35;
            }

            if (name == Name.Blinky)
            {
                targetSquareLoc.X = 26; //NOT A TYPO!! YES IT IS ACTUALLY 1 OFF THE EDGE. IDK HOW IT WORKS BUT IT DOES
                targetSquareLoc.Y = 0;
            }

            if (name == Name.Pinky)
            {
                targetSquareLoc.X = 1; //NOT A TYPO!!! YES IT IS ACTUALLY 1, NOT 0
                targetSquareLoc.Y = 0;
            }

            if (name == Name.Clyde)
            {
                targetSquareLoc.X = 0;
                targetSquareLoc.Y = 35;//height;
            }
        }

        //TODO
        private void CheckScatter()
        {
            if (name == Name.Clyde)
            {
                scatter = false; //TODO
            }
        }

        protected void UpdateVelocity()
        {
            velocity.X = 0;
            velocity.Y = 0;
            if (dir == Direction.Up) velocity.Y = -speed;
            if (dir == Direction.Left) velocity.X = -speed;
            if (dir == Direction.Down) velocity.Y = speed;
            if (dir == Direction.Right) velocity.X = speed;
        }

        public int getSquareX() { return (int)(x / 24); }

        public int getSquareY() { return (int)(y / 24); }
        
        public Rectangle getRect() { return rect; }

        public Rectangle getSource() { return sourceRect; }
    }
}
