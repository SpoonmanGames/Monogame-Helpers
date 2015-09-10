using Helpers.Polygon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers.ExtensionMethods.Vector2Ext;
using DebugManager.Outputs;
using Helpers.InputController;

namespace DebugManager
{
    public class DebugManager : DrawableGameComponent
    {
        #region Fields

        //lista de objetos contenidos en el DebugManager
        private List<DebugObject> objects = new List<DebugObject>();

        //lista de objetos a actualizar y dibujar
        private List<DebugObject> objectsToUpdate = new List<DebugObject>();

        //Para tener un mapa de los controles en cada pantalla
        InputState input = new InputState();

        //DebugBox que mostraran información elemental y básica
        private DebugBox fpsBox;
        private DebugBox resolutionBox;
        private DebugBox objectCounterBox;
        private DebugBox assetCounterBox;

        #endregion

        #region Properties

        /// <summary>
        /// Un SpriteBatch por defecto que será compartido para todos los objects.
        /// Evita que cada object tenga que crear su propia instancia local.
        /// </summary>
        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// Un Font por defecto compartido para todos los objects.
        /// Evita que cada object tenga que crear su propia instancia local.
        /// </summary>
        public SpriteFont Font { get; private set; }

        /// <summary>
        /// Permite saber si DebugManager se ha inicializado y cargado sus componentes graficos.
        /// </summary>
        public bool IsInitialized { get; private set; }

        public static int ObjectCounter { get; set; }

        public static int AssetCounter { get; set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Constructor del DebugManager
        /// </summary>
        /// <param name="game">Instancia del juego donde tendrá lugar este DebugManager</param>
        public DebugManager(Game game)
            : base(game)
        {
        }

        /// <summary>
        /// Inicializa los componentes del DebugManager
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            ObjectCounter = 0;
            AssetCounter = 0;
            IsInitialized = true;
        }

        /// <summary>
        /// Carga los componente graficos por defecto para los demás Object
        /// </summary>
        protected override void LoadContent()
        {
            //Para cargar el contenido que es exclusivo del DebugManager
            ContentManager content = Game.Content;

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = content.Load<SpriteFont>("Fonts/DebugFont");

            this.fpsBox = new DebugBox(new Vector2(10, 10), Font, true);
            this.fpsBox.LoadContent();

            this.resolutionBox = new DebugBox(new Vector2(Game.Window.ClientBounds.Width - 10, 10), Font, false);
            this.resolutionBox.LoadContent();

            this.objectCounterBox = new DebugBox(new Vector2(Game.Window.ClientBounds.Width - 10, Game.Window.ClientBounds.Height - 10), Font, false);
            this.objectCounterBox.LoadContent();

            this.assetCounterBox = new DebugBox(new Vector2(10, Game.Window.ClientBounds.Height - 10), Font, true);
            this.assetCounterBox.LoadContent();

            //Hace que cada uno de los objects contenidos carguen sus content
            foreach (DebugObject obj in objects)
            {
                obj.LoadContent();
            }
        }

        /// <summary>
        /// Descarga el contenido grafico de todas los Object contenidos
        /// </summary>
        protected override void UnloadContent()
        {
            foreach (DebugObject obj in objects)
            {
                obj.UnloadContent();
            }
        }

        #endregion

        #region Update & Draw

        public override void Update(GameTime gameTime)
        {
            //Lee el input para los object
            this.input.Update();

            //Limpia la lista y la vuelve a llenar
            //Esto es para evitar errores de sincronización entre las listas
            objectsToUpdate.Clear();
            foreach (DebugObject obj in objects)
                objectsToUpdate.Add(obj);

            //Actualiza los DebugBox básicos
            this.fpsBox.Update(string.Format("FPS: {0}", 1000 / gameTime.ElapsedGameTime.Milliseconds));
            this.resolutionBox.Update("Resolution", string.Format("{0}x{1}", Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height));
            this.objectCounterBox.Update(string.Format("Objetos: {0}", ObjectCounter));
            this.assetCounterBox.Update(string.Format("Assets: {0}", AssetCounter));

            //Setea de forma general los booleanos de control
            bool otherObjectHasFocus = !Game.IsActive;
            bool coveredByOtherObject = false;

            while (objectsToUpdate.Count > 0)
            {
                DebugObject obj = objectsToUpdate[objectsToUpdate.Count - 1];
                objectsToUpdate.RemoveAt(objectsToUpdate.Count - 1);

                obj.Update(gameTime, otherObjectHasFocus, coveredByOtherObject);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    //Si esta pantalla es la que tiene el foco
                    //Le permite manejar el input
                    if (!otherObjectHasFocus)
                    {
                        obj.HandleInput(input);

                        otherObjectHasFocus = true;
                    }

                    // Se le avisa a los otros DebugObj que serán tapadas por este Object
                    coveredByOtherObject = true;
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();

            this.fpsBox.Draw(gameTime, this.spriteBatch);
            this.resolutionBox.Draw(gameTime, this.spriteBatch);
            this.objectCounterBox.Draw(gameTime, this.spriteBatch);
            this.assetCounterBox.Draw(gameTime, this.spriteBatch);

            SpriteBatch.End();
        }

        #endregion
    }
}
