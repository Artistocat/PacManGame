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
    public class MapSquares
    {
        int size = 24;
        public Boolean powerPellet;
        public Boolean pellet;
        public Boolean dead;
        //Vector2
        public MapSquares()
        {
            //for (int r = 0; r < 28; r++)
            //{
            //    for(int c = 0; c < 36; c++)
            //    {
            //        dead[r,c] = false;
            //    }
            //}

            dead = false;
        }
        public MapSquares(int t)
        {
            switch (t)
            {
                case 0:
                    dead = true;
                    pellet = false;
                    powerPellet = false;
                    break;
                case 1:
                    dead = false;
                    pellet = true;
                    powerPellet = false;
                    break;
                case 2:
                    dead = false;
                    pellet = false;
                    powerPellet = true;
                    break;
                case 3:
                    dead = false;
                    pellet = false;
                    powerPellet = false;
                    break;
            }
            //dead[(int)loc.X,(int)loc.Y] = ded;
        }
    }
}
