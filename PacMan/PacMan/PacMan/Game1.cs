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

        Texture2D boardt;
        Rectangle boardr;
        MapSquares board;

        String text;
        Vector2 pos;
        Boolean dead = false;
        KeyboardState Oldkb;

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

            // TODO: Add your initialization logic here
            boi = new Pacboi(Content.Load<Texture2D>("spritesheet"), new Rectangle(300, 400, 45, 45),
                new Rectangle(3, 0, 15, 15), new Vector2(0, 0));

            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;

            ghosts = new Ghost[]
            {
                new Ghost(0, 0, Name.Inky),
                new Ghost(0, 0, Name.Blinky),
                new Ghost(0, 0, Name.Pinky), 
                new Ghost(0, 0, Name.Clyde)
            };

            boardt = Content.Load<Texture2D>("pacman board");

            text = "Test Text hererererere.....";

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
            boardr = new Rectangle(0, 72, 672, 744);

            // TODO: use this.Content to load your game content here
            arcadeNormal = Content.Load<SpriteFont>("SpriteFont1");

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
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            this.Exit();

            // TODO: Add your update logic here

            //Warps (Left and right sides)
            if (boi.rec.X > graphics.GraphicsDevice.Viewport.Width)
                boi.rec.X = -45;
            if (boi.rec.X < -45)
                boi.rec.X = graphics.GraphicsDevice.Viewport.Width;

            //Pacman movement
            if (dead == false)
            {
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

                boi.Update();
            }
            //Death test
            if (kb.IsKeyDown(Keys.E) && kb.IsKeyDown(Keys.R) || dead == true)
            {
                if(dead == false)
                {
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
            spriteBatch.Draw(boardt, boardr, Color.White);
            spriteBatch.Draw(boi.tex, boi.rec, boi.source, boi.colour);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void MakePellet(double a, double b, int n)
        {
            //At start of every round game, pellet objects are made
            //
            Pellet asdf = new Pellet(a,b,n);
            //addPellettTexture here
        }

        public void addPelletTexture(Pellet myPellet)
        {

        }
    }
}
