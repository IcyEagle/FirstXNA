using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XNA
{
    class TextureHelper
    {
        private Game game;

        public TextureHelper(Game game)
        {
            this.game = game;
        }

        public Texture2D generateSimpleTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(game.GraphicsDevice, width, height);
            Color[] colorMap = new Color[width * height];
            for (int i = 0; i < colorMap.Length; i++) colorMap[i] = color;
            texture.SetData(colorMap);

            return texture;
        }


    }
}
