using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XNA.model
{
    class Character
    {

        private Game1 game;

        public string name;
        public int level;

        public int x;
        public int y;
        public int width;
        public int height;

        public Body body;
        public Texture2D texture;

        public Character(Game1 game, string name, int level)
        {
            this.game = game;
            this.name = name;
            this.level = level;
            width = 100; height = 200;
            create();
        }

        private void create()
        {
            body = GameModel.instance.bodyManager.createCharacterBody(this);
            TextureHelper helper = (TextureHelper)game.Services.GetService(typeof(TextureHelper));
            texture = helper.generateSimpleTexture(width, height, Color.Azure);
        }

        public void Draw()
        {
            game.spriteBatch.Draw(texture, body.Position, new Rectangle(x, y, 100, 200), Color.Azure, body.Rotation, new Vector2(50, 100), 1f, SpriteEffects.None, 0f);
        }

    }
}
