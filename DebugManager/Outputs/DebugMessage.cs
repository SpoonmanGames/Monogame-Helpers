using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugManager.Outputs
{
    public class DebugMessage
    {
        #region Properties

        #region Constantes

        const int SCALE_SPEED = 4;

        #endregion

        public string Text { get; set; }

        /// <summary>
        /// Obtiene y asigna la posición en la que será dibujado el Message.
        /// Esto es actualizado cada vez que update es llamado
        /// </summary>
        public Vector2 Position { get; set; }


        /// <summary>
        /// Guarda un acceso al DebugBox en la que este Message toma lugar
        /// </summary>
        internal DebugBox DebugBoxController { get; set; }

        /// <summary>
        /// Obtiene el espacio en altura que requiere este Message según el texto
        /// </summary>
        public int TextHeight
        {
            get { return DebugBoxController.DebugManagerController.Font.LineSpacing; }
            //get { return (int)DebugBoxController.DebugManagerController.Font.MeasureString(Text).Y; }
        }


        /// <summary>
        /// Obtiene el espacio en anchura que requiere este Message según el texto.
        /// Sirve para centrarlo.
        /// </summary>
        public int TextWidth
        {
            get { return (int)DebugBoxController.DebugManagerController.Font.MeasureString(Text).X; }
        }

        /// <summary>
        /// Realiza un efecto de FADE durante un evento del Message.
        /// </summary>
        private float SelectionFade { get; set; }

        /// <summary>
        /// Determina la scala del texto según su tamaño dado por la Font
        /// </summary>
        private Vector2 TextScale { get; set; }


        #endregion

        #region Events

        /// <summary>
        /// El evento se activa cuando el DebugMessage es seleccionado.
        /// </summary>
        public event EventHandler SwitchShowed;

        /// <summary>
        /// Methodo para activar el Selected Event.
        /// </summary>
        protected internal virtual void OnSwitchShowedEntry(EventArgs e)
        {
            if (SwitchShowed != null)
                SwitchShowed(this, e);
        }

        #endregion

        #region Constructor


        /// <summary>
        /// Crea un DebugMessage con el texto especificado
        /// </summary>
        /// <param name="debugBox">DebugBox donde toma contexto este Message.</param>
        /// <param name="text">Texto a escribir en el entry</param>
        public DebugMessage(DebugBox debugBox, string text)
        {
            DebugBoxController = debugBox;
            Text = text;
            TextScale = new Vector2(0, 1);
        }

        #endregion

        #region Update and Draw


        /// <summary>
        /// Mantiene actualizada el Message en el Juego
        /// </summary>
        /// <param name="gameTime">Para obtener el tiempo del juego</param>
        /// <param name="text">Texto a mostrar.</param>
        /// <param name="isShowed">Para saber si se está mostrando o no.</param>
        public virtual void Update(GameTime gameTime, string text, bool isShowed)
        {
            Text = text;

            float showSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * SCALE_SPEED;

            if (isShowed)
                TextScale = new Vector2(Math.Min(SelectionFade + showSpeed, 1), 1);
            else
                TextScale = new Vector2(Math.Max(SelectionFade - showSpeed, 0), 1);
        }


        /// <summary>
        /// Dibuja el Message. Puede realizarce un override de este metodo para mejorar la apariencia
        /// </summary>
        /// <param name="gameTime">Para obtener el tiempo del juego</param>
        /// <param name="isSelected">Booleano para saber si está seleccionada o no</param>
        public virtual void Draw(GameTime gameTime, bool isSelected)
        {
            //Si está seleccionada le da un color amarillo, si no, es blanco
            Color color = isSelected ? Color.Yellow : Color.White;

            //Modifica el alpha del Message de acuerdo al estado de transición del DebugBox al que pertenece
            color *= DebugBoxController.TransitionAlpha;

            //Dibuja el texto centrado en la Screen
            DebugManager debugManager = DebugBoxController.DebugManagerController;
            SpriteBatch spriteBatch = debugManager.SpriteBatch;
            SpriteFont font = debugManager.Font;

            spriteBatch.DrawString(font, Text, Position, color, 0, Vector2.Zero, TextScale, SpriteEffects.None, 0);
        }

        #endregion
    }
}
