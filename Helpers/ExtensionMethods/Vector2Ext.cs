using Microsoft.Xna.Framework;

namespace Helpers.ExtensionMethods.Vector2Ext
{
    public static class Vector2Ext
    {
        public static Vector2 Add(this Vector2 v2, int value2)
        {
            v2.X += value2;
            v2.Y += value2;
            return v2;
        }

        public static Vector2 Add(this Vector2 v2, float value2)
        {
            v2.X += value2;
            v2.Y += value2;
            return v2;
        }

        public static Vector2 AddToX(this Vector2 v2, int value2)
        {
            v2.X += value2;
            return v2;
        }

        public static Vector2 AddToX(this Vector2 v2, float value2)
        {
            v2.X += value2;
            return v2;
        }

        public static Vector2 AddToY(this Vector2 v2, int value2)
        {
            v2.Y += value2;
            return v2;
        }

        public static Vector2 AddToY(this Vector2 v2, float value2)
        {
            v2.Y += value2;
            return v2;
        }
    }
}
