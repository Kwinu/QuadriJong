using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace QuadriJong
{
    public class Sprite
    {
        public Vector2 Position { get; set; }
        public Vector2 Speed { get; set; }
        public Rectangle BoundingBox { get; private set; }

        public int Height
        {
            get
            {
                return Texture.Height;
            }
        }
        public int MidHeight
        {
            get
            {
                return Texture.Height / 2;
            }
        }
        public int Width
        {
            get
            {
                return Texture.Width;
            }
        }
        public int MidWidth
        {
            get
            {
                return Texture.Width / 2;
            }
        }

        protected Texture2D Texture;
        protected Rectangle Screen;
        protected ITextureService myTextures;

        public Sprite(Texture2D pTexture, Rectangle pScreen, ITextureService pTextureService)
        {
            Texture = pTexture;
            Screen = pScreen;
            myTextures = pTextureService;
        }

        public void SetPosition(Vector2 pPosition)
        {
            Position = pPosition;
        }

        public void SetPosition(float pX, float pY)
        {
            Position = new Vector2(pX, pY);
        }

        public Rectangle NextPositionX()
        {
            Rectangle nextPosition = BoundingBox;
            nextPosition.Offset(new Point((int)Speed.X, 0));
            return nextPosition;
        }

        public Rectangle NextPositionY()
        {
            Rectangle nextPosition = BoundingBox;
            nextPosition.Offset(new Point(0, (int)Speed.Y));
            return nextPosition;
        }

        public void ReverseSpeedX()
        {
            Speed = new Vector2(-Speed.X, Speed.Y);
        }

        public void ReverseSpeedY()
        {
            Speed = new Vector2(Speed.X, -Speed.Y);

        }

        public void AngleCollision(Sprite sprCible)
        {
            float angle = (float)Math.Atan2(this.Position.Y - sprCible.Position.Y, this.Position.X - sprCible.Position.X);
            float speed = Speed.Length();
            Speed = new Vector2(speed * (float)Math.Cos(angle), speed * (float)Math.Sin(angle));
        }

        public virtual bool DetectCollision(Sprite sprCible)
        {
            if (BoundingBox.Intersects(sprCible.BoundingBox))
            {
                return true; 
            }

            return false;
        }

        public virtual void Update()
        {
            Position += Speed; 

            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }


        public virtual void Draw(SpriteBatch pBatch, string pName)
        {
            Texture2D sprTexture = myTextures.GetTexture(pName);
            pBatch.Draw(sprTexture, Position, Color.White);
        }
    }
}
