using GameLibrary;
using Microsoft.Xna.Framework;
using XNA.model.block;
using XNA.model.character;
using XNA.model.item;

namespace XNA.model
{
    class GenericFactory
    {
        public Block CreateBlock(string type, int x, int y)
        {
            var data = GameModel.instance.contentManager.getBlockDTOByType(type);
            var texture = GameModel.instance.contentManager.getBlockTextureByName(data.type);

            var instance = new Block
                               {
                                   Texture = texture,
                                   Width = Terrain.BLOCK_SIZE,
                                   Height = Terrain.BLOCK_SIZE,
                                   Restitution = data.restitution,
                                   Friction = data.friction
                               };

            instance.Position = new Vector2(x, y);
            return instance;
        }

        public Character CreateCharacter(float x, float y)
        {
            var data = GameModel.instance.game.Content.Load<CharacterDTO>("CharacterDTO");

            // generate texture.
            var helper = (TextureHelper)GameModel.instance.game.Services.GetService(typeof(TextureHelper));
            const int width = 32 - 2;
            const int height = 48 - 2;
            var texture = helper.generateSimpleTexture(width, height, Color.BlanchedAlmond);

            var instance = new Character(data.name, data.level)
                               {
                                   Texture = texture, 
                                   Width = width, 
                                   Height = height
                               };

            instance.Body = GameModel.instance.bodyManager.createCharacterBody(instance);
            instance.Position = new Vector2(x, y);
            return instance;
        }

        public Item CreateItem(float x, float y)
        {
            // generate texture.
            var helper = (TextureHelper)GameModel.instance.game.Services.GetService(typeof(TextureHelper));
            const int width = 16;
            const int height = 16;
            var texture = helper.generateSimpleTexture(16, 16, Color.Magenta);

            var instance = new Item
                           {
                               Texture = texture,
                               Width = width,
                               Height = height,
                           };

            instance.Body = GameModel.instance.bodyManager.createPickableBody(instance);
            instance.Position = new Vector2(x, y);
            return instance;
        }
    }
}
