using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace QuadriJong
{
    public interface ITextureService
    {
        void LoadTexture(string pTextureName, string pPath);
        Texture2D GetTexture(string pName);
    }
    class TextureService : ITextureService
    {
        private ContentManager contentManager;
        private Dictionary<string, Texture2D> textures;

        public TextureService(ContentManager contentManager) 
        {
            textures = new Dictionary<string, Texture2D>();
            this.contentManager = contentManager; 
        }

        public void LoadTexture (string pTextureName, string pPath)
        {
            Texture2D texture = contentManager.Load<Texture2D>(pPath);
            textures[pTextureName] = texture;
        }

        public Texture2D GetTexture(string pTextureName)
        {
            return textures[pTextureName];
        }
    }
}
