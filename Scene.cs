using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QuadriJong
{
    public abstract class Scene
    {
        public Rectangle Screen { get; private set; }        

        public Scene(Game pGame)
        {
            Screen = pGame.Window.ClientBounds;
        }

        public virtual void Initialize()
        {

        }

        public abstract void Update(GameTime pgameTime);


        public virtual void Draw(SpriteBatch pBatch)
        {
            Texture2D backgroundTexture = ServiceLocator.GetService<ITextureService>().GetTexture("Background");
            pBatch.Begin();
            pBatch.Draw(backgroundTexture, Vector2.Zero, Color.White);
            pBatch.End();
        }
    }
}
