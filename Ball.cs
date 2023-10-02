using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace QuadriJong
{
    public class Ball : Sprite
    {

        public Ball(Texture2D pTexture, Rectangle pScreen, ITextureService pTextureService) : base(pTexture, pScreen, pTextureService)
        {

        }

        public override void Update()
        {

            if (Position.X > Screen.Width - Width)
            {
                SetPosition(Screen.Width - Width, Position.Y);
                ReverseSpeedX();
            }

            if (Position.X < 0)
            {
                SetPosition(0, Position.Y);
                ReverseSpeedX();
            }

            if (Position.Y < 0)
            {
                SetPosition(Position.X, 0);
                ReverseSpeedY();
            }


            base.Update();
        }
    }
}
