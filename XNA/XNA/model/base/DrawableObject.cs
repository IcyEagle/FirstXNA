using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA.model.@base
{
    public class DrawableObject
    {

        public Texture2D Texture;
        public int Width;
        public int Height;

        private float _x;
        private float _y;
        private float _rotation;

        public virtual void Update()
        {
            
        }

        public virtual void Draw()
        {
            GameModel.Instance.SpriteBatch.Draw(Texture, new Vector2(_x, _y), null, Color.White, _rotation, new Vector2(Width / 2f, Height / 2f), 1f, SpriteEffects.None, 0f);
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

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
	}
}
