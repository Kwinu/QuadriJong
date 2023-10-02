using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace QuadriJong
{
    public class ScenePause : Scene
    {
        Game game;
        public ScenePause(Game pGame) : base(pGame)
        {
            game = pGame;
        }

        public override void Update(GameTime pGameTime)
        {
          
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);
            SpriteFont FontTitle = ServiceLocator.GetService<IFontService>().GetTitleFont();
            SpriteFont Font = ServiceLocator.GetService<IFontService>().GetFont();

            pBatch.Begin();
            
            pBatch.DrawString(FontTitle, "PAUSE", new Vector2(Screen.Width / 3, Screen.Height / 8), Color.White);
            pBatch.DrawString(Font, "Appuyer sur 'S' pour revenir au jeu", new Vector2(Screen.Width / 5, Screen.Height - 200), Color.White);

            pBatch.End();
        }
    }
}
