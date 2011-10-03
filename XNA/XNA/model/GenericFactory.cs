using GameLibrary;
using Microsoft.Xna.Framework;
using XNA.model.behavior;
using XNA.model.block;
using XNA.model.character;
using XNA.model.item;

namespace XNA.model
{
    class GenericFactory
    {
        public Block CreateBlock(string type, int x, int y)
        {
            var data = GameModel.Instance.ContentManager.getBlockDTOByType(type);
            var texture = GameModel.Instance.ContentManager.getBlockTextureByName(data.type);

            var instance = new Block
                               {
                                   Texture = texture,
                                   Width = Terrain.BLOCK_SIZE,
                                   Height = Terrain.BLOCK_SIZE,
                                   DrawLayerType = DrawManager.DrawLayerType.StaticBlock,
                                   Restitution = data.restitution,
                                   Friction = data.friction
                               };

            instance.Position = new Vector2(x, y);
            GameModel.Instance.DrawManager.AddObjectForDraw(instance);
            return instance;
        }

        public Character CreateCharacter(float x, float y)
        {
            var data = GameModel.Instance.Game.Content.Load<CharacterDTO>("CharacterDTO");

            // generate texture.
            const int width = 32 - 12;
            const int height = 48 - 8;
            var texture = GameModel.Instance.TextureHelper.GenerateSimpleTexture(width, height, Color.BlanchedAlmond);

            var instance = new Character(data.name, data.level)
                               {
                                   Texture = texture, 
                                   Width = width, 
                                   Height = height,
                                   DrawLayerType = DrawManager.DrawLayerType.Character
                               };

            instance.Body = GameModel.Instance.BodyManager.CreateCharacterBody(instance);
            instance.Position = new Vector2(x, y);
            return instance;
        }

        public Item CreateItem(float x, float y)
        {
            // generate texture.
            const int width = 16;
            const int height = 16;
            var texture = GameModel.Instance.TextureHelper.GenerateSimpleTexture(16, 16, Color.Magenta);

            var instance = new Item
                           {
                               Texture = texture,
                               Width = width,
                               Height = height,
                               DrawLayerType = DrawManager.DrawLayerType.PickableItem
                           };

            instance.Body = GameModel.Instance.BodyManager.CreatePickableBody(instance);
            instance.Position = new Vector2(x, y);

            // behaviors.
            instance.AddBehavior(new Sticky(instance, 64, 1));
            instance.AddBehavior(new Pickup(instance, 30));

            GameModel.Instance.UpdateManager.AddObjectForUpdate(instance);
            GameModel.Instance.DrawManager.AddObjectForDraw(instance);

            return instance;
        }
    }
}
