using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNA.model.@base;

namespace XNA.model.block
{
    public class Block : PhysicalObject
    {
        // DEBUG
        public static Texture2D enabledTexture;

        public delegate void onDestroyHandler (Block block);
        public event onDestroyHandler onDestroy;

        private Texture2D textureTest;
        /*public int width;
        public int height;
        public int x;
        public int y;
        private Body body;*/

        public Block(string type, int x, int y)
        {
            BlockDTO blockDto = GameModel.instance.contentManager.getBlockDTOByType(type);
            textureTest = GameModel.instance.contentManager.getBlockTextureByName(blockDto.type);
            texture = textureTest;
            restitution = blockDto.restitution;
            friction = blockDto.friction;
            width = Terrain.BLOCK_SIZE;
            height = Terrain.BLOCK_SIZE;
            this.x = x;
            this.y = y;
        }

        /*public void Draw(SpriteBatch batch) {
            //batch.Draw(this.texture, new Rectangle(x, y, width, height), Color.White);

            //DEBUG
            batch.Draw(body != null ? Block.enabledTexture : this.texture, new Rectangle(x, y, width, height), Color.White);
        }*/
        
        public void enablePhysics()
        {
            if (this.body == null)
            {
                this.body = GameModel.instance.bodyManager.createBlockBody(this);
                texture = enabledTexture;//add
            }
        }

        public void disablePhysics()
        {
            if (body != null)
            {
                GameModel.instance.bodyManager.removeBody(body);
                body = null;
                texture = textureTest;//add
            }
        }

        public bool damage()
        {
            // change logic later.
            bool destroyed = true;

            if (destroyed)
            {
                onDestroy.Invoke(this);
            }

            return destroyed;
        }

    }

    
}
