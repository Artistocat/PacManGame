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
        //protected Rectangle centerRect;
        private Name name;
        protected Vector2 velocity;
        protected Vector2 targetSquareLoc;
        protected int counter;
        protected int animateCounter;
        protected bool scatter;
        protected Rectangle sourceRect;
        protected Direction dir;
        protected bool run;

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
            //centerRect = new Rectangle(x + 10, y + 10, 24, 24);
            this.name = n;
            velocity = new Vector2(0, speed);
            scatter = false;
            run = false;
            sourceRect = source;
        }

        public void Update(Pacboi pacman, Ghost blinky, Board board)
        {
            counter++;
            animateCounter++;
            x += velocity.X;
            y += velocity.Y;
            if (x + rect.Width < 0) x += board.screenSize.Width;
            if (x > board.screenSize.Width) x -= board.screenSize.Width;
            rect.X = (int)x;
            rect.Y = (int)y;
            //centerRect.X = (int)x;
            //centerRect.Y = (int)y;
            //if (scatter)
            //{
            //    CheckScatter();
            //}
            //else
            if (counter == 6)
            {
                counter = 0;
                if (scatter)
                    UpdateDirection(board);
                else
                    UpdateTarget(pacman, blinky, board);
            }

            sourceRect.X = 4;
            if (animateCounter < 8)
            {
                //First one
            }
            else
            {
                //second one
                sourceRect.X += 16;
            }
            if (dir == Direction.Left) sourceRect.X += 32;
            if (dir == Direction.Up) sourceRect.X += 64;
            if (dir == Direction.Down) sourceRect.X += 96;
            if (animateCounter == 16) animateCounter = 0;

        }

        protected virtual void UpdateTarget(Pacboi pacman, Ghost blinky, Board board)
        {
        }

        protected void UpdateDirection(Board board)
        {

            double xDistOff = targetSquareLoc.X - x / 24;
            double yDistOff = targetSquareLoc.Y - y / 24;

            double?[] distsOff = new double?[4];
            Direction closestDir = Direction.Up;

            //Up
            distsOff[0] = getDistOff(xDistOff, yDistOff - 1, board, Direction.Up);

            //Left
            distsOff[1] = getDistOff(xDistOff - 1, yDistOff, board, Direction.Left);

            //Down
            distsOff[2] = getDistOff(xDistOff, yDistOff + 1, board, Direction.Down);

            //Right
            distsOff[3] = getDistOff(xDistOff + 1, yDistOff, board, Direction.Right);

            //for (int i = 0; i < distsOff.Length; i++)
            //{
            //    if (distsOff[i] != null && distsOff[i] > distsOff[(int)closestDir])
            //    {
            //        closestDir = (Direction)i;
            //    }
            //    if (distsOff[i] == null)
            //        Console.WriteLine("bad direction");
            //}

            HashSet<Direction> validDirs = new HashSet<Direction>();
            for (int i = 0; i < distsOff.Length; i++)
            {
                if (distsOff[i] != null)
                    validDirs.Add((Direction)(i));
            }
            if (!run)
            {
                closestDir = GetClosestDir(validDirs, distsOff);
                dir = closestDir;
            }
            else
            {
                dir = RandomDir(validDirs);
            }
            UpdateVelocity();
        }

        private Direction RandomDir(HashSet<Direction> validDirs)
        {
            Random rand = new Random();
            int choice = rand.Next(validDirs.Count);
            try
            {
                return validDirs.ElementAt(choice);
            }
            catch (ArgumentOutOfRangeException e)
            {
                return dir;
            }
        }

        private Direction GetClosestDir(HashSet<Direction> validDirs, double?[] distsOff)
        {
            try
            {
                if (validDirs.Count == 1) return validDirs.ElementAt(0);
                if (distsOff[(int)validDirs.ElementAt(0)] > distsOff[(int)validDirs.ElementAt(1)])
                {
                    validDirs.Remove(validDirs.ElementAt(1));
                    return GetClosestDir(validDirs, distsOff);
                }
                validDirs.Remove(validDirs.ElementAt(0));
            } catch (ArgumentOutOfRangeException e) { return dir; }
            return GetClosestDir(validDirs, distsOff);
        }

        public virtual void Scatter()
        {
            //scatter = true;
            //if (name == Name.Inky)
            //{
            //    targetSquareLoc.X = 27;
            //    targetSquareLoc.Y = 35;
            //}

            //if (name == Name.Blinky)
            //{
            //    targetSquareLoc.X = 26; //NOT A TYPO!! YES IT IS ACTUALLY 1 OFF THE EDGE. IDK HOW IT WORKS BUT IT DOES
            //    targetSquareLoc.Y = 0;
            //}

            //if (name == Name.Pinky)
            //{
            //    targetSquareLoc.X = 1; //NOT A TYPO!!! YES IT IS ACTUALLY 1, NOT 0
            //    targetSquareLoc.Y = 0;
            //}

            //if (name == Name.Clyde)
            //{
            //    targetSquareLoc.X = 0;
            //    targetSquareLoc.Y = 35;//height;
            //}
        }

        //TODO
        //protected void CheckScatter()
        //{
        //    if (name == Name.Clyde)
        //    {
        //        scatter = false; //TODO
        //    }
        //}

        public void StopScatter()
        {
            scatter = false;
        }

        public void Run()
        {
            run = true;
            scatter = false;
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

        protected double? getDistOff(double xDistOff, double yDistOff, Board board, Direction newDir)
        {
            if (dir == Direction.Up && newDir == Direction.Down ||
                dir == Direction.Left && newDir == Direction.Right ||
                dir == Direction.Down && newDir == Direction.Up ||
                dir == Direction.Right && newDir == Direction.Left)
                return null;

            int newSquareX = getSquareX();
            int newSquareY = getSquareY();
            if (newDir == Direction.Up) newSquareY -= 1;
            if (newDir == Direction.Left) newSquareX -= 1;
            if (newDir == Direction.Down) newSquareY += 1;
            if (newDir == Direction.Right) newSquareX += 1;

            try
            {
                if (board.space[newSquareX, newSquareY].Gdead)
                {
                    return null;
                }

            } catch (IndexOutOfRangeException e)
            {
                return null;
            }

            return Math.Sqrt(xDistOff * xDistOff + yDistOff * yDistOff);
        }

        public int getSquareX() { return (int)(x / 24); }

        public int getSquareY() { return (int)(y / 24); }
        
        public Rectangle getRect() { return rect; }

        public Rectangle getSource() { return sourceRect; }
    }
}
