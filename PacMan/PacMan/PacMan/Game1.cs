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

        bool pacJustDied;
        bool pacMoved;

        SpriteFont arcadeNormal;

        Texture2D whiteBoxTexture;
        Texture2D powerPelletTexture;

        String topText;
        Vector2 posOfTopText;
        Pellet[] pellets;

        Pellet[,] tester = new Pellet[28, 36];

        int[] pelletPositionsX;
        int[] pelletPositionsY;
        Boolean isPowerMode;
        Texture2D spritesheet;

        int[,] mapsquare = new int[28, 36];
        Board map;

        String text;
        Vector2 pos;
        Boolean dead = false;
        KeyboardState Oldkb;
        int score;
        Rectangle lifesource;
        Pacboi boi;
        Direction nextInQueue;

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

            // TODO: Add your initialization logic here
            boi = new Pacboi(Content.Load<Texture2D>("spritesheet"), new Rectangle(312, 615, 45, 45),
                new Rectangle(3, 0, 15, 15), new Vector2(0, 0), new Rectangle(327,630,14,14));
            lifesource = new Rectangle(132, 17, 15, 15);
            //pacboi's starting location based off of map tiles    
            //25.625 y
            //13 x
            pacMoved = false;
            pacJustDied = false;
            nextInQueue = Direction.Up;

            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;

            score = 0;

            mapsquare = GetTiles();
            map = new Board(Content.Load<Texture2D>("pacmenu"), new Rectangle(0, 72, 672, 744), mapsquare);

            text = "Test Text hererererere.....";
            //Isaiahs Stuff \______________________
            pellets = new Pellet[244];

            pelletPositionsX = new int[] { 50, 100, 150 };
            pelletPositionsY = new int[] { 50, 100, 150 };
            //setPellets();

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
                    else if (map.space[r, c].powerPellet == true)
                        tester[r, c] = new Pellet(r, c, true);
                    else
                        tester[r, c] = new Pellet(10000, 100000, false);

                }
            }
            //______________________________________

            ghosts = new Ghost[]
            {
                new Inky(24 * 12, 24 * 17 ),
                new Blinky(24 * 14, 24 * 14),
                new Pinky(24 * 14, 24 * 17),
                new Clyde(24 * 16, 24 * 17)
            };
            foreach (Ghost g in ghosts) g.Run();

            Oldkb = Keyboard.GetState();

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
            Boolean test = false;
            KeyboardState kb = Keyboard.GetState();
            GamePadState gp = GamePad.GetState(PlayerIndex.One);
            if (kb.IsKeyDown(Keys.Escape) || gp.Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (map.start == true)
                if (kb.IsKeyDown(Keys.Space) || gp.Buttons.Start == ButtonState.Pressed)
                    map.start = false;

            if (map.start == false)
                map.screen = Content.Load<Texture2D>("pacman board");
            for (int r = 0; r < 28; r++)
            {
                for (int c = 0; c < 36; c++)
                {
                    if (tester[r, c] != null && tester[r, c].rect.Intersects(boi.rec))
                    {
                        score += 10;
                        tester[r, c] = null;
                    }
                }
            }

            // TODO: Add your update logic here

            //Warps (Left and right sides)
            if (boi.rec.X > graphics.GraphicsDevice.Viewport.Width)
                boi.rec.X = -45;
            if (boi.rec.X < -45)
                boi.rec.X = graphics.GraphicsDevice.Viewport.Width;

            //Pacman movement
            /*
             * get new Direction
             * check new direction
             * if new direction is legit, go in that direction
             */

            if (dead == false)
            {
                if (pacJustDied)
                {
                    pacJustDied = false;
                    ghosts[0].x = 24 * 12;
                    ghosts[0].y = 24 * 17;
                    ghosts[1].x = 24 * 14;
                    ghosts[1].y = 24 * 14;
                    ghosts[2].x = 24 * 14;
                    ghosts[2].y = 24 * 17;
                    ghosts[3].x = 24 * 16;
                    ghosts[3].y = 24 * 17;
                }
                for (int r = 0; r < 28; r++)
                {
                    for (int c = 0; c < 36; c++)
                    {
                        if (map.space[r, c].Pdead)
                            if (map.space[r, c].rect.Intersects(boi.hitbox))
                                test = true;

                    }
                }

                if (test == false)
                {
                    boi.velocities.X = 0;
                    boi.velocities.Y = 0;
                }
                if (kb.IsKeyDown(Keys.W) || gp.DPad.Up == ButtonState.Pressed)
                {
                    pacMoved = true;
                    boi.dir = Direction.Up;
                    nextInQueue = Direction.Up;
                }
                if (kb.IsKeyDown(Keys.A) || gp.DPad.Left == ButtonState.Pressed)
                {
                    pacMoved = true;
                    boi.dir = Direction.Left;
                    nextInQueue = Direction.Left;
                }
                if (kb.IsKeyDown(Keys.S) || gp.DPad.Down == ButtonState.Pressed)
                {
                    pacMoved = true;
                    boi.dir = Direction.Down;
                    nextInQueue = Direction.Down;
                }
                if (kb.IsKeyDown(Keys.D) || gp.DPad.Right == ButtonState.Pressed)
                {
                    pacMoved = true;
                    boi.dir = Direction.Right;
                    nextInQueue = Direction.Right;
                }

                //check direction
                Rectangle newRectHitBox = new Rectangle(boi.hitbox.X, boi.hitbox.Y, boi.hitbox.Width, boi.hitbox.Height);
                bool canMove = true;
                switch (boi.dir)
                {
                    case Direction.Up:
                        newRectHitBox.Y -= 7;
                        break;
                    case Direction.Left:
                        newRectHitBox.X -= 7;
                        break;
                    case Direction.Down:
                        newRectHitBox.Y += 7;
                        break;
                    case Direction.Right:
                        newRectHitBox.X += 7;
                        break;
                }

                for (int r = 0; canMove && r < 28; r++)
                {
                    for (int c = 0; canMove && c < 36; c++)
                    {
                        if (map.space[r, c].Pdead)
                            if (map.space[r, c].rect.Intersects(newRectHitBox))
                                canMove = false;
                    }
                }
                if (canMove)
                {
                    switch (boi.dir)
                    {
                        case Direction.Up:
                            boi.velocities.Y = -4;
                            boi.velocities.X = 0;
                            break;
                        case Direction.Left:
                            boi.velocities.Y = 0;
                            boi.velocities.X = -4;
                            break;
                        case Direction.Down:
                            boi.velocities.Y = 4;
                            boi.velocities.X = 0;
                            break;
                        case Direction.Right:
                            boi.velocities.Y = 0;
                            boi.velocities.X = 4;
                            break;
                    }
                }
                boi.Update();
                foreach (Ghost g in ghosts)
            {
                if (pacMoved)
                    g.Update(boi, ghosts[1], map); //ghosts[1] 
                //collisions with ghosts and pacboi
                    if (g.getRect().Intersects(boi.hitbox))
                    {
                        Console.WriteLine("Lose a life");
                        ///kelby added following. testing death
                        if (dead == false)
                        {
                            boi.lives--;
                            boi.source.Y = 0;
                            boi.counter = 0;
                            dead = true;
                        }
                        boi.death();
                        if (boi.counter >= 150)
                        {
                            boi.respawn();
                            dead = false;
                        }
                    }
            }
                if (boi.lives == 0)
                {
                    map.screen = Content.Load<Texture2D>("game over");
                    

                }


                if (isPowerMode)
                {
                }


                //else
                //{
                //    if (boi.velocities.Y > 0)
                //    {
                //        boi.rec.Y -= 4;
                //        boi.hitbox.Y -= 4;
                //    }
                //    if (boi.velocities.Y < 0)
                //    {
                //        boi.rec.Y += 4;
                //        boi.hitbox.Y += 4;
                //    }
                //    if (boi.velocities.X > 0)
                //    {
                //        boi.rec.X -= 4;
                //        boi.hitbox.X -= 4;
                //    }
                //    if (boi.velocities.X < 0)
                //    {
                //        boi.rec.X += 4;
                //        boi.hitbox.X += 4;
                //    }
                //}
            }
            //Death test
            if (kb.IsKeyDown(Keys.E) && kb.IsKeyDown(Keys.R) || dead == true)
            {
                pacJustDied = true;
                if(dead == false)
                {
                    boi.lives--;
                    boi.source.Y = 0;
                    boi.counter = 0;
                    dead = true;
                }
                boi.death();
                if (boi.counter >= 150)
                {
                    boi.respawn();
                    dead = false;
                }
            }
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
                //pellet drawing
                for (int r = 0; r < 28; r++)
                {
                    for (int c = 0; c < 36; c++)
                    {
                        if (tester[r, c] != null)
                        {
                            //if(map.space[r,c].Pdead == true)
                                //spriteBatch.Draw(whiteBoxTexture, new Rectangle(r *24,c*24,24,24), Color.Green);
                            if (tester[r, c].isPowerPellet == false)
                                spriteBatch.Draw(whiteBoxTexture, tester[r, c].rect, Color.White);
                            else
                                spriteBatch.Draw(powerPelletTexture, tester[r, c].rect, Color.White);
                        }
                    }
                }
                //each ghost drawing
                foreach (Ghost g in ghosts)
                {
                    Rectangle otherRect = new Rectangle(g.getRect().X - 16, g.getRect().Y - 12, g.getRect().Width, g.getRect().Height);
                    spriteBatch.Draw(spritesheet, otherRect, g.getSource(), Color.White);
                }
                //pacman drawing
                spriteBatch.Draw(boi.tex, boi.rec, boi.source, boi.colour);

            }
            
            //lives
            int l = 0;
            while (l < boi.lives)
            {
                l++;
                spriteBatch.Draw(boi.tex, new Rectangle((l * 48) + (2 * 24), 34 * 24, 48, 48), lifesource, Color.White);
            }
            spriteBatch.DrawString(arcadeNormal, topText, posOfTopText, Color.White);

            if(boi.lives <= 0)
            {
                spriteBatch.Draw(map.screen, map.screenSize, Color.White);
            }


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