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

        public DebugOutput(Game game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            this.frame_rates = "TESTING GAME COMPONETS";//string.Empty;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(Game.Services, "Content");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            debug_font = content.Load<SpriteFont>("Fonts/DebugFont");

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
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Begin();

            spriteBatch.DrawString(debug_font, frame_rates, new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2), Color.Black);

            spriteBatch.End();
        }
    }
}
