using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ScreenManager.StateControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGame_Helpers
{
    class PantallaSaltoVertical:GameScreen
    {
        ContentManager content;
        SpriteFont gameFont;
        SpriteBatch spriteBatch;
        Texture2D bola;
        //posicion  de la bola en (x,y)
        Vector2 bolapos = Vector2.Zero;
        

        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManagerController.Game.Services, "Content");

            bola = content.Load<Texture2D>("Texturas/ball");




            bolapos.X = ScreenManagerController.Game.Window.ClientBounds.Width / 2 - bola.Width / 2; //bola en el medio
            bolapos.Y = ScreenManagerController.Game.Window.ClientBounds.Height - bola.Height; //bola en piso

        }

        public override void UnloadContent()
        {
            content.Unload();
        }


        double vi, t = 0; // vi - velocidad inicial | t - tiempo
        double g = 1500; // pixeles por segundo cuadrado| aceleracion grabitacional.
        int keyState = 0;
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //this.Exit();//solo para windows phono, deprecado para windows8.1
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                keyState = 1; vi = -1500; // velocidad inicial en 820 pixels
            }
            if (keyState == 1)
            {
                //el valor normal de la formula es vi * t - g * t^2 / 2 y vi es positivo,  pero hacia arriba  decrece Y, hacia abajo aumenta Y
                bolapos.Y = (float)(vi * t + g * t * t / 2) + ScreenManagerController.Game.Window.ClientBounds.Height - bola.Height;
                t = t + gameTime.ElapsedGameTime.TotalSeconds; //Calcula el tiempo que la bola deja el suelo
            }
            if (bolapos.Y > ScreenManagerController.Game.Window.ClientBounds.Height - bola.Height) //No permitir que la bola vaya mas allá del limite inferior de la pantalla(piso)
            {
                bolapos.Y = ScreenManagerController.Game.Window.ClientBounds.Height - bola.Height;
                keyState = 0;
                t = 0;
            }
            base.Update(gameTime, otherScreenHasFocus, false);
        }


        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManagerController.SpriteBatch;
            
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteBatch.Draw(bola, bolapos, Color.White);
            spriteBatch.End();
        }

        public override void HandleInput(InputState input)
        {
            
        }


    }
}
