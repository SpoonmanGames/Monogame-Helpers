** Será necesario crear una Sprite Class ?? (Learning XNA- Pág 68)
** Será necesario un manager para el audio ?? (Learning XNA- Pág 85)
** Será necesario un modulo para Inteligencias Artificiales ?? (Learning XNA- Pág 103)
** Será necesario HLSL Effects ?? (Learning XNA- Pág 265 - Negative, Blur y Grayscale)
** Será necesario Particle System ?? (Learning XNA- Pág 299)


# EXTRAS

Game.Window.ClientBounds
	Accede a los limites de la pantall
usar 'enum' para estados:
	enum GameState { Start, InGame, GameOver };

# Helpers para MonoGame

Lista de helpers desde Learning XNA 4.0

# Consideración 1: Extensiones

Ejemplo de extensión

public static class PointExt
{
    public static Vector2 ToVector2(this Point point)
    {
        return new Vector2(point.X, point.Y);
    }
}


# Consideración 2: Crear algo que fácilmente permita crear animaciones

// TODO

# Consideracion 3: Cambiar el framerate del juego

En el constructor de Game1

TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 50);

Este ejemplo lo pone a 50 milisegundos = 20 fps

# Consideracion 4: Detectar si el juego anda lento

GameTime tiene la propiedad IsRunningSlowly

//TODO: Buscar una forma de detectar esto y avisar al usuario del problema.

# Consideración 5: Animation Frame configurable por animación

int timeSinceLastFrame = 0;
int millisecondsPerFrame = 50; // equivale a 20fps

The timeSinceLastFrame variable will be used to track how much time has passed since
the animation frame was changed. The millisecondsPerFrame variable will be used to
specify how much time you want to wait before moving the current frame index.

timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
if (timeSinceLastFrame > millisecondsPerFrame){
	timeSinceLastFrame −= millisecondsPerFrame;
	++currentFrame.X;
	if (currentFrame.X >= sheetSize.X){
		currentFrame.X = 0;
		++currentFrame.Y;
	if (currentFrame.Y >= sheetSize.Y)
		currentFrame.Y = 0;
	}
}

# Consideración 6: Sistema de colisión simple

protected bool Collide( )
{
	Rectangle ringsRect = new Rectangle((int)ringsPosition.X,
	(int)ringsPosition.Y, ringsFrameSize.X, ringsFrameSize.Y);
	Rectangle skullRect = new Rectangle((int)skullPosition.X,
	(int)skullPosition.Y, skullFrameSize.X, skullFrameSize.Y);

	return ringsRect.Intersects(skullRect);
}

Tener en cuenta portabilidad


# Consideración 8: Lluvia

This is actually really simple. The rain is just a plane with a uv panning texture. 

# Consideración 9: GameComponents

Permite que los metodos clásicos de XNA se llamen automaticamente en el orden de los componentes de Game.
Util para escenarios, textos de gui o debuggin.

class DebugComponent : DrawableGameComponent

protected override void Initialize()
{
	spriteManager = new SpriteManager(this);
	Components.Add(spriteManager);
	base.Initialize();
}

Ejemplos:

Game State/Screen Managers
ParticleEmitters
Drawable primitives
AI controllers
Input controllers
Animation controllers
Billboards
Physics & Collision Managers
Cameras
Manager de diferentes tipos.
Bubble Message y Rush Message (como los de nikoniko douga)

spriteManager.Enabled = false; // desactiva update
spriteManager.Visible = false; // desactiva draw

# Consideracion 10: Shaders

http://blog.josack.com/2011/07/my-first-2d-pixel-shaders-part-1.html

# Consideración 11: Camara 2D:

// TODO
