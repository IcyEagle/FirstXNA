using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNA.model.@base
{
    public class DrawableObject
    {

        public DrawManager.DrawLayerType DrawLayerType;

        public Texture2D Texture;
        private float _width;
        private float _height;

        private float _x;
        private float _y;
        private float _rotation;

        public virtual void Update()
        {
            
        }

        public virtual void Draw()
        {
            GameModel.Instance.SpriteBatch.Draw(Texture, new Vector2(ConvertUnits.ToDisplayUnits(_x), ConvertUnits.ToDisplayUnits(_y)), null, Color.White, _rotation, new Vector2(ConvertUnits.ToDisplayUnits(_width) / 2f, ConvertUnits.ToDisplayUnits(_height) / 2f), 1f, SpriteEffects.None, GetDepth());
        }

        public virtual Vector2 Position
        {
            get { return ConvertUnits.ToDisplayUnits(new Vector2(_x, _y)); }
            set { _x = ConvertUnits.ToSimUnits(value.X); _y = ConvertUnits.ToSimUnits(value.Y); }
        }

        public float X
        {
            get { return ConvertUnits.ToDisplayUnits(_x); }
        }

        public float Y
        {
            get { return ConvertUnits.ToDisplayUnits(_y); }
        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public float Width
        {
            get { return ConvertUnits.ToDisplayUnits(_width); }
            set { _width = ConvertUnits.ToSimUnits(value); }
        }

        public float Height
        {
            get { return ConvertUnits.ToDisplayUnits(_height); }
            set { _height = ConvertUnits.ToSimUnits(value); }
        }

        private float GetDepth()
        {
            return (float)DrawLayerType / 10;
        }
	}
}
