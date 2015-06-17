using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
public class Camera
{
    public Camera(Viewport viewport)
    {
        Origin = new Vector2(viewport.Width / 2.0f, viewport.Height / 2.0f);
        Zoom = 1.0f;
    }

    public Vector2 Position { get; set; }
    public Vector2 Origin { get; set; }
    public float Zoom { get; set; }
    public float Rotation { get; set; }

    public Matrix GetViewMatrix(Vector2 parallax)
    {
        // To add parallax, simply multiply it by the position
        return Matrix.CreateTranslation(new Vector3(-Position * parallax, 0.0f)) *
            // The next line has a catch. See note below.
            Matrix.CreateTranslation(new Vector3(-Origin, 0.0f)) *
            Matrix.CreateRotationZ(Rotation) *
            Matrix.CreateScale(Zoom, Zoom, 1) *
            Matrix.CreateTranslation(new Vector3(Origin, 0.0f));
    }



    /*IMPLEMENTACION*/
    //Camera camera = new Camera(GraphicsDevice.Viewport);
    //Vector2 parallax = new Vector2(0.5f);
    //spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.GetViewMatrix(parallax));
    //spriteBatch.Draw(texture, position, Color.White); // This sprite will appear to move at 50% of the normal speed
    //spriteBatch.End();
    ////https://youtu.be/Ly7SJeiirhU

}