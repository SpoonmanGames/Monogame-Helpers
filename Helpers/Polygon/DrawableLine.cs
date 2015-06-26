using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Helpers.Polygon
{
    /// <summary>
    /// TODO:
    ///     - Revisar que pasa cuando X < Y
    /// </summary>
    public class DrawableLine : _2DPrimitiveShapes
    {
        private float size;
        private float angle;
        private Vector2 from;
        private Vector2 to;
        public int Weigth { get; set; }

        public DrawableLine(Vector2 from, Vector2 to, Color color)
            : base(color) { this.size = 0; this.angle = 0; this.from = from; this.to = to; this.Weigth = 1; }

        public DrawableLine(Vector2 from, Vector2 to)
            : this(from, to, Color.Black) { }

        public override void LoadContent(GraphicsDevice graphicsDevice)
        {
            base.LoadContent(graphicsDevice);

            this.size = Vector2.Distance(from, to);
            this.angle = (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.from, null, this.Color * this.Alpha, this.angle, Vector2.Zero, new Vector2(this.size, (float)this.Weigth), SpriteEffects.None, 0);
        }
    }
}
