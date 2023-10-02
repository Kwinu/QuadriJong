using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace QuadriJong
{
    public class Paddle : Sprite
    {
        public float CenterX
        {
            get { return Position.X + MidWidth; }
        }

        public Paddle(Texture2D pTexture, Rectangle pScreen, ITextureService pTextureService) : base(pTexture, pScreen, pTextureService)
        {
            
        }

        public override void Update()
        {   
            base.Update();
        }       
    }
}
