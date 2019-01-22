using System;
using System.IO;
using System.Collections;
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

namespace Pacman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont arcadeNormal;

        Texture2D whiteBoxTexture;
        Texture2D powerPelletTexture;

        String topText;
        Vector2 posOfTopText;
        Pellet[] pellets;

        Pellet[,] tester = new Pellet[28, 36];

        int[] pelletPositionsX;
        int[] pelletPositionsY;
        Texture2D spritesheet;

        int[,] mapsquare = new int[28,36];
        Board map;
 
        String text;
        Vector2 pos;
        int score;

        Pacboi boi;

        Ghost[] ghosts;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 672;
            graphics.PreferredBackBufferHeight = 864;
            graphics.ApplyChanges();

            this.Window.AllowUserResizing = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //pacboi's starting location based off of map tiles
            //25.625 y
            //13 x
            boi = new Pacboi(Content.Load<Texture2D>("spritesheet"), new Rectangle(312, 615, 45, 45),
                new Rectangle(3, 0, 16, 16), new Vector2(0, 0));

            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;

            ghosts = new Ghost[]
            {
                ///aaron, i commented out your old pos and put in new ones, see if you like them more or less. we still need to implement the detection of whether
                ///or not its legal space or dead space.
                //new Inky(24 * 12 + 12, 24 * 17 + 12),
                new Inky(24 * 11, 24 * 17 - 6),
                //new Blinky(24 * 14 + 12, 24 * 14 + 12),
                new Blinky(24 * 13, 24 * 14 - 6),
                //new Pinky(24 * 14 + 12, 24 * 17 + 12), 
                new Pinky(24 * 13, 24 * 17 - 6),
                //new Clyde(24 * 16 + 12, 24 * 17 + 12)
                new Clyde(24 * 15, 24 * 17 - 6)
            };

            score = 0;

            mapsquare = GetTiles();
            map = new Board(Content.Load<Texture2D>("pacmenu"), new Rectangle(0, 72, 672, 744), mapsquare);

            text = "Test Text hererererere.....";
            //Isaiahs Stuff \______________________
            pellets = new Pellet[244];

            pelletPositionsX = new int[] { 50, 100, 150};
            pelletPositionsY = new int[] { 50, 100, 150 };
            setPellets();

            topText = "1UP     HIGH SCORE";
            posOfTopText = new Vector2(100, 0);

            //dem
            //224
            //288
            //672
            //864

            //if its a normal or power pellet
            Boolean isPowerPelletTrue = false;
            for (int i = 0; i < pelletPositionsX.Length; i++)
            {
                //if (pelletPositionsX[i] == 24 || pelletPositionsX[i] == 816 || pelletPositionsY[i] == 144 || pelletPositionsY[i] == 624)
                //{
                //    isPowerPelletTrue = true;
                //}
                //else
                //{
                //    isPowerPelletTrue = false;
                //}
                //makes the pellet objects
                pellets[i] = new Pellet(pelletPositionsX[i], pelletPositionsY[i], i, isPowerPelletTrue);
            }
            for (int r = 0; r < 28; r++)
            {
                for (int c = 0; c < 36; c++)
                {
                    if (map.space[r, c].pellet == true)
                        tester[r, c] = new Pellet(r, c, false);
                    else if(map.space[r, c].powerPellet == true)
                        tester[r, c] = new Pellet(r, c, true);
                    else
                        tester[r, c] = new Pellet(10000, 100000,false);

                }
            }
            //______________________________________
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            IsMouseVisible = true;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            spritesheet = Content.Load<Texture2D>("spritesheet");
            
            arcadeNormal = Content.Load<SpriteFont>("SpriteFont1");

            whiteBoxTexture = Content.Load<Texture2D>("white box");
            powerPelletTexture = Content.Load<Texture2D>("powerpellet");

            //Loop through every pellet object and give texture
            //for (int i = 0; i < pelletPositionsX.Length; i++)
            //{
            //    if (pellets[i].getIsPowerPellet())
            //    {
            //        pellets[i].texture = Content.Load<Texture2D>("powerpellet");
            //    }
            //    else
            //    {
            //        pellets[i].texture = Content.Load<Texture2D>("white box");
            //    }
            //}

        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            GamePadState gp = GamePad.GetState(PlayerIndex.One);
            if (kb.IsKeyDown(Keys.Escape) || gp.Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (map.start == true)
                if (kb.IsKeyDown(Keys.Space) || gp.Buttons.Start == ButtonState.Pressed)
                    map.start = false;

            if (map.start == false)
                map.screen = Content.Load<Texture2D>("pacman board");

            for (int i = 0; i < pellets.Length; i++)
            {
                if (pellets[i] != null && pellets[i].rect.Intersects(boi.rec))
                {
                    score += 10;
                    pellets[i] = null;
                }
            }

            if (boi.rec.X > graphics.GraphicsDevice.Viewport.Width)
                boi.rec.X = -45;
            if (boi.rec.X < -45)
                boi.rec.X = graphics.GraphicsDevice.Viewport.Width;

            if (kb.IsKeyDown(Keys.A) || gp.DPad.Left == ButtonState.Pressed)
            {
                boi.velocities.X = -4;
                boi.velocities.Y = 0;
            }
            if (kb.IsKeyDown(Keys.D) || gp.DPad.Right == ButtonState.Pressed)
            {
                boi.velocities.X = 4;
                boi.velocities.Y = 0;
            }
            if (kb.IsKeyDown(Keys.W) || gp.DPad.Up == ButtonState.Pressed)
            {
                boi.velocities.Y = -4;
                boi.velocities.X = 0;
            }
            if (kb.IsKeyDown(Keys.S) || gp.DPad.Down == ButtonState.Pressed)
            {
                boi.velocities.Y = 4;
                boi.velocities.X = 0;
            }
            ///aaron - commenting this out just to place the ghosts correctly rn
            //foreach (Ghost g in ghosts)
            //{
            //    g.Update(boi, ghosts[1], map);
            //}

            boi.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            // this entire if else statement sets up whether or not its the start screen or not.
            if (map.start == true)
                spriteBatch.Draw(map.screen, map.screenSize, Color.White);
            else
            {
                //refreshing the map
                spriteBatch.Draw(map.screen, map.screenSize, Color.White);
                //each ghost drawing
                foreach (Ghost g in ghosts)
                {
                    spriteBatch.Draw(spritesheet, g.getRect(), g.getSource(), Color.White);
                }
                //pacman drawing
                spriteBatch.Draw(boi.tex, boi.rec, boi.source, boi.colour);
                //pellet drawing
                for (int r = 0; r < 28; r++)
                {
                    for (int c = 0; c < 36; c++)
                    {
                        if (tester[r, c].isPowerPellet == false)
                            spriteBatch.Draw(whiteBoxTexture, tester[r, c].rect, Color.White);
                        else
                            spriteBatch.Draw(powerPelletTexture, tester[r, c].rect, Color.White);
                    }
                }

            }
            spriteBatch.DrawString(arcadeNormal,topText,posOfTopText,Color.White);

            


            //foreach (Pellet p in pellets)
            //{
            //    if (p != null)
            //    {
            //        if (p.getIsPowerPellet())
            //            spriteBatch.Draw(powerPelletTexture, p.getRect(), Color.White);
            //        else
            //            spriteBatch.Draw(whiteBoxTexture, p.getRect(), Color.White);
            //    }
            //}

            //spriteBatch.Draw(boi.tex, boi.rec, boi.source, boi.colour);
            //foreach (Ghost g in ghosts){
            //    spriteBatch.Draw(spritesheet, g.getRect(), g.getSource(), Color.White);
            //}
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /*public Pellet MakePellet(double a, double b, int n, Boolean i)
        {
            //At start of every round game, pellet objects are made
            //
            Pellet asdf = new Pellet(a,b,n,i);

            return asdf;
        }*/
        
        public void setPellets()
        {
            //Pellet[] pellets;
            //double[] pelletPositionsX;
            //double[] pelletPositionsY;

            for (int a = 0; a < 36; a++)
            {
                // a = rows
                int b = 0; // collumns

                //28
                //36

                //conditions
                //Updates the x y values for the pellets

                if (a == 0)
                {
                    if (b != 15 || b != 14)
                    {
                        //dont add pellet
                    }
                }
                if (a == 1)
                {

                }
            }
            //Pellet asdf = new Pellet(a, b, n);
            //addPellettTexture here
        }

        /*public void addPelletTexture(Pellet myPellet)
        {

        }*/
        // This function will take a file's data and separate it by ',' found in the
        // file. This is not my function but I will try to explain it's code.

        private static int[,] GetTiles()
        {


            int width = 28;
            int height = 36;

            int[,] mapSquares = new int[28, 36];

            StreamReader myFileC = new StreamReader("Pacman.txt");

            for (int i = 0; i < height; i++)
            {
                String nextLine = myFileC.ReadLine();
                for (int j = 0; j < 14; j++)
                {
                    mapSquares[j, i] = mapSquares[width - 1 - j, i] = int.Parse(nextLine.Substring(j * 2, 1));
                }
            }
            myFileC.Close();
            return mapSquares;
        }



    }
}
