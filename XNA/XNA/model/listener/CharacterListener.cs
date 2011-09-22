using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XNA.model.grid;
using Microsoft.Xna.Framework;

namespace XNA.model.listener
{
    class CharacterListener
    {
        public CharacterListener()
        {
            GameModel.instance.grid.onEnterRegion += new Grid.onEnterRegionDelegate(onEnterRegionHandler);
        }

        private void onEnterRegionHandler(ActiveObject target, Vector2 destination)
        {
            if (target.master is Character)
            {
                Character character = (Character)target.master;
                for (int x = (int)destination.X - 1; x < (int)destination.X + 2; ++x)
                {
                    for (int y = (int)destination.Y - 1; y < (int)destination.Y + 2; ++y)
                    {
                        if (GameModel.instance.grid.hasRegionByCoordinate(new Vector2(x, y))) {
                            Region region = GameModel.instance.grid.getRegion(new Vector2(x, y));

                            foreach (ActiveObject member in region.members)
                            {
                                if (member.master is Item)
                                {
                                    Console.Write("AO");
                                    Item item = (Item)member.master;
                                    item.approachTo(character.body.Position);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
