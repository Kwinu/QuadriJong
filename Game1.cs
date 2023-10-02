using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace QuadriJong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private FontService myFonts;
        private TextureService myTextures;
        private ScoreService myScore;

        SceneStart myStartScene;
        SceneGame myGameScene;
        Scene myCurrentScene;
        ScenePause myPauseScene;
        SceneGameOver myGameOverScene;
        SceneVictory myVictoryScene;

        private bool isGamePaused = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            myFonts = new FontService(this);
            SpriteFont FontGame = myFonts.GetFont();
            SpriteFont FontTitle = myFonts.GetTitleFont();
            ServiceLocator.RegisterService<IFontService>(myFonts);

            myTextures = new TextureService(Content);
            ServiceLocator.RegisterService<ITextureService>(myTextures);
            myTextures.LoadTexture("Background", "quadriBG");
            myTextures.LoadTexture("Paddle", "paddle");
            myTextures.LoadTexture("Ball", "ballMain");

            var scoreService = new ScoreService();
            ServiceLocator.RegisterService<IScoreService>(scoreService);

            for (int i = 1; i <= 41; i++)
            {
                string tileName = "Tile" + i.ToString();
                string tilePath = "tile" + i.ToString();
                myTextures.LoadTexture(tileName, tilePath);
            }
           

            myStartScene = new SceneStart(this);
            myGameScene = new SceneGame(this, myTextures);
            myPauseScene = new ScenePause(this);
            myVictoryScene = new SceneVictory(this); 
            myGameOverScene = new SceneGameOver(this);

            myCurrentScene = new SceneStart(this);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                int mouseX = Mouse.GetState().X;
                int mouseY = Mouse.GetState().Y;

                if (mouseX < 0 || mouseX >= GraphicsDevice.Viewport.Width || mouseY < 0 || mouseY >= GraphicsDevice.Viewport.Height)
                {
                   // Jeu en pause
                    isGamePaused = true;
                }
                else
                {
                    // Reactivation jeu
                    isGamePaused = false;
                }
            }

            if (!isGamePaused)
            {

                base.Update(gameTime);
            }
            else
            {
               // Scene Pause
                myCurrentScene = myPauseScene;
                myPauseScene.Update(gameTime);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                myCurrentScene = myGameScene;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                myCurrentScene = myPauseScene;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                myCurrentScene = myStartScene;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.V))
            {
                myCurrentScene = myVictoryScene;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.G))
            {
                myCurrentScene = myGameOverScene;
            }

            bool allTilesEliminated = myGameScene.AllTilesEliminated();
            bool allPaddlesRemoved = myGameScene.AllPaddlesRemoved();

            if (allTilesEliminated)
            {
                myCurrentScene = myVictoryScene; // Changez de scène en cas de victoire
            }
            else if (allPaddlesRemoved)
            {
                myCurrentScene = myGameOverScene; // Changez de scène en cas de défaite
            }


            myCurrentScene.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            myCurrentScene.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}