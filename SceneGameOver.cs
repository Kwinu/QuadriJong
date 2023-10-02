using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadriJong
{
    public class SceneGameOver : Scene
    {
        Game game;
        public SceneGameOver(Game pGame) : base(pGame)
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

            pBatch.DrawString(FontTitle, "GAMEOVER", new Vector2(Screen.Width / 3, Screen.Height / 8), Color.White);
            pBatch.DrawString(Font, "Appuyer sur 'Echap' pour quitter le jeu", new Vector2(Screen.Width / 5, Screen.Height - 200), Color.White);

            pBatch.End();
        }
    }
}
