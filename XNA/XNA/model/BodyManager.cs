using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace XNA.model
{
    class BodyManager
    {

        World world;

        public BodyManager(World world)
        {
            this.world = world;
        }

        public Body createCharacterBody(Character character)
        {
            Body body = BodyFactory.CreateRectangle(world, character.width, character.height, 1f);
            body.Position = new Vector2((int)(Game1.SCREEN_WIDTH / 2), 0);
            body.BodyType = BodyType.Dynamic;
            body.Restitution = .5f;
            body.Friction = .5f;
            return body;
        }

        public Body createBlockBody(Block block)
        {
            Body body = BodyFactory.CreateRectangle(world, block.width, block.height, 1f);
            body.Position = new Vector2(block.x, block.y);
            body.BodyType = BodyType.Static;
            body.Restitution = .5f;
            body.Friction = .5f;
            return body;
        }

        public Body createPickableBody(int x, int y, int width, int height)
        {
            Body body = BodyFactory.CreateRectangle(world, width, height, 1f);
            body.Position = new Vector2(x, y);
            body.BodyType = BodyType.Dynamic;
            body.Restitution = .5f;
            body.Friction = .5f;
            return body;
        }

    }
}
