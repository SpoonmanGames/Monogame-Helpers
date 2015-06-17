using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGame_Helpers.clases
{
    public class Layer
    {
        public Layer(Camera camera)
        {
            _camera = camera;
            Parallax = Vector2.One;
            Sprites = new List<SimpleSprite>();
        }

        public Vector2 Parallax { get; set; }
        public List<SimpleSprite> Sprites { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetViewMatrix(Parallax));
            foreach (SimpleSprite sprite in Sprites)
                sprite.Draw(spriteBatch);
            spriteBatch.End();
        }

        private readonly Camera _camera;
    }
}


/*IMPLEMENTACION*/
//_layers = new List<Layer>();
 
//// Create a camera instance and limit its moving range
//_camera = new Camera(GraphicsDevice.Viewport) { Limits = new Rectangle(0, 0, 3200, 600) };
 
//// Create 9 layers with parallax ranging from 0% to 100% (only horizontal)
//_layers = new List<Layer>
//{
//    new Layer(_camera) { Parallax = new Vector2(0.0f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(0.1f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(0.2f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(0.3f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(0.4f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(0.5f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(0.6f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(0.8f, 1.0f) },
//    new Layer(_camera) { Parallax = new Vector2(1.0f, 1.0f) }
//};
 
//// Add one sprite to each layer
//_layers[0].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer1") });
//_layers[1].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer2") });
//_layers[2].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer3") });
//_layers[3].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer4") });
//_layers[4].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer5") });
//_layers[5].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer6") });
//_layers[6].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer7") });
//_layers[7].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer8") });
//_layers[8].Sprites.Add(new Sprite { Texture = Content.Load<Texture2D>("Layer9") });
//foreach (Layer layer in _layers)
//    layer.Draw(_spriteBatch);
