using System.Collections.Generic;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using XNA.model.block;
using XNA.model.character;
using XNA.model.item;
using FarseerPhysics.Common;

namespace XNA.model
{
    class BodyManager
    {
        public Body CreateCharacterBody(Character character)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.Instance.World, ConvertUnits.ToSimUnits(character.Width), ConvertUnits.ToSimUnits(character.Height), 1f);
            body.FixedRotation = true;
            body.SleepingAllowed = false;
            body.Position = ConvertUnits.ToSimUnits(new Vector2(Game1.ScreenWidth / 2, 0));
            body.BodyType = BodyType.Dynamic;
            body.Restitution = 0;
            body.Friction = 0f;
            body.CollisionCategories = Category.Cat2;
            body.CollidesWith = Category.Cat1;
            return body;

            /*Body body = BodyFactory.CreateBody(GameModel.Instance.World, ConvertUnits.ToSimUnits(new Vector2(Game1.ScreenWidth / 2, 0)));

            var topFixture = FixtureFactory.AttachRectangle(ConvertUnits.ToSimUnits(18), ConvertUnits.ToSimUnits(2), 1f, new Vector2(0, ConvertUnits.ToSimUnits(-19)), body);
            topFixture.Friction = 0f;

            var bottomFixture = FixtureFactory.AttachRectangle(ConvertUnits.ToSimUnits(18), ConvertUnits.ToSimUnits(2), 1f, new Vector2(0, ConvertUnits.ToSimUnits(19)), body);
            bottomFixture.Friction = .5f;

            var leftFixture = FixtureFactory.AttachRectangle(ConvertUnits.ToSimUnits(1), ConvertUnits.ToSimUnits(38), 1f, new Vector2(ConvertUnits.ToSimUnits(-9.5), 0), body);
            leftFixture.Friction = 0;

            var rightFixture = FixtureFactory.AttachRectangle(ConvertUnits.ToSimUnits(1), ConvertUnits.ToSimUnits(38), 1f, new Vector2(ConvertUnits.ToSimUnits(9.5), 0), body);
            rightFixture.Friction = 0;

            body.FixedRotation = true;
            body.SleepingAllowed = false;
            body.Restitution = 0.05f;
            body.BodyType = BodyType.Dynamic;
            body.CollisionCategories = Category.Cat2;
            body.CollidesWith = Category.Cat1;

            return body;*/
        }

        public Body CreateBlockBody(Block block)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.Instance.World, ConvertUnits.ToSimUnits(block.Width), ConvertUnits.ToSimUnits(block.Height), 1f);
            body.Position = ConvertUnits.ToSimUnits(block.Position);
            body.BodyType = BodyType.Static;
            body.Restitution = 0;//block.Restitution;
            body.Friction = 0;//block.Friction;
            body.CollisionCategories = Category.Cat1;
            body.CollidesWith = Category.All;
            return body;
        }

        public Body CreatePickableBody(Item item)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.Instance.World, ConvertUnits.ToSimUnits(item.Width), ConvertUnits.ToSimUnits(item.Height), 1f);
            body.SleepingAllowed = false;
            body.BodyType = BodyType.Dynamic;
            body.Restitution = .5f;
            body.Friction = .5f;
            body.CollisionCategories = Category.Cat3;
            body.CollidesWith = Category.Cat1;
            return body;
        }

        public void RemoveBody(Body body)
        {
            GameModel.Instance.World.RemoveBody(body);
        }
    }
}
