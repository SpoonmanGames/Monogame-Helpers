using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.DebugOutput
{
    public class DebugObject
    {        
        public DebugObject() { }

        [System.Diagnostics.Conditional("DEBUG")]
        public virtual void LoadContent(GraphicsDevice graphicsDevice, Rectangle ClientBounds) { }

        [System.Diagnostics.Conditional("DEBUG")]
        public virtual void Update(params string[] messages) { }

        [System.Diagnostics.Conditional("DEBUG")]
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }
    }
}
