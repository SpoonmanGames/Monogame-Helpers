using Helpers.Polygon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.ExtensionMethods.Vector2Ext;

namespace Helpers.DebugOutput
{
    public class DebugOutput : DrawableGameComponent
    {
        private ContentManager content;
        private SpriteBatch spriteBatch;
        private SpriteFont debug_font;

        private RenderTarget2D rectangles;

        private DebugBox fpsBox;
        private DebugBox resolutionBox;

        #region Object Counter

        #endregion

        #region Assets Counter

        #endregion


        public DebugOutput(Game game)
            : base(game) { }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Game.Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debug_font = content.Load<SpriteFont>("Fonts/DebugFont");

            this.fpsBox = new DebugBox(new Vector2(10, 10), debug_font, true, "FPS: 60");
            this.fpsBox.LoadContent(GraphicsDevice);

            this.resolutionBox = new DebugBox(new Vector2(Game.Window.ClientBounds.Width - 10, 10), debug_font, false, "Resolution:","1920x1080");
            this.resolutionBox.LoadContent(GraphicsDevice);

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            content.Unload();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Capturando datos para FPS
            this.fpsBox.Update(string.Format("FPS: {0}", 1000 / gameTime.ElapsedGameTime.Milliseconds));

            // Capturando datos para Resolution
            this.resolutionBox.Update("Resolution",string.Format("{0}x{1}", Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            // Dibujando FPS
            this.fpsBox.Draw(gameTime, spriteBatch);

            // Dibujando Resolution
            this.resolutionBox.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
