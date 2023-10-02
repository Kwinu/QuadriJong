using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace QuadriJong
{
    public class PaddleManager
    {
        private List<Paddle> lstPaddles;
        private ITextureService myTextures;
        private Rectangle screen;

        public PaddleManager (ITextureService pTextureService, int nbrPaddles, Rectangle Screen )
        {
            myTextures = pTextureService;
            lstPaddles = new List<Paddle> ();
            screen = Screen;

            int firstPositionX = 120;
            int paddleSpacing = 200;        

            for (int i = 0; i < nbrPaddles; i++)
            {
                Paddle paddle = new Paddle(myTextures.GetTexture("Paddle"), Screen, myTextures);
                paddle.SetPosition(firstPositionX + (i * (paddle.Width + paddleSpacing)), Screen.Height - 50);
                lstPaddles.Add(paddle);
            }            
        }

        public Paddle GetPaddleIndex(int index)
        {
            if (index >= 0 && index < lstPaddles.Count)
            {
                return lstPaddles[index];
            }
            return null;
        }

        public List<Paddle> GetPaddles()
        {
            return lstPaddles;
        }

        public void RemovePaddle(Paddle paddle)
        {
            lstPaddles.Remove(paddle);
        }

        public void Update()
        {
            int paddleSpacing = 200;

            for (int i = 0; i < lstPaddles.Count; i++)
            {
                Paddle paddle = lstPaddles[i];
                paddle.Update();

                // La souris est au milieu des paddles
                float mouseX_Offset = Mouse.GetState().X - paddle.MidWidth + i * (paddle.Width + paddleSpacing) - (1.5f * (paddle.Width + paddleSpacing));

                // La souris reste a l'ecran
                mouseX_Offset = MathHelper.Clamp(mouseX_Offset, 0, screen.Width - paddle.Width);

                // Deplacement paddle
                paddle.SetPosition((int)mouseX_Offset, paddle.Position.Y);                    
            }
                
        }

        public void Draw(SpriteBatch pBatch, string pTextureName)
        {
            foreach (Paddle paddle in lstPaddles)
            {
                paddle.Draw(pBatch, pTextureName);
            }
        }
    }
}
