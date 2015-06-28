using Helpers.Polygon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.ExtensionMethods.Vector2Ext;

namespace Helpers.DebugOutput
{
    class DebugBox
    {
        private List<string> messages;
        private DrawableRectangle rect_background;
        private Vector2 rect_position;
        private SpriteFont Font;
        private bool left_corner;
        private Vector2 msg_bounds;
        private const int POS_OFFSET = 10;
        private const float RECT_ALPHA = 0.5f;
        private const int LINE_SPACING = 5;

        public DebugBox(Vector2 position, SpriteFont font, bool left_corner, params string[] messages)
        {
            this.rect_position = position;
            this.Font = font;
            this.left_corner = left_corner;
            this.msg_bounds = Vector2.Zero;
            this.messages = messages.ToList();          
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            this.msg_bounds.X = this.messages.Max(s => this.Font.MeasureString(s).X);
            this.msg_bounds.Y = this.Font.MeasureString(this.messages[0]).Y;

            if (!this.left_corner)
                this.rect_position = this.rect_position - new Vector2(this.msg_bounds.X + 10, 0);

            this.rect_background = new DrawableRectangle(
                this.rect_position,
                (int)this.msg_bounds.X + POS_OFFSET,
                (int)(this.messages.Count * (LINE_SPACING + this.msg_bounds.Y) - LINE_SPACING + POS_OFFSET)
            );
            this.rect_background.Alpha = RECT_ALPHA;
            this.rect_background.LoadContent(graphicsDevice);
        }

        public void Update(params string[] messages)
        {
            List<string> input_msgs = messages.ToList();

            if (!Enumerable.SequenceEqual(this.messages, input_msgs))
            {
                this.messages = input_msgs;

                this.msg_bounds.X = this.messages.Max(s => this.Font.MeasureString(s).X);
                this.msg_bounds.Y = this.Font.MeasureString(this.messages[0]).Y;

                this.rect_background.Update(new Rectangle(
                    (int)this.rect_position.X,
                    (int)this.rect_position.Y,
                    (int)this.msg_bounds.X + POS_OFFSET,
                    (int)(this.messages.Count * (LINE_SPACING + this.msg_bounds.Y) - LINE_SPACING + POS_OFFSET)
                    )
                );                
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.rect_background.Draw(gameTime, spriteBatch);

            for (int i = 0; i < this.messages.Count; i++)
            {
                spriteBatch.DrawString(
                    this.Font,
                    this.messages[i], 
                    this.rect_position.Add(POS_OFFSET / 2.0f) + new Vector2(0, i*(msg_bounds.Y + LINE_SPACING)),
                    Color.White
                );
            }
        }
    }
}
