using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA.model.@base
{
    public class DrawableObject
    {

        public Texture2D Texture;
        public int Width;
        public int Height;

        protected float _x;
        protected float _y;
        public float Rotation;

        public virtual void Update()
        {
            
        }

        public virtual void Draw()
        {
            GameModel.instance.spriteBatch.Draw(Texture, new Vector2(_x, _y), null, Color.White, Rotation, new Vector2(Width / 2f, Height / 2f), 1f, SpriteEffects.None, 0f);
        }

        public virtual Vector2 Position
        {
            get { return new Vector2(_x, _y); }
            set { _x = value.X; _y = value.Y; }
        }

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }
	}
}
