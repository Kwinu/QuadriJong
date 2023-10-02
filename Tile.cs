using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace QuadriJong
{
    public class Tile : Sprite
    {
        Random rnd = new Random();
        public string TextureName { get; private set; }

        public Tile(Texture2D pTexture, Rectangle pScreen, ITextureService pTextureService) : base(pTexture, pScreen, pTextureService)
        {
            RandomSpawn();
        }

        public void RandomSpawn()
        {
            float posX = rnd.Next(0, Screen.Width - Width);
            float posY = -Height - rnd.Next(0, Screen.Height); 
            SetPosition(posX, posY);
        }

        public static Tile RandomTile(Rectangle pScreen, ITextureService pTextureService)
        {
            Random rnd = new Random();
            int rndTile = rnd.Next(1, 42);
            string tileName = "Tile" + rndTile.ToString();
            Texture2D tileTexture = pTextureService.GetTexture(tileName);

            Tile tile = new Tile(tileTexture, pScreen, pTextureService);
            tile.RandomSpawn();
            tile.TextureName = tileName; // Recuperation du nom de la texture

            return tile;
        }

        public override void Update()
        {

            Position += Speed;

            if (Position.Y >= Screen.Height)
            {
                RandomSpawn();
            }

            base.Update();
        }
    }
}



