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
using System.Diagnostics;
using Helper;

namespace Peggle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        internal static GraphicsDeviceManager graphics;
        internal static ContentManager cm;
        internal static Game1 game;
        internal static Level currentLevel;
        SpriteBatch spriteBatch;

        Curve testCurve  = new Curve(new Vector2(0, 0), new Vector2(100, 0), new Vector2(100, 100));
        Curve testCurve2 = new Curve(new Vector2(0, 10), new Vector2(90, 10), new Vector2(90, 100));
        CurvedBrick curvedBrickTest;
        QuadCollection curveQuads;
        //Quad testQuad = new Quad(new Vector2(94.24f, 57.76f), new Vector2(80.64f, 31.36f),  new Vector2(72.576f, 38.224f), new Vector2(84.816f, 61.984f));
        Quad testQuad2 = new Quad(new Vector2(x:80.49374f, y:51.00624f), new Vector2(x:89.4375f,  y:45.56249f), new Vector2(x:87.24375f, y:71.25624f), new Vector2(x:96.9375f,  y:68.06248f));
        Line testLine = Line.getLineFromPoints(new Vector2(0,0), new Vector2(1,1));

        public Game1()
        {
            game = this;
            curvedBrickTest = new CurvedBrick(testCurve, testCurve2);
            curveQuads = curvedBrickTest.toQuads();
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            cm = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            currentLevel = LevelLoader.loadXML(this, @"Content\level.xml");
            currentLevel.load();

            Components.Add(curveQuads);
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
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

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState ms = Mouse.GetState();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            testCurve.draw();
            testCurve2.draw();

            testQuad2.draw();

            DebugHelper.drawPoint(testQuad2.topLeft);
            DebugHelper.drawPoint(testQuad2.topRight);
            DebugHelper.drawPoint(testQuad2.bottomLeft);
            DebugHelper.drawPoint(testQuad2.bottomRight);
            DebugHelper.drawPoint(new Vector2(89f, 52f));

            base.Draw(gameTime);
        }

        public static void clearGameComponents()
        {
            game.Components.Clear();
        }

        public static void addGameComponent(GameComponent gameComponent)
        {
            game.Components.Add(gameComponent);
        }

        public static void removeGameComponent(GameComponent gameComponent)
        {
            game.Components.Remove(gameComponent);
        }

        public static GameComponentCollection getComponents()
        {
            return game.Components;
        }
    }
}
