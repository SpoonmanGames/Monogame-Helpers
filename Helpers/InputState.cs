using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Helpers.InputController
{
    /// <summary>
    /// Lee el input del teclado y del Gamepad manteniendo un estado
    /// actual del controller y el estado previo. También controla las acciones
    /// generales como por ejemplo "PAUSE" o moverse por el menú
    /// <remarks>Esta clase permite manejar Inputs ya sea por teclado o por GamePad.</remarks>
    /// </summary>
    public class InputState
    {
        #region Properties

        /// <summary>
        /// Máxima cantidad de controles que pueden jugar
        /// </summary>
        public int MaxInputs
        {
            get { return maxInputs; }
        }
        private const int maxInputs = 4;

        /// <summary>
        /// Estado actual de los contorles del teclado
        /// </summary>
        public KeyboardState[] CurrentKeyboardStates { get; private set; }

        /// <summary>
        /// Estado actual de los controles del GamePad
        /// </summary>
        public GamePadState[] CurrentGamePadStates { get; private set; }

        /// <summary>
        /// Esado previo de los controles del teclado
        /// </summary>
        public KeyboardState[] LastKeyboardStates { get; private set; }

        /// <summary>
        /// Estado previo de los controles del Gamepad
        /// </summary>
        public GamePadState[] LastGamePadStates { get; private set; }

        /// <summary>
        /// Permite saber si el gamepad de ese jugar está conectado o no
        /// </summary>
        public bool[] GamePadWasConnected { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa los controladores para los controles
        /// </summary>
        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];

            GamePadWasConnected = new bool[MaxInputs];
        }

        #endregion

        #region Update & NewKeys

        /// <summary>
        /// Actualiza el Controler State de todos los jugadores.
        /// También detecta si hay o no un GamePad Conectado
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState();
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                // Permite saber cuando un control ha sido conectado o desconectado
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }
        }

        /// <summary>
        /// Detecta si una nueva tecla ha sido "clickeada"
        /// </summary>
        /// <param name="key">Key a presionar</param>
        /// <param name="controllingPlayer">Player que realizará la acción, si es null todos los player pueden</param>
        /// <param name="playerIndex">PlayerIndex para guardar el index del jugador que realizó la acción</param>
        /// <returns>Bool de que la tecla especificada ha sido presinada</returns>
        private bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            //Si hay un player controlando las teclas
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key) &&
                        LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                //Acepta el input de cualquier player
                return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Detecta si una nueva tecla ha sido "clickeada"
        /// </summary>
        /// <param name="key">Key a presionar</param>
        /// <param name="controllingPlayer">Player que realizará la acción, si es null todos los player pueden</param>
        /// <param name="playerIndex">PlayerIndex para guardar el index del jugador que realizó la acción</param>
        /// <returns>Bool de que la tecla especificada ha sido presinada</returns>
        private bool IsNewKeyRelease(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            //Si hay un player controlando las teclas
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                bool uno = CurrentKeyboardStates[i].IsKeyUp(key);
                bool dos = LastKeyboardStates[i].IsKeyDown(key);

                return (uno && dos);
            }
            else
            {
                //Acepta el input de cualquier player
                return (IsNewKeyRelease(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyRelease(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyRelease(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyRelease(key, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// Detecta si una nueva tecla ha sido "clickeada"
        /// </summary>
        /// <param name="key">Key a presionar</param>
        /// <param name="controllingPlayer">Player que realizará la acción, si es null todos los player pueden</param>
        /// <param name="playerIndex">PlayerIndex para guardar el index del jugador que realizó la acción</param>
        /// <returns>Bool de que la tecla especificada ha sido presinada</returns>
        private bool IsContinuousKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            //Si hay un player controlando las teclas
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentKeyboardStates[i].IsKeyDown(key));
            }
            else
            {
                //Acepta el input de cualquier player
                return (IsContinuousKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsContinuousKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsContinuousKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsContinuousKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }


        /// <summary>
        /// Detecta si un nuevo botón esta siendo presionado de forma constante
        /// </summary>
        /// <param name="key">Key a presionar</param>
        /// <param name="controllingPlayer">Player que realizará la acción, si es null todos los player pueden</param>
        /// <param name="playerIndex">PlayerIndex para guardar el index del jugador que realizó la acción</param>
        /// <returns>Bool de que la tecla está siendo presionada</returns>
        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            //Si hay un control manejando esto
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;

                int i = (int)playerIndex;

                return (CurrentGamePadStates[i].IsButtonDown(button) &&
                        LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                //Acepta input de cualquier jugador
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }

        #endregion

        #region Regular Key Mappers

        public bool IsLeftCtrl(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            //Acepta Key.LeftControl
            return IsContinuousKeyPress(Keys.LeftControl, controllingPlayer, out playerIndex);
        }

        public bool IsUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            //Acepta Keys.Up
            return IsNewKeyRelease(Keys.Up, controllingPlayer, out playerIndex);
        }

        public bool IsDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            //Acepta Keys.Down
            return IsNewKeyRelease(Keys.Down, controllingPlayer, out playerIndex);
        }

        public bool IsLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            //Acepta Keys.Left
            return IsNewKeyRelease(Keys.Left, controllingPlayer, out playerIndex);
        }

        public bool IsRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            //Acepta Keys.Right
            return IsNewKeyRelease(Keys.Right, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Revisa si se ha realizado una acción de "Seleccionar Menu".
        /// </summary>
        /// <param name="controllingPlayer">Especifica que player realizará la selección, si es NULL
        /// aceptará input de cualquier jugador</param>
        /// <param name="playerIndex">Guardará el Index de quíen hizó la selección</param>
        /// <returns>Bool de que la tecla está siendo presionada</returns>
        public bool IsMenuSelect(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            //Acepta la Key.Space o Key.Enter
            return IsNewKeyPress(Keys.Space, controllingPlayer, out playerIndex) ||
                   IsNewKeyPress(Keys.Enter, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Revisa si se ha realizado una acción de "Cancelar Menu".
        /// Acepta Keys.Escape
        /// </summary>
        /// <param name="controllingPlayer">Especifica que player realizará la selección, si es NULL
        /// aceptará input de cualquier jugador</param>
        /// <param name="playerIndex">Guardará el Index de quíen hizó la selección</param>
        /// <returns>Bool de que la tecla está siendo presionada</returns>
        public bool IsMenuCancel(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            //Acepta Keys.Escape
            return IsNewKeyPress(Keys.Escape, controllingPlayer, out playerIndex);
        }


        /// <summary>
        /// Revisa si se ha realizado una acción de "Arriba".
        /// </summary>
        /// <param name="controllingPlayer">Especifica que player realizará la selección, si es NULL
        /// aceptará input de cualquier jugador</param>
        /// <returns>Bool de que la tecla está siendo presionada</returns>
        public bool IsMenuUp(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            //Acepta Keys.Up
            return IsNewKeyPress(Keys.Up, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Revisa si se ha realizado una acción de "Izquierda".
        /// </summary>
        /// <param name="controllingPlayer">Especifica que player realizará la selección, si es NULL
        /// aceptará input de cualquier jugador</param>
        /// <returns>Bool de que la tecla está siendo presionada</returns>
        public bool IsMenuLeft(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            //Acepta Keys.Left
            return IsNewKeyPress(Keys.Left, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Revisa si se ha realizado una acción de "Derecha".
        /// </summary>
        /// <param name="controllingPlayer">Especifica que player realizará la selección, si es NULL
        /// aceptará input de cualquier jugador</param>
        /// <returns>Bool de que la tecla está siendo presionada</returns>
        public bool IsMenuRight(PlayerIndex? controllingPlayer,
                                 out PlayerIndex playerIndex)
        {
            //Acepta Keys.Rigt
            return IsNewKeyPress(Keys.Right, controllingPlayer, out playerIndex);
        }

        /// <summary>
        /// Revisa si se ha realizado una acción de "Abajo".
        /// </summary>
        /// <param name="controllingPlayer">Especifica que player realizará la selección, si es NULL
        /// aceptará input de cualquier jugador</param>
        /// <returns>Bool de que la tecla está siendo presionada</returns>
        public bool IsMenuDown(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            //Acepta Keys.Down
            return IsNewKeyPress(Keys.Down, controllingPlayer, out playerIndex);
        }

        #endregion
    }
}
