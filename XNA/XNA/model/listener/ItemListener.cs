using Microsoft.Xna.Framework;
using XNA.model.behavior;
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
            for (int x = (int)destination.X - 1; x < (int)destination.X + 2; ++x)
            {
                for (int y = (int)destination.Y - 1; y < (int)destination.Y + 2; ++y)
                {
                    if (GameModel.Instance.Grid.hasRegionByCoordinate(new Vector2(x, y)))
                    {
                        Region region = GameModel.Instance.Grid.getRegion(new Vector2(x, y));

                        foreach (var member in region.members)
                        {
                            if (member != target)
                            {
                                if (member is Character)
                                {
                                    foreach (IBehavior behavior in target.Behaviors)
                                    {
                                        if (behavior is IActive)
                                        {
                                            member.OnMove += ((IActive) behavior).OnActive;
                                        }
                                    }
                                } else if (target is Character)
                                {
                                    foreach (IBehavior behavior in member.Behaviors)
                                    {
                                        if (behavior is IActive)
                                        {
                                            target.OnMove += ((IActive)behavior).OnActive;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void onLeaveRegionHandler(ActiveObject target, Vector2 source)
        {
            for (int x = (int) source.X - 1; x < (int) source.X + 2; ++x)
            {
                for (int y = (int) source.Y - 1; y < (int) source.Y + 2; ++y)
                {
                    if (GameModel.Instance.Grid.hasRegionByCoordinate(new Vector2(x, y)))
                    {
                        Region region = GameModel.Instance.Grid.getRegion(new Vector2(x, y));

                        foreach (var member in region.members)
                        {
                            if (member != target)
                            {
                                if (member is Character)
                                {
                                    foreach (IBehavior behavior in target.Behaviors)
                                    {
                                        if (behavior is IActive)
                                        {
                                            member.OnMove -= ((IActive) behavior).OnActive;
                                        }
                                    }
                                }
                                else if (target is Character)
                                {
                                    foreach (IBehavior behavior in member.Behaviors)
                                    {
                                        if (behavior is IActive)
                                        {
                                            target.OnMove -= ((IActive) behavior).OnActive;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
