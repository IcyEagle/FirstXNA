using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA
{
    class TextureHelper
    {
        public Texture2D generateSimpleTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(GameModel.instance.game.GraphicsDevice, width, height);
            Color[] colorMap = new Color[width * height];
            for (int i = 0; i < colorMap.Length; i++) colorMap[i] = color;
            texture.SetData(colorMap);

            return texture;
        }


    }
}
