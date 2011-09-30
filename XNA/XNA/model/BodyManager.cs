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
            body.Restitution = 0f;
            body.Friction = 1f;
            body.CollisionCategories = Category.Cat2;
            body.CollidesWith = Category.Cat1;
            return body;

            //Body body = BodyFactory.CreateBody(GameModel.Instance.World, ConvertUnits.ToSimUnits(new Vector2(Game1.ScreenWidth / 2, 0)));
            /*Vertices rectangleVertices1 = PolygonTools.CreateRectangle(ConvertUnits.ToSimUnits(character.Width) / 2, ConvertUnits.ToSimUnits(character.Height) / 2);
            Vertices rectangleVertices2 = PolygonTools.CreateRectangle(ConvertUnits.ToSimUnits(character.Width) / 2, ConvertUnits.ToSimUnits(character.Height) / 2);
            PolygonShape rectangleShape1 = new PolygonShape(rectangleVertices1, 1f);
            rectangleVertices1.Translate(new Vector2(-2f, 0));
            PolygonShape rectangleShape2 = new PolygonShape(rectangleVertices2, 1f);
            rectangleVertices2.Translate(new Vector2(2f, 0));
            List<Vertices> verticles = new List<Vertices>(2);
            verticles.Add(rectangleVertices1);
            verticles.Add(rectangleVertices2);
            
            Body body = BodyFactory.CreateBody(GameModel.Instance.World);
            
            body.CreateFixture(rectangleShape1);
            body.CreateFixture(rectangleShape2);

            body.FixedRotation = true;
            body.SleepingAllowed = false;
            body.BodyType = BodyType.Dynamic;
            body.Restitution = 0f;
            body.Friction = 1f;
            body.CollisionCategories = Category.Cat2;
            body.CollidesWith = Category.Cat1;
            return body;*/
        }

        public Body CreateBlockBody(Block block)
        {
            Body body = BodyFactory.CreateRectangle(GameModel.Instance.World, ConvertUnits.ToSimUnits(block.Width), ConvertUnits.ToSimUnits(block.Height), 1f);
            body.Position = ConvertUnits.ToSimUnits(block.Position);
            body.BodyType = BodyType.Static;
            body.Restitution = block.Restitution;
            body.Friction = block.Friction;
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
