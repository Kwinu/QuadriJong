using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace QuadriJong
{
    public interface IFontService
    {
        SpriteFont GetFont();
        SpriteFont GetTitleFont();
    }

    class FontService : IFontService
    {
        SpriteFont mainFont;
        SpriteFont titleFont;        

        public FontService(Game pGame)
        {
            LoadFonts(pGame);
        }

        private void LoadFonts(Game pGame)
        {
            mainFont = pGame.Content.Load<SpriteFont>("VikingFont");
            titleFont = pGame.Content.Load<SpriteFont>("NordicFont");
        }

        public SpriteFont GetFont()
        {
            return mainFont;
        }

        public SpriteFont GetTitleFont()
        {
            return titleFont;
        }
    }
}
