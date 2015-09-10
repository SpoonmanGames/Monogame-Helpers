using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Xna.Framework.Content;
using Helpers.InputController;

namespace DebugManager.Outputs
{
    /// <summary>
    /// Clase que permite mostrar información de un Object usando
    /// dos DebugBox, uno para las propiedades y otro para los atributos (fields)
    /// </summary>
    public class DebugObject
    {
        public bool IsActive { get { return this.isActive; } set { this.isActive = value; } }
        private bool isActive;

        /// <summary>
        /// Obtiene el DebugManager que maneja esta DebugObject, de modo de acceder al contexto de ella.
        /// </summary>
        public DebugManager DebugManagerController
        {
            get { return debugManagerController; }
            internal set { debugManagerController = value; }
        }
        private DebugManager debugManagerController;

        private ContentManager content;
        private SpriteFont gameFont;

        private object obj;

        private DebugBox debugProperties;
        private List<string> propMessages;
        private DebugBox debugFields;
        private List<string> fieldMessages;
        private bool isControllingProperty;        

        public DebugObject(object obj)
        {
            this.obj = obj;
            this.propMessages = new List<string>();
            this.fieldMessages = new List<string>();
            this.isActive = true;
            this.isControllingProperty = true;
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public virtual void LoadContent() {
            if (this.content == null)
                this.content = new ContentManager(DebugManagerController.Game.Services, "Content");

            this.gameFont = this.content.Load<SpriteFont>("Fonts/gameFont");

            this.debugProperties = new DebugBox(new Vector2(300, 100), this.gameFont, true);
            this.debugProperties.DebugManagerController = DebugManagerController;
            this.debugProperties.LoadContent();

            this.debugFields = new DebugBox(new Vector2(1000, 500), this.gameFont, true);
            this.debugFields.DebugManagerController = DebugManagerController;
            this.debugFields.LoadContent();
        }

        /// <summary>
        /// Unload todo el contenido cargado por esta Screen
        /// </summary>
        public virtual void UnloadContent()
        {
            content.Unload();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public virtual void Update(GameTime gameTime, bool otherObjectHasFocus, bool coveredByOtherObject)
        {
            UpdateMessages();

            this.debugProperties.Update(this.propMessages.ToArray());
            this.debugFields.Update(this.fieldMessages.ToArray());
        }

        /// <summary>
        /// Actualiza los mensajes mostrados con la información actual del objecto
        /// </summary>
        [System.Diagnostics.Conditional("DEBUG")]
        private void UpdateMessages()
        {
            int i = -1;
            foreach (var prop in obj.GetType().GetRuntimeProperties())
            {
                ++i;
                if (this.propMessages.Count == i)
                {
                    this.propMessages.Add(string.Format("{0}.-{1}={2}", i, prop.Name, prop.GetValue(this)));
                }
                else
                {
                    this.propMessages[i] = string.Format("{0}.-{1}={2}", i, prop.Name, prop.GetValue(this));
                }
            }

            i = -1;
            foreach (var prop in obj.GetType().GetRuntimeFields())
            {
                ++i;
                if (this.fieldMessages.Count == i)
                {
                    this.fieldMessages.Add(string.Format("{0}.-{1}={2}", i, prop.Name, prop.GetValue(this)));
                }
                else
                {
                    this.fieldMessages[i] = string.Format("{0}.-{1}={2}", i, prop.Name, prop.GetValue(this));
                }
            }
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            this.debugProperties.Draw(gameTime, spriteBatch);
            this.debugFields.Draw(gameTime, spriteBatch);
        }

        #region HandleInput

        [System.Diagnostics.Conditional("DEBUG")]
        public void HandleInput(InputState input)
        {
            if (this.isActive)
            {
                if (input.IsLeftCtrl(null))
                {
                    if (input.IsUp(null))
                    {
                        MoveDebugBox(0, -10);
                    }

                    if (input.IsDown(null))
                    {
                        MoveDebugBox(0, 10);
                    }

                    if(input.IsLeft(null) || input.IsRight(null))
                    {
                        SetDebugBoxFocus(this.isControllingProperty);
                    }
                }
            }
        }

        #endregion

        #region Actions

        private void MoveDebugBox(int x, int y)
        {
            if (this.isControllingProperty)
            {
                this.debugProperties.X += x;
                this.debugProperties.Y += y;
            }
            else
            {
                this.debugFields.X += x;
                this.debugFields.Y += y;
            }
        }

        private void SetDebugBoxFocus(bool controllingProperty)
        {
            this.isControllingProperty = !controllingProperty;
        }

        #endregion
    }
}
