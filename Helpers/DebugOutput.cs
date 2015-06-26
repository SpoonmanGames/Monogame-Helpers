using Helpers.Polygon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class DebugOutput : DrawableGameComponent
    {
        ContentManager content;
        SpriteBatch spriteBatch;

        SpriteFont debug_font;
        string frame_rates;

        DrawableRectangle rectangle;

        public DebugOutput(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            this.frame_rates = string.Empty;
            this.rectangle = new DrawableRectangle(new Rectangle(10, 10, 100, 100));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Game.Services, "Content");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            debug_font = content.Load<SpriteFont>("Fonts/DebugFont");
            this.rectangle.LoadContent(GraphicsDevice);

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

            frame_rates = string.Format("FPS: {0}", 1000 / gameTime.ElapsedGameTime.Milliseconds);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawString(debug_font, frame_rates, new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2), Color.Black);
            rectangle.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
