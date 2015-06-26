using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers.Polygon
{
    public abstract class _2DPrimitiveShapes
    {
        public Texture2D Texture { get; set; }
        public Rectangle Rectangle { get; set; }
        public int Width { get { return this.Rectangle.Width; } }
        public int Height { get { return this.Rectangle.Height; } }
        public Color Color { get; set; }

        #region Constructor

        public _2DPrimitiveShapes(Rectangle rectangle, Color color){ this.Rectangle = rectangle; this.Color = color; }

        public _2DPrimitiveShapes(Point posicion, Point tamano, Color color) { this.Rectangle = new Rectangle(posicion, tamano); this.Color = color; }

        public _2DPrimitiveShapes(int x, int y, int width, int height, Color color) { this.Rectangle = new Rectangle(x, y, width, height); this.Color = color; }

        public _2DPrimitiveShapes(Rectangle rectangle) : this(rectangle, Color.Black){ }

        public _2DPrimitiveShapes(Point posicion, Point tamano) : this(posicion, tamano, Color.Black) { }

        public _2DPrimitiveShapes(int x, int y, int width, int height) : this(x, y, width, height, Color.Black) { }

        #endregion

        public virtual void LoadContent(GraphicsDevice graphicsDevice) {
            this.Texture = new Texture2D(graphicsDevice, 1, 1);
            this.Texture.SetData<Color>(new Color[] { Color.White });
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
