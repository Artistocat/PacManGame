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
        public Boolean Pdead;
        public Boolean Gdead;
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
            pellet = false;
            powerPellet = false;
            Pdead = false;
            Gdead = false;
        }
        public MapSquares(int t)
        {
            switch (t)
            {
                case 0:
                    Pdead = true;
                    Gdead = true;
                    pellet = false;
                    powerPellet = false;
                    break;
                case 1:
                    Pdead = false;
                    Gdead = false;
                    pellet = true;
                    powerPellet = false;
                    break;
                case 2:
                    Pdead = false;
                    Gdead = false;
                    pellet = false;
                    powerPellet = true;
                    break;
                case 3:
                    Pdead = false;
                    Gdead = false;
                    pellet = false;
                    powerPellet = false;
                    break;
                case 4:
                    Pdead = true;
                    Gdead = false;
                    pellet = false;
                    powerPellet = false;
                    break;

            }
            //dead[(int)loc.X,(int)loc.Y] = ded;
        }
    }
}
