using Helpers.InputController;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugManager.Outputs
{
    /// <summary>
    /// Enum para saber el estado de transición del Output
    /// <list type="bullet">
    /// <item> 
    /// <description>TransitionOn: Cuando el Output está comenzando a verse.</description> 
    /// </item> 
    /// <item> 
    /// <description>Active: Cuando el Output está activo.</description> 
    /// </item>
    /// <item> 
    /// <description>TransitionOff: Cuando el Output comienza a irse.</description> 
    /// </item>
    /// <item> 
    /// <description>Hidde: Cuando el Output se ha ido pero sigue cargada para volver a aparecer.</description> 
    /// </item>
    /// </list>
    /// </summary>
    public enum OutputState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    /// <summary>
    /// Un DebugOut es una instancia base para todos los outputs mostrados por el DebugManager,
    /// define todo lo escensial para el coportamiento de estos en la pantalla.
    /// Se puede combinar con otros tipos de outputs heredades desde esta clase.
    /// </summary>
    public abstract class DebugOutput
    {
        #region Properties

        #region Control

        /// <summary>
        /// Obtiene el estado actual de la transición del Output.
        /// </summary>
        public OutputState OutputState
        {
            get { return outputState; }
            protected set { outputState = value; }
        }
        private OutputState outputState = OutputState.TransitionOn;

        /// <summary>
        /// Un Output puede TransitionOff por dos razones:
        /// Puede quedar en standy by cuando se oculta para mostrar otro Output.
        /// O puede ser que no sea útil y se esté desechando, por lo que debe ser eliminada despues
        /// de hacer TransitionOff.
        /// Si esta variable es true, el Output será removido según el segundo caso enunciado.
        /// </summary>
        public bool IsExiting
        {
            get { return isExiting; }
            protected internal set { isExiting = value; }
        }
        private bool isExiting = false;

        /// <summary>
        /// Revisa si este Output es el Output activo en este momento,
        /// por lo que el usuario está interactuando con ella.
        /// </summary>
        public bool IsActive
        {
            get
            {
                /* Si ninguna otra Output tiene el foco y
                 * la Output está actualmente "apareciendo" (TransitionOn) o
                 * la Output está actualmente activa
                 */
                return !otherOutputHasFocus &&
                    (outputState == OutputState.TransitionOn ||
                    outputState == OutputState.Active);
            }
        }
        private bool otherOutputHasFocus;

        #endregion

        #region Transición

        /// <summary>
        /// Indica cuanto tiempo le toma hacer TransitionOn
        /// cuando el Output ha sido activado.
        /// </summary>
        public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            protected set { transitionOnTime = value; }
        }
        private TimeSpan transitionOnTime = TimeSpan.Zero;


        /// <summary>
        /// Indica cuanto tiempo le toma hacer TransitionOff
        /// cuando el Output ha sido desactivado
        /// </summary>
        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            protected set { transitionOffTime = value; }
        }
        private TimeSpan transitionOffTime = TimeSpan.Zero;


        /// <summary>
        /// Obtiene la posición/estado actual de la transición del Output.
        /// Va de 0(output activo sin transición) a 1(transición en su maximo punto).
        /// </summary>
        public float TransitionPosition
        {
            get { return transitionPosition; }
            protected set { transitionPosition = value; }
        }
        private float transitionPosition = 1;


        /// <summary>
        /// Obtiene el alpha(nivel de transparencia) del Output.
        /// Va de 1(output activa sin transición) a 0(Output en su máximo punto)
        /// </summary>
        public float TransitionAlpha
        {
            get { return 1f - TransitionPosition; }
        }

        #endregion

        #region Referencias a otros Objetos        

        /// <summary>
        /// Obtiene el DebugManager que maneja este Output, de modo de acceder al contexto de ella.
        /// </summary>
        public DebugManager DebugManagerController
        {
            get { return debugManagerController; }
            internal set { debugManagerController = value; }
        }
        private DebugManager debugManagerController;

        #endregion

        #endregion

        #region Methods To Override

        #region Load & Unload Content

        /// <summary>
        /// Carga los graphics content para el Output
        /// </summary>
        public virtual void LoadContent() { }

        /// <summary>
        /// Descarga los graphics content para el Output
        /// </summary>
        public virtual void UnloadContent() { }

        #endregion

        #region HandleInput & Draw

        /// <summary>
        /// Permite que el Output puedan usar diferentes input.
        /// Este metodo debe ser desactivado para los Output cuando no esté activada(IsActive == false)
        /// </summary>
        /// <param name="input">Clase input que permite mapear el teclado.</param>
        public virtual void HandleInput(InputState input) { }

        /// <summary>
        /// Llamada para dibujar el Output en pantalla
        /// </summary>
        /// <param name="gameTime">Clase que guarda dinamicamente el tiempo transcurrido.</param>
        public virtual void Draw(GameTime gameTime) { }

        #endregion

        #endregion

        #region Update & UpdateTransition

        /// <summary>
        /// Permite que el Output se actualice, actualiza las transiciones On y Off.
        /// Este metodo es siempre llamado, de forma que la pantalla se mantenga en contexto
        /// en cualquier estado en el que se encuentre (Active, Hidde o Transition)
        /// </summary>
        /// <param name="gameTime">Tiempo del juego</param>
        /// <param name="otherScreenHasFocus">boleano para determinar si esta pantalla tiene el foco o no</param>
        /// <param name="coveredByOtherScreen">boleano para determinar si está siendo tapada por otra Screen</param>
        public virtual void Update(GameTime gameTime, bool otherOutputHasFocus, bool coveredByOtherOutput)
        {
            //Actualiza si la Screen tiene el foco en ese momento o no
            this.otherOutputHasFocus = otherOutputHasFocus;

            if (IsExiting)
            {
                //La Screen morirá, por lo que debe tener una transición de salida
                this.outputState = OutputState.TransitionOff;

                //Cuando la transición termine se debe remover la Screen
                if (!UpdateTransition(gameTime, this.transitionOffTime, OutputState.TransitionOff))
                {
                    DebugManagerController.RemoveOutput(this);
                }
            }
            //Si la pantalla es tapada por otra, debe hacer transitionOff
            else if (coveredByOtherOutput)
            {
                if (UpdateTransition(gameTime, this.transitionOffTime, OutputState.TransitionOff))
                {
                    this.outputState = OutputState.TransitionOff;
                }
                //Terminó el TransitionOff
                else
                {
                    this.outputState = OutputState.Hidden;
                }
            }
            //En este caso la screen debe hacer un TransitionOn y volverse activa
            else
            {
                if (UpdateTransition(gameTime, this.transitionOnTime, OutputState.TransitionOn))
                {
                    this.outputState = OutputState.TransitionOn;
                }
                //Terminó el TransitionOn
                else
                {
                    this.outputState = OutputState.Active;
                }
            }
        }

        /// <summary>
        /// Actualiza la posición de la transición, true si aún se está realizando la transición.
        /// </summary>
        /// <param name="gameTime">Tiempo del juego</param>
        /// <param name="time">Intervalo de tiempo para la transición</param>
        /// <param name="outputState">
        /// OutputState que se desea realizar, solo acepta TransitionOn y TransitionOff,
        /// si se entrega otro tipo de OutputState se asumirá TransitionOff.
        /// </param>
        /// <returns>Retorna verdadero si aun no termina de actualizarse o false en caso contrario.</returns>
        bool UpdateTransition(GameTime gameTime, TimeSpan time, OutputState outputState)
        {
            //Define la dirección
            int direction = 1;
            if(outputState == OutputState.TransitionOn)
                direction *= -1;               

            //Guarda cuando queda para realizar la transición
            float transitionDelta;

            if (time == TimeSpan.Zero)
                transitionDelta = 1;
            else
                //GameTime.ElapsedGametime tiene el tiempo transcurrido en cada update/frame
                //Esto dividido por el tiempo total que debe tomar la transición nos da el rango de avance del efecto
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds /
                                          time.TotalMilliseconds);

            //Actualiza la posición de transición en la dirección dada
            this.transitionPosition += transitionDelta * direction;

            //Comprueba si se ha terminado de realizar la transición
            if (((direction < 0) && (this.transitionPosition <= 0)) ||
                ((direction > 0) && (this.transitionPosition >= 1)))
            {
                //Fija transitionPosition entre 0 y 1 en caso de que haya superado estos limites
                this.transitionPosition = MathHelper.Clamp(this.transitionPosition, 0, 1);
                return false;
            }

            //Retorna verdadero si aún no ha terminado
            return true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Le dice al Output que debe irse. A diferencia de DebugManagerContorller.RemoveScreen
        /// este método realiza una transitionOff antes de eliminar el Output. No la elimina
        /// inmediatamente.
        /// </summary>
        public void ExitOutput()
        {
            //Si el tiempo de transición del Output es 0 se elimina inmediatamente
            if (TransitionOffTime == TimeSpan.Zero)
            {
                DebugManagerController.RemoveOutput(this);
            }
            //Sino, se asigna true a IsExiting, para que se realice la transición adecuada
            else
            {
                IsExiting = true;
            }
        }

        #endregion
        

        
    }
}
