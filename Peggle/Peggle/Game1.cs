using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Peggle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        internal static Game1 game;
        internal static GraphicsDeviceManager graphics;
        internal static ContentManager cm;
        internal static LevelStateManager levelStateManager;

        public Game1()
        {
            game = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 500;

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

            levelStateManager = new LevelStateManager();
            levelStateManager.loadLevel();
            
            base.Initialize();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
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
