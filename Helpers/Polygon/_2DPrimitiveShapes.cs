using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Helpers.Polygon
{
    public abstract class _2DPrimitiveShapes
    {
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }

        #region Constructor

        public _2DPrimitiveShapes(Color color) { this.Texture = null; this.Color = color; this.Alpha = 1.0f; }

        public _2DPrimitiveShapes() : this(Color.Black) { }

        #endregion

        public virtual void LoadContent(GraphicsDevice graphicsDevice) {
            this.Texture = new Texture2D(graphicsDevice, 1, 1);
            this.Texture.SetData<Color>(new Color[] { Color.White });
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
