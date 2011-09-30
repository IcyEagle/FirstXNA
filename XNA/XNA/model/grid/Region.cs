using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNA.model.grid
{
    /**
     * A particle of grid.
     */
    public class Region
    {

        private readonly ICollection<ActiveObject> _members = new List<ActiveObject>();

        public void ActivateMembers(ActiveObject caller)
        {
            foreach (ActiveObject member in _members)
            {
                if (member != caller)
                {
                    member.Activate(caller);
                }
            }
        }

        public void DeactivateMembers(ActiveObject caller)
        {
            foreach (ActiveObject member in _members)
            {
                if (member != caller)
                {
                    member.Deactivate(caller);
                }
            }
        }

        public void AddMember(ActiveObject member)
        {
            _members.Add(member);
        }

        public void RemoveMember(ActiveObject member)
        {
            _members.Remove(member);
        }

        // TODO Replace me somewhere.
        public static IEnumerable<Point> GetRegionRectangle(Rectangle source)
        {
            ICollection<Point> regions = new List<Point>();
            for (int x = source.Left; x < source.Right; ++x)
            {
                for (int y = source.Top; y < source.Bottom; ++y)
                {
                    regions.Add(new Point(x, y));
                }
            }

            return regions;
        }

            // TODO Replace me somewhere.
        public static IEnumerable<Point> GetRegionRectangleDifference(Rectangle source, Rectangle destination)
        {
            ICollection<Point> regions = new List<Point>();
            for (int x = source.Left; x < source.Right; ++x)
            {
                for (int y = source.Top; y < source.Bottom; ++y)
                {
                    regions.Add(new Point(x, y));
                }
            }

            for (int x = destination.Left; x < destination.Right; ++x)
            {
                for (int y = destination.Top; y < destination.Bottom; ++y)
                {
                    regions.Remove(new Point(x, y));
                }
            }

            return regions;
        }

    }
}
