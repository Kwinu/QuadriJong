using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace QuadriJong
{
    public class SceneStart : Scene
    {
        public SceneStart(Game pGame) : base(pGame)
        {

        }

        public override void Update(GameTime pgameTime)
        {

        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);

            SpriteFont Font = ServiceLocator.GetService<IFontService>().GetFont();
            SpriteFont FontTitle = ServiceLocator.GetService<IFontService>().GetTitleFont();            

            pBatch.Begin();

            pBatch.DrawString(Font, "Appuyer sur 'S' pour commencer une partie", new Vector2(Screen.Width / 5 ,Screen.Height - 200), Color.White);
            pBatch.DrawString(FontTitle, "QuadriJong", new Vector2(Screen.Width / 2.8f , Screen.Height /8), Color.White);         
            
            pBatch.End();

        }
    }
}
