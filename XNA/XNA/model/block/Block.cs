using Microsoft.Xna.Framework.Graphics;
using XNA.model.@base;

namespace XNA.model.block
{
    public class Block : PhysicalObject
    {
        // DEBUG
        public static Texture2D physicsTexture;

        public delegate void onDestroyHandler (Block block);
        public event onDestroyHandler onDestroy;

        // DEBUG
        private Texture2D _classicTexture;

        public void enablePhysics()
        {
            if (this.Body == null)
            {
                this.Body = GameModel.Instance.BodyManager.CreateBlockBody(this);

                // DEBUG
                _classicTexture = Texture;
                Texture = physicsTexture;
            }
        }

        public void disablePhysics()
        {
            if (Body != null)
            {
                GameModel.Instance.BodyManager.RemoveBody(Body);
                Body = null;

                // DEBUG
                Texture = _classicTexture;//add
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
