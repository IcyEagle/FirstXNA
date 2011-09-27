using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA.model
{
    class TextureHelper
    {
        public Texture2D GenerateSimpleTexture(int width, int height, Color color)
        {
            var texture = new Texture2D(GameModel.Instance.Game.GraphicsDevice, width, height);
            var colorMap = new Color[width * height];
            for (int i = 0; i < colorMap.Length; i++) colorMap[i] = color;
            texture.SetData(colorMap);
            return texture;
        }


    }
}
