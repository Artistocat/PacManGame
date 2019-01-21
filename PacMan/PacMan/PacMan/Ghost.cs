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
        public double x;
        public double y;
        protected Rectangle rect;
        private Name name;
        protected Vector2 velocity;
        protected Vector2 targetSquareLoc;
        protected int counter;
        protected bool scatter;
        protected Rectangle sourceRect;
        protected Direction dir;

        //88 * 3 pixels per second
        //each second is 60 fps
        //4.4 pixels per frame
        //each square is 
        //28 x 36
        const float speed = (float)4;

        public Ghost(int x, int y, Name n, Rectangle source)
        {
            this.x = x;
            this.y = y;
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
            /*if (x == 0)
            {
                Console.WriteLine("It broke");
                return;
            }*/
            //Console.WriteLine("We good " + x);
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

        protected virtual void UpdateTarget(Pacboi pacman, Ghost blinky, Board board)
        {
            Console.WriteLine("We're actually doing this");
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

        protected void UpdateDirection(Board board)
        {

            double xDistOff = targetSquareLoc.X - x / 24;
            double yDistOff = targetSquareLoc.Y - y / 24;

            double?[] distsOff = new double?[4];
            Direction closestDir = Direction.Up;

            //Up
            distsOff[0] = getDistOff(xDistOff, yDistOff - 1, board);

            //Left
            distsOff[1] = getDistOff(xDistOff - 1, yDistOff, board);

            //Down
            distsOff[2] = getDistOff(xDistOff, yDistOff + 1, board);

            //Right
            distsOff[3] = getDistOff(xDistOff + 1, yDistOff, board);

            for (int i = 1; i < distsOff.Length; i++)
            {
                if (distsOff[i] != null && distsOff[i] > distsOff[(int)closestDir])
                {
                    closestDir = (Direction)i;
                }
            }
            dir = closestDir;
            UpdateVelocity();
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
        protected void CheckScatter()
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

        protected double? getDistOff(double xDistOff, double yDistOff, Board board)
        {
            /*if (board[x][y].isDeadSpace)
            {
                return null;
            }*/
            return Math.Sqrt(xDistOff * xDistOff + (yDistOff) * (yDistOff));
        }

        public int getSquareX() { return (int)(x / 24); }

        public int getSquareY() { return (int)(y / 24); }
        
        public Rectangle getRect() { return rect; }

        public Rectangle getSource() { return sourceRect; }
    }
}
