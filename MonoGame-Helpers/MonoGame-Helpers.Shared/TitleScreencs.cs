using Microsoft.Xna.Framework;
using ScreenManager;
using ScreenManager.PantallasBases;
using ScreenManager.MenuScren;

namespace MonoGame_Helpers
{
    class TitleScreen : BaseMenuScreen
    {
        #region Constructor

        /// <summary>
        /// Constructor del menu, asigna las opciones y los eventos a la Screen
        /// </summary>
        public TitleScreen(string titulo)
            : base(titulo)
        {
            
        }

        #endregion
    }
}
