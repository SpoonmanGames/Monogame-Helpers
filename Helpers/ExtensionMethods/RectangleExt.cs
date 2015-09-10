using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.ExtensionMethods.RectangleExt
{
    public static class RectangleExt
    {
        /// <summary>
        /// Permite comparar las posiciones y dimensiones entre dos rectangulos
        /// </summary>
        /// <param name="rect">Rectangulo original.</param>
        /// <param name="value1">Rectangulo contra el que se compara.</param>
        /// <returns></returns>
        public static bool Compare(this Rectangle rect, Rectangle value1)
        {
            return rect.X == value1.X && 
                   rect.Y == value1.Y &&
                   rect.Height == value1.Height &&
                   rect.Width == value1.Width;
        }

        /// <summary>
        /// Transforma el Rectangulo en un Vector2 usando las variables de posición X e Y
        /// </summary>
        /// <param name="rect">Rectangulo original.</param>
        /// <returns></returns>
        public static Vector2 ToVector2P(this Rectangle rect)
        {
            return new Vector2(rect.X, rect.Y);
        }

        /// <summary>
        /// Transforma el Rectangulo en un Vector2 usando las variables de dimesión Width e Height
        /// </summary>
        /// <param name="rect">Rectangulo original.</param>
        /// <returns></returns>
        public static Vector2 ToVector2S(this Rectangle rect)
        {
            return new Vector2(rect.Width, rect.Height);
        }
    }
}
