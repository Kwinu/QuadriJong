using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace QuadriJong
{
    public class SceneGame : Scene
    {
        Ball sprBall;
        bool ballStick;
        private ITextureService myTextures;
        private IScoreService myScores;
        private PaddleManager paddleManager;
        private TileManager tileManager;

        public SceneGame(Game pGame, ITextureService pTextureService) : base(pGame)
        {
            myTextures = pTextureService;
            myScores = ServiceLocator.GetService<IScoreService>();

            sprBall = new Ball(myTextures.GetTexture("Ball"), Screen, myTextures);

            paddleManager = new PaddleManager(pTextureService, 4, Screen);
            tileManager = new TileManager(myTextures, Screen);

            sprBall.Speed = new Vector2(5, -5);

            ballStick = true;
        }

        public bool AllTilesEliminated()
        {
            // Vérifiez si toutes les tuiles ont été éliminées
            return tileManager.GetTiles().Count == 0;
        }

        public bool AllPaddlesRemoved()
        {
            // Vérifiez si tous les paddles ont été supprimés
            return paddleManager.GetPaddles().Count == 0;
        }

        public override void Update(GameTime pgameTime)
        {
            // Emplacement souris
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                foreach (Paddle paddle in paddleManager.GetPaddles())
                {
                    paddle.SetPosition(Mouse.GetState().X, paddle.Position.Y);                    
                }
                ballStick = false;
            }   

            List<Tile> tilesToRemove = new List<Tile>();
            List<Paddle> paddlesToRemove = new List<Paddle>();

            foreach (Tile tile in tileManager.GetTiles())
            {
                if (sprBall.DetectCollision(tile))
                {
                    sprBall.AngleCollision(tile);
                    tilesToRemove.Add(tile);

                    myScores.IncreaseScore(10);
                }

                foreach (Paddle paddle in paddleManager.GetPaddles())
                {
                    if (tile.DetectCollision(paddle))
                    {
                        paddlesToRemove.Add(paddle);
                    }
                }
            }


            // Suppresion des tuiles touchées par la balle
            foreach (Tile tile in tilesToRemove)
            {
                tileManager.RemoveTile(tile);
                int tilesCount = tileManager.GetTiles().Count;
                if (tilesCount == 0)
                {
                    AllTilesEliminated();
                }
            }

            // Suppression du paddle touché par la tuile
            foreach (Paddle paddleToRemove in paddlesToRemove)
            {
               // Centrage de la souris sur les paddles restants
                int paddlesCount = paddleManager.GetPaddles().Count;
                if (paddlesCount > 1)
                {
                    int mouseX = 0;
                    foreach (Paddle paddle in paddleManager.GetPaddles())
                    {
                        if (paddle != paddleToRemove)
                        {
                            mouseX += (int)paddle.CenterX;
                        }
                    }
                    mouseX /= (paddlesCount - 1);
                    Mouse.SetPosition(mouseX, Mouse.GetState().Y);
                }
                paddleManager.RemovePaddle(paddleToRemove);
                if (paddlesCount == 0 )
                {
                    AllPaddlesRemoved();
                }
            }

            foreach (Paddle paddle in paddleManager.GetPaddles())
            {
                if (sprBall.DetectCollision(paddle))
                {
                    sprBall.AngleCollision(paddle);
                }
            }   

            sprBall.Update();            
            paddleManager.Update();
            tileManager.Update(pgameTime);

            // balle collée 
            if (ballStick)
            {
                Random rnd = new Random();
                int rndPaddleIndex = rnd.Next(0, paddleManager.GetPaddles().Count); 
                Paddle randomPaddle = paddleManager.GetPaddleIndex(rndPaddleIndex); 

                // Déplace la balle vers le paddle aléatoire
                sprBall.SetPosition(randomPaddle.CenterX - sprBall.MidWidth, randomPaddle.Position.Y - sprBall.Height);
            }

            if (sprBall.Position.Y > Screen.Height)
            {
                if (paddleManager.GetPaddles().Count > 0)
                {
                    sprBall.ReverseSpeedY();
                    ballStick = true;
                }                
            }
        }

        public override void Draw(SpriteBatch pBatch)
        {
            base.Draw(pBatch);

            pBatch.Begin();

            sprBall.Draw(pBatch, "Ball");
            
            paddleManager.Draw(pBatch, "Paddle");

            tileManager.Draw(pBatch, "Tile");

            SpriteFont Font = ServiceLocator.GetService<IFontService>().GetFont();
            pBatch.DrawString(Font, "Score : " + myScores.Score.ToString(), new Vector2(10, 10), Color.White);

            pBatch.End();
        }
    }
}
