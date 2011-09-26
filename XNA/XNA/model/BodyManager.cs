using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using XNA.model.block;
using XNA.model.character;
using XNA.model.item;

namespace XNA.model
{
    class BodyManager
    {
        public Body createCharacterBody(Character character)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.instance.world, character.Width, character.Height, 1f);
            body.FixedRotation = true;
            body.Position = new Vector2((int)(Game1.SCREEN_WIDTH / 2), 0);
            body.BodyType = BodyType.Dynamic;
            body.Restitution = 0f;
            body.Friction = 1f;
            body.CollisionCategories = Category.Cat2;
            body.CollidesWith = Category.Cat1;
            return body;
        }

        public Body createBlockBody(Block block)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.instance.world, block.Width, block.Height, 1f);
            body.Position = block.Position;
            body.BodyType = BodyType.Static;
            body.Restitution = block.Restitution;
            body.Friction = block.Friction;
            body.CollisionCategories = Category.Cat1;
            body.CollidesWith = Category.All;
            return body;
        }

        public Body createPickableBody(Item item)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.instance.world, item.Width, item.Height, 1f);
            body.BodyType = BodyType.Dynamic;
            body.Restitution = .5f;
            body.Friction = .5f;
            body.CollisionCategories = Category.Cat3;
            body.CollidesWith = Category.Cat1;
            return body;
        }

        public void removeBody(Body body)
        {
            GameModel.instance.world.RemoveBody(body);
        }
    }
}
