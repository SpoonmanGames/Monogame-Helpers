using Microsoft.Xna.Framework;

namespace Helpers.ExtensionMethods.Vector2Ext
{
    public static class Vector2Ext
    {
        public static Vector2 Add(this Vector2 v2, int value1)
        {
            v2.X += value1;
            v2.Y += value1;
            return v2;
        }

        public static Vector2 Add(this Vector2 v2, float value1)
        {
            v2.X += value1;
            v2.Y += value1;
            return v2;
        }

        public static Vector2 AddToX(this Vector2 v2, int value1)
        {
            v2.X += value1;
            return v2;
        }

        public static Vector2 AddToX(this Vector2 v2, float value1)
        {
            v2.X += value1;
            return v2;
        }

        public static Vector2 AddToY(this Vector2 v2, int value1)
        {
            v2.Y += value1;
            return v2;
        }

        public static Vector2 AddToY(this Vector2 v2, float value1)
        {
            v2.Y += value1;
            return v2;
        }

        public static Vector2 SubstractToX(this Vector2 v2, int value1) { return v2.AddToX(value1 * -1); }

        public static Vector2 SubstractToX(this Vector2 v2, float value1) { return v2.AddToX(value1 * -1); }
    }
}
