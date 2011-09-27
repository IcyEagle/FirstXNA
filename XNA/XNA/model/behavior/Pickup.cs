using Microsoft.Xna.Framework;
using XNA.model.@base;
using XNA.model.grid;
using XNA.model.item;

namespace XNA.model.behavior
{
    class Pickup : IBehavior, IActive
    {
        private readonly PhysicalObject _master;
        private readonly float _distance;

        public Pickup(PhysicalObject master, float distance)
        {
            _master = master;
            _distance = distance;
        }

        public void OnActive(ActiveObject target, Vector2 coordinates)
        {
            Vector2 diff = coordinates - _master.Position;
            if (diff.Length() < _distance)
            {
                // TODO pick up item.
                GameModel.Instance.UpdateManager.removeObjectForUpdate(_master);
                GameModel.Instance.Terrain.items.Remove(_master as Item);
            }
        }
    }
}
