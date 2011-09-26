using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using XNA.model.block;
using XNA.model.character;

namespace XNA.model
{
    class BodyManager
    {
        public Body createCharacterBody(Character character)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.instance.world, character.width, character.height, 1f);
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
            Body body = BodyFactory.CreateRectangle(GameModel.instance.world, block.width, block.height, 1f);
            body.Position = block.getPosition();
            body.BodyType = BodyType.Static;
            body.Restitution = block.restitution;
            body.Friction = block.friction;
            body.CollisionCategories = Category.Cat1;
            body.CollidesWith = Category.All;
            return body;
        }

        public Body createPickableBody(int width, int height)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.instance.world, width, height, 1f);
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
