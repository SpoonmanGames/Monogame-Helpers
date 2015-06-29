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

        public static List<DebugBox> ObjectInfoBoxList { get; set; }

        public static int ObjectCounter { get; set; }

        public static int AssetCounter { get; set; }

        public DebugOutput(Game game)
            : base(game) { }
        
        public override void Initialize()
        {
            base.Initialize();

            ObjectInfoBoxList = new List<DebugBox>();
            ObjectCounter = 0;
            AssetCounter = 0;
        }
        
        protected override void LoadContent()
        {
            if (this.content == null)
                this.content = new ContentManager(Game.Services, "Content");

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.debug_font = this.content.Load<SpriteFont>("Fonts/DebugFont");

            this.fpsBox = new DebugBox(new Vector2(10, 10), this.debug_font, true);
            this.fpsBox.LoadContent(GraphicsDevice, Game.Window.ClientBounds);

            this.resolutionBox = new DebugBox(new Vector2(Game.Window.ClientBounds.Width - 10, 10), this.debug_font, false);
            this.resolutionBox.LoadContent(GraphicsDevice, Game.Window.ClientBounds);

            this.objectCounterBox = new DebugBox(new Vector2(Game.Window.ClientBounds.Width - 10, Game.Window.ClientBounds.Height - 10), this.debug_font, false);
            this.objectCounterBox.LoadContent(GraphicsDevice, Game.Window.ClientBounds);

            this.assetCounterBox = new DebugBox(new Vector2(10, Game.Window.ClientBounds.Height - 10), this.debug_font, true);
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
            this.resolutionBox.Update("Resolution", string.Format("{0}x{1}", Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height));
            this.objectCounterBox.Update(string.Format("Objetos: {0}", ObjectCounter));
            this.assetCounterBox.Update(string.Format("Assets: {0}", AssetCounter));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            this.spriteBatch.Begin();

            this.fpsBox.Draw(gameTime, this.spriteBatch);
            this.resolutionBox.Draw(gameTime, this.spriteBatch);
            this.objectCounterBox.Draw(gameTime, this.spriteBatch);
            this.assetCounterBox.Draw(gameTime, this.spriteBatch);

            this.spriteBatch.End();
        }
    }
}
