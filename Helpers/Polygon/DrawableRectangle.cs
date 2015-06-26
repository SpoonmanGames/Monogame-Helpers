using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers.Polygon
{
    /// <summary>
    /// TODO: 
    ///     - angle de rotación
    ///     - Revisar que pasa cuando X < Y  
    /// </summary>
    public class DrawableRectangle : _2DPrimitiveShapes
    {
        private Rectangle rectangle;
        bool isLinebase;
        DrawableLine linebase_rectangle;

        public DrawableRectangle(Rectangle rectangle, Color color)
            : base(color) { this.rectangle = rectangle; this.isLinebase = false; }

        public DrawableRectangle(Vector2 X, Vector2 Y, Color color)
            : base(color) { this.isLinebase = true; this.linebase_rectangle = new DrawableLine(X, Y, color); this.linebase_rectangle.Weigth = (int)(Y.Y - X.Y); }

        public DrawableRectangle(Rectangle rectangle)
            : this(rectangle, Color.Black) { }

        public DrawableRectangle(Vector2 position, int weigth, int height, Color color)
            : this(new Rectangle((int)position.X, (int)position.Y, weigth, height), color) { }

        public DrawableRectangle(Vector2 position, int weigth, int height)
            : this(position, weigth, height, Color.Black) { }

        public DrawableRectangle(Vector2 X, Vector2 Y)
            : this(X, Y, Color.Black) { }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            if (this.isLinebase)
                this.linebase_rectangle.LoadContent(graphicsDevice);
            else
                base.LoadContent(graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (this.isLinebase)
                this.linebase_rectangle.Draw(gameTime, spriteBatch);
            else
                spriteBatch.Draw(this.Texture, this.rectangle, this.Color * this.Alpha);
        }
    }
}
