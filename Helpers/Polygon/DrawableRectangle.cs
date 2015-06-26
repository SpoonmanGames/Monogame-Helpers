using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers.Polygon
{
    public class DrawableRectangle : _2DPrimitiveShapes
    {
        public DrawableRectangle(Rectangle rectangle, Color color) : base(rectangle, color) { }

        public DrawableRectangle(Rectangle rectangle) : base(rectangle) { }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            base.LoadContent(graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Rectangle, this.Color);
        }
    }
}
