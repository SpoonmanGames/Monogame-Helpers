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

        private DebugBox fpsBox;
        private DebugBox resolutionBox;
        private DebugBox objectCounterBox;
        private DebugBox assetCounterBox;

        public int ObjectCounter { get; set; }
        public int AssetCounter { get; set; }

        public DebugOutput(Game game)
            : base(game) { }

        public override void Initialize()
        {
            base.Initialize();

            this.ObjectCounter = 0;
            this.AssetCounter = 0;
        }

        protected override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Game.Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debug_font = content.Load<SpriteFont>("Fonts/DebugFont");

            this.fpsBox = new DebugBox(new Vector2(10, 10), debug_font, true);
            this.fpsBox.LoadContent(GraphicsDevice, Game.Window.ClientBounds);

            this.resolutionBox = new DebugBox(new Vector2(Game.Window.ClientBounds.Width - 10, 10), debug_font, false);
            this.resolutionBox.LoadContent(GraphicsDevice, Game.Window.ClientBounds);

            this.objectCounterBox = new DebugBox(new Vector2(Game.Window.ClientBounds.Width - 10, Game.Window.ClientBounds.Height - 10), debug_font, false);
            this.objectCounterBox.LoadContent(GraphicsDevice, Game.Window.ClientBounds);

            this.assetCounterBox = new DebugBox(new Vector2(10, Game.Window.ClientBounds.Height - 10), debug_font, true);
            this.assetCounterBox.LoadContent(GraphicsDevice, Game.Window.ClientBounds);

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

            this.fpsBox.Update(string.Format("FPS: {0}", 1000 / gameTime.ElapsedGameTime.Milliseconds));
            this.resolutionBox.Update("Resolution",string.Format("{0}x{1}", Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height));
            this.objectCounterBox.Update(string.Format("Objetos: {0}", this.ObjectCounter));
            this.assetCounterBox.Update(string.Format("Assets: {0}", this.AssetCounter));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            this.fpsBox.Draw(gameTime, spriteBatch);
            this.resolutionBox.Draw(gameTime, spriteBatch);
            this.objectCounterBox.Draw(gameTime, spriteBatch);
            this.assetCounterBox.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
