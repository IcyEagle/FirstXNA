using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA.model.block;
using XNA.model.character;

namespace XNA.model
{
    class ContentManager
    {

        private readonly Dictionary<string, BlockDTO> blocksDTOByType;
        private readonly Dictionary<string, Texture2D> texturesByName;

        public ContentManager()
        {
            blocksDTOByType = new Dictionary<string, BlockDTO>();
            texturesByName = new Dictionary<string, Texture2D>();
        }

        public void init()
        {

            var characterDTO = GameModel.instance.game.Content.Load<CharacterDTO>("CharacterDTO");
            GameModel.instance.character = new Character(characterDTO.name, characterDTO.level);
            GameModel.instance.updateManager.addObjectForUpdate(GameModel.instance.character);

            var blocksDTO = GameModel.instance.game.Content.Load<BlockDTO[]>("BlockDTO");
            foreach (var blockDto in blocksDTO)
            {
                blocksDTOByType[blockDto.type] = blockDto;
            }
            
            var helper = (TerrainGenerator)GameModel.instance.game.Services.GetService(typeof(TerrainGenerator));

            //DEBUG
            var textureHelper = (TextureHelper)GameModel.instance.game.Services.GetService(typeof(TextureHelper));
            Block.enabledTexture = textureHelper.generateSimpleTexture(Terrain.BLOCK_SIZE, Terrain.BLOCK_SIZE, Color.White);

            var map = helper.generateMap(Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT);
            GameModel.instance.terrain = new Terrain(map);
            GameModel.instance.game.Components.Add(GameModel.instance.terrain);

        }

        public BlockDTO getBlockDTOByType(string type)
        {
            return blocksDTOByType[type];
        }

        public Texture2D getBlockTextureByName(string textureName)
        {
            return checkTexturesCache("images/blocks/" + textureName);
        }

        private Texture2D checkTexturesCache(string textureName)
        {
            Texture2D texture;
            if (!texturesByName.ContainsKey(textureName))
            {
                texture = GameModel.instance.game.Content.Load<Texture2D>(textureName);
                texturesByName[textureName] = texture;
            }
            else
            {
                texture = texturesByName[textureName];
            }
            return texture;
        }

    }
}
