using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace QuadriJong
{
    public class TileManager
    {
        private List<Tile> lstTiles;
        private ITextureService myTextures;
        private Rectangle screen;


        public TileManager(ITextureService pTextureService, Rectangle pScreen)
        {
            myTextures = pTextureService;
            screen = pScreen;
            lstTiles = CreateListTiles();
        }

        public List<Tile> CreateListTiles()
        {
            List<Tile> lstTiles = new List<Tile>();

            for (int i = 0; i < 40; i++)
            {
                Tile randomTile = Tile.RandomTile(screen, myTextures);
                lstTiles.Add(randomTile);
            }
            return lstTiles;
        }

        public List<Tile> GetTiles()
        {
            return lstTiles;
        }

        public void RemoveTile(Tile tile)
        {
            lstTiles.Remove(tile);
        }

        public void Update(GameTime pGameTime)
        {
            foreach (Tile tile in lstTiles)
            {
                tile.Update();

                if (tile.Position.Y >= screen.Height)
                {
                    tile.RandomSpawn();
                }
                tile.Speed = new Vector2(0, 0.3f);
            }
        }

        public void Draw(SpriteBatch pBatch, string pTextureName)
        {
            for (int i = 0; i < lstTiles.Count; i++)
            {
                Tile tile = lstTiles[i];
                tile.Draw(pBatch, tile.TextureName);
            }            
        }
    }
}
