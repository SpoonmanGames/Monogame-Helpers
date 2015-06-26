using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers.Polygon
{
    public class DrawablePoint : _2DPrimitiveShapes
    {
        private Vector2 point;

        public DrawablePoint(Vector2 punto, Color color) : base(color) { this.point = punto; }

        public DrawablePoint(Vector2 punto) : this(punto, Color.Black) { }

        public DrawablePoint(int X, int Y, Color color) : this(new Vector2(X, Y), color) { }

        public DrawablePoint(int X, int Y) : this(X, Y, Color.Black) { }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            base.LoadContent(graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, new Rectangle((int)point.X, (int)point.Y, 1, 1), this.Color * this.Alpha);
        }
    }
}
