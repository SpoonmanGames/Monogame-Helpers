using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGame_Helpers
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        /**
         * Arreglo para cargar cualquier asset de GUI (Graphical User Interface) de forma previa
         * Esto evita glitches cuando se cargan repentinamente
         * Aquí se debiera cargar todo lo que hace referencia a GUI
         */
        static readonly string[] preloadAssets =
        {
            "Gradientes/PopupGradient",
        };

        #region GameComponents
        
        ScreenManager.ScreenManager screenManager;
        DebugManager.DebugManager debugManager;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";            
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
            this.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            screenManager = new ScreenManager.ScreenManager(this);
            screenManager.Enabled = true;
            screenManager.Visible = true;
            Components.Add(screenManager);

#if DEBUG
            debugManager = new DebugManager.DebugManager(this);
            debugManager.Enabled = true;
            debugManager.Visible = true;            
            Components.Add(debugManager);
#endif
            
            screenManager.AddScreen(new TitleScreen("Monogame Helpers"), null);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
