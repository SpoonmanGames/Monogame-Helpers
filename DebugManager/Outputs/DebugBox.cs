using Helpers.Polygon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.ExtensionMethods.Vector2Ext;
using Helpers.ExtensionMethods.RectangleExt;
using Helpers.InputController;

namespace DebugManager.Outputs
{
    /// <summary>
    /// Permite dibujar un cuadrado negro con transparencia el cual contendrá y mostrará
    /// mensajes pasados por parametros.
    /// Usado para mostrar por pantalla toda la información del DebugManager.
    /// </summary>
    public class DebugBox : DebugOutput
    {
        #region Fields

        #region Constantes

        private const int POS_OFFSET = 10;
        private const float RECT_ALPHA = 0.5f;
        private const int LINE_SPACING = 5;

        #endregion

        /// <summary>
        /// Título del DebugBox
        /// </summary>
        private string boxTitle;

        /// <summary>
        /// Lista de mensajes a mostrar 
        /// </summary>
        private List<DebugMessage> messages = new List<DebugMessage>();

        /// <summary>
        /// Rectangulo de fondo negro donde irán todos los mensajes.
        /// </summary>
        private DrawableRectangle rect_background;

        /// <summary>
        /// Posicion y dimensión del DebugBox.
        /// </summary>
        private Rectangle rect_position;

        /// <summary>
        /// Permite saber si la posición dada corresponde a la esquina sup izq o der.
        /// </summary>
        private bool left_corner;

        /// <summary>
        /// Vector que guarda el ancho y el alto máximo que tendrá un mensaje.
        /// </summary>
        private Vector2 msg_bounds;

        /// <summary>
        /// Guarda la posición inicial dada al DebugBox.
        /// </summary>
        private Vector2 posicionInicial;

        /// <summary>
        /// Guarda el último ClientBound para poder mantener el DebugBox
        /// y mantener el Output en una posición propocional a la inicialmente dada
        /// </summary>
        private Rectangle LastClientBounds;


        #endregion

        #region Properties

        /// <summary>
        /// Para identificar cual MenuEntry está siendo seleccionado en este momento
        /// </summary>
        public int SelectedMessage { get { return this.selectedMessage; } set { this.selectedMessage = value; } }
        private int selectedMessage = 0;

        /// <summary>
        /// Para obtener o asignar la posición X del DebugBox general
        /// </summary>
        public int X { 
            get { return this.rect_position.X; }
            set {
                this.posicionInicial.X = value;
                this.rect_position.X = value; 
            }
        }

        /// <summary>
        /// Para obtener o asignar la posición Y del DebugBox general
        /// </summary>
        public int Y
        {
            get { return this.rect_position.Y; }
            set
            {
                this.posicionInicial.Y = value;
                this.rect_position.Y = value;
            }
        }

        /// <summary>
        /// Obtiene la lista de Messages para poder agregar o quitar mensajes
        /// según sea necesario
        /// </summary>
        protected IList<DebugMessage> Messages
        {
            get { return this.messages; }
        }

