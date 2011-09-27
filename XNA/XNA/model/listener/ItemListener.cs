using Microsoft.Xna.Framework;
using XNA.model.character;
using XNA.model.grid;
using XNA.model.item;

namespace XNA.model.listener
{
    class ItemListener
    {
        public ItemListener()
        {
            GameModel.Instance.Grid.onEnterRegion += onEnterRegionHandler;
            GameModel.Instance.Grid.onLeaveRegion += onLeaveRegionHandler;
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
                        if (GameModel.Instance.Grid.hasRegionByCoordinate(new Vector2(x, y)))
                        {
                            Region region = GameModel.Instance.Grid.getRegion(new Vector2(x, y));

                            foreach (var member in region.members)
                            {
                                if (member is Item)
                                {
                                    var item = member as Item;
                                    item.onMove += onMoveHandler;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void onLeaveRegionHandler(ActiveObject target, Vector2 source)
        {
            if (target is Character)
            {
                var character = target as Character;
                for (int x = (int)source.X - 1; x < (int)source.X + 2; ++x)
                {
                    for (int y = (int)source.Y - 1; y < (int)source.Y + 2; ++y)
                    {
                        if (GameModel.Instance.Grid.hasRegionByCoordinate(new Vector2(x, y)))
                        {
                            Region region = GameModel.Instance.Grid.getRegion(new Vector2(x, y));

                            foreach (var member in region.members)
                            {
                                if (member is Item)
                                {
                                    var item = member as Item;
                                    //item.onMove -= onMoveHandler;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void onMoveHandler(ActiveObject target, Vector2 coordinates)
        {
            Vector2 diff = coordinates - GameModel.Instance.Character.Position;
            if (diff.Length() < 20)
            {
                GameModel.Instance.UpdateManager.removeObjectForUpdate(target);
                GameModel.Instance.Terrain.items.Remove(target as Item);
            }
        }
    }
}
