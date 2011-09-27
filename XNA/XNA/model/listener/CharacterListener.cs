using XNA.model.character;
using XNA.model.grid;
using Microsoft.Xna.Framework;
using XNA.model.item;

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
            if (target is Character)
            {
                var character = target as Character;
                for (int x = (int)destination.X - 1; x < (int)destination.X + 2; ++x)
                {
                    for (int y = (int)destination.Y - 1; y < (int)destination.Y + 2; ++y)
                    {
                        if (GameModel.instance.grid.hasRegionByCoordinate(new Vector2(x, y))) {
                            Region region = GameModel.instance.grid.getRegion(new Vector2(x, y));

                            foreach (var member in region.members)
                            {
                                if (member is Item)
                                {
                                    var item = member as Item;
                                    item.ApproachTo(character.Body.Position);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