        /// <summary>
        /// Obtiene el total de mensajes en el Debugbox
        /// </summary>
        public int MessagesCount {
            get { return this.messages.Count; } 
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor del DebugBox, inicializa las variables principales
        /// </summary>
        /// <param name="position">Posición de la esquina superior izquierda o derecha de la DebugBox.</param>
        /// <param name="font">Font a usar para los mensajes.</param>
        /// <param name="left_corner">Define si la posición dada es respecto a la esquina superior izquierda o no.</param>
        public DebugBox(string boxTitle, Vector2 position, bool left_corner = true)
        {

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            this.boxTitle = boxTitle;
            this.rect_position = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            this.posicionInicial = position;
            this.left_corner = left_corner;
            this.msg_bounds = Vector2.Zero;
            this.messages.Add(new DebugMessage(this, string.Empty));            
        }

        #endregion

        #region HandleInput

        /// <summary>
        /// Reacciona al input del usuario, cambiando entre Messages y aceptando
        /// eventos.
        /// </summary>
        /// <param name="input">Input de los controles</param>
        public override void HandleInput(InputState input)
        {

        }

        #endregion

        #region LoadContent

        /// <summary>
        /// Carga el contenido gráfico y define el valor de las variables principales.
        /// </summary>
        [System.Diagnostics.Conditional("DEBUG")]
        public void LoadContent()
        {
            this.LastClientBounds = DebugManagerController.Game.Window.ClientBounds;
            this.UpdateDebugBoxBounds();

            this.rect_background = new DrawableRectangle(
                this.rect_position
            );
            this.rect_background.Alpha = RECT_ALPHA;
            this.rect_background.LoadContent(DebugManagerController.Game.GraphicsDevice);
        }

        #endregion

        #region Update & Draw

        /// <summary>
        /// Actualiza la posición y los mensajes mostrados
        /// </summary>
        /// <param name="ClientBounds">Fronteras o limites de la pantalla en el momento actual.</param>
        /// <param name="messages">Variable opcional, si este valor es distinto a lo que se tenía antes se actualizará el mensaje mostrado.</param>
        [System.Diagnostics.Conditional("DEBUG")]
        public void Update(GameTime gameTime, bool otherOutputHasFocus, bool coveredByOtherOutput, params string[] messages)
        {
            base.Update(gameTime, otherOutputHasFocus, coveredByOtherOutput);

            List<string> input_msgs = messages.ToList();

            if (!this.LastClientBounds.Compare(DebugManagerController.Game.Window.ClientBounds)
                || !Enumerable.SequenceEqual(this.messages, input_msgs))
            {
                this.LastClientBounds = DebugManagerController.Game.Window.ClientBounds;
                this.rect_position.X = (int)this.posicionInicial.X;
                this.rect_position.Y = (int)this.posicionInicial.Y;
                this.messages = input_msgs;

                this.UpdateDebugBoxBounds();

                this.rect_background.Update(this.rect_position);
            }
        }

        private void UpdateDebugBoxBounds()
        {
            this.msg_bounds.X = this.messages[0].TextWidth;
            this.msg_bounds.Y = this.messages[0].TextHeight;

            this.rect_position.Width = (int)this.msg_bounds.X + POS_OFFSET;

            if (!this.left_corner)
                this.rect_position.X -= this.rect_position.Width;

            if (this.rect_position.Right > this.LastClientBounds.Width - POS_OFFSET)
                this.rect_position.X = this.LastClientBounds.Width - POS_OFFSET - this.rect_position.Width;

            this.rect_position.Height = (int)(this.messages.Count * (LINE_SPACING + this.msg_bounds.Y) - LINE_SPACING + POS_OFFSET);

            if (this.rect_position.Bottom > this.LastClientBounds.Height - POS_OFFSET)
                this.rect_position.Y = this.LastClientBounds.Height - POS_OFFSET - this.rect_position.Height;
        }

        /// <summary>
        /// Dibuja el DebugBox según los datos dados
        /// </summary>
        /// <param name="gameTime">Para obtener el tiempo de juego.</param>
        /// <param name="spriteBatch">Para dibujar el DebugBox.</param>
        [System.Diagnostics.Conditional("DEBUG")]
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.rect_background.Draw(gameTime, spriteBatch);

            for (int i = 0; i < this.messages.Count; i++)
            {
                bool isSelected = IsActive && (i == this.selectedMessage);

                this.messages[i].Draw(gameTime, isSelected);

                //spriteBatch.DrawString(
                    //this.Font,
                    //this.messages[i].Text, 
                    this.rect_position.ToVector2P().Add(POS_OFFSET / 2.0f).AddToY(i*(msg_bounds.Y + LINE_SPACING)),
                    //Color.White
                //);
            }
        }

        #endregion
    }
}
