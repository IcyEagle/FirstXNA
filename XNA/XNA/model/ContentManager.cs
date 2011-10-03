using System.Collections.Generic;
using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA.model.block;

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
            GameModel.Instance.Character =  GameModel.Instance.GenericFactory.CreateCharacter(Game1.ScreenWidth / 2, 0);
            GameModel.Instance.UpdateManager.AddObjectForUpdate(GameModel.Instance.Character);
            GameModel.Instance.DrawManager.AddObjectForDraw(GameModel.Instance.Character);

            var blocksDTO = GameModel.Instance.Game.Content.Load<BlockDTO[]>("BlockDTO");
            foreach (var blockDto in blocksDTO)
            {
                blocksDTOByType[blockDto.type] = blockDto;
            }
            
            var helper = (TerrainGenerator)GameModel.Instance.Game.Services.GetService(typeof(TerrainGenerator));

            //DEBUG
            Block.physicsTexture = GameModel.Instance.TextureHelper.GenerateSimpleTexture(Terrain.BlockSize, Terrain.BlockSize, Color.White);

            var map = helper.generateMap(Game1.ScreenWidth, Game1.ScreenHeight);
            GameModel.Instance.Terrain = new Terrain(map);

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
                texture = GameModel.Instance.Game.Content.Load<Texture2D>(textureName);
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
