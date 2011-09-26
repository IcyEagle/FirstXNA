using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA.model.@base
{
    public class DrawableObject
    {

        public Texture2D texture;

        protected float x;
        protected float y;
        public float rotation;
        public int width;
        public int height;

        protected bool bodyNeedPosition;

        public DrawableObject()
        {
            
        }

        public virtual void Update()
        {
            
        }

        public virtual void Draw()
        {
            GameModel.instance.spriteBatch.Draw(texture, new Vector2(x, y), null, Color.White, rotation, new Vector2(width / 2f, height / 2f), 1f, SpriteEffects.None, 0f);
        }

        public void setPosition(float x, float y)
        {
            this.x = x;
            this.y = y;
            bodyNeedPosition = true;
        }

        public void setPosition(Vector2 position)
        {
            setPosition(position.X, position.Y);
        }

        public Vector2 getPosition()
        {
            return new Vector2(x, y);
        }

        public float getPosX()
        {
            return x;
        }

        public float getPosY()
        {
            return y;
        }
	}
}
