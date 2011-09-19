using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNA.model.grid
{
    public class Grid
    {
        public const int REGION_SIZE = 64; // pixels
        public static readonly int REGIONS_IN_ROW;
        public static readonly int REGIONS_IN_COLUMN;

        public delegate void onChangeRegionDelegate(Object target, Vector2 source, Vector2 destination);
        public event onChangeRegionDelegate onChangeRegion;

        // region map.
        internal Region[,] regions;

        // provides information about object position (region).
        private Dictionary<int, Vector2> objectMap;

        static Grid()
        {
            REGIONS_IN_ROW = (int)Math.Ceiling((double)Game1.SCREEN_WIDTH / REGION_SIZE);
            REGIONS_IN_COLUMN = (int)Math.Ceiling((double)Game1.SCREEN_HEIGHT / REGION_SIZE);
        }

        public Grid()
        {
            this.regions = new Region[REGIONS_IN_ROW, REGIONS_IN_COLUMN];
            this.objectMap = new Dictionary<int, Vector2>();
        }

        public void placeIn(MoveableObject target, Vector2 coordinates)
        {
            Vector2 sourceRegion = objectMap[target.objectID];
            Vector2 destinationRegion = determineRegion(coordinates);

            if (sourceRegion != destinationRegion)
            {
                onChangeRegion(target, sourceRegion, destinationRegion);

                // switch region.
                regions[(int)sourceRegion.X, (int)sourceRegion.Y].members.Remove(target);
                regions[(int)destinationRegion.X, (int)destinationRegion.Y].members.Add(target);

                // update object position.
                objectMap[target.objectID] = destinationRegion;
            }

        }

        private Vector2 determineRegion(Vector2 coordinates)
        {
            return new Vector2((float)Math.Floor(coordinates.X / REGION_SIZE), (float)Math.Floor(coordinates.Y / REGION_SIZE));
        }
    }

    /**
     * Each object which should be tracked in grid should be derived from this class.
     */
    public abstract class MoveableObject
    {
        private static int objectCounter = 0;

        public Vector2 coordinates;

        // for Grid class as identifier.
        internal int objectID;

        public MoveableObject()
        {
            this.objectID = ++MoveableObject.objectCounter;
        }

        public void update()
        {
            GameModel.instance.grid.placeIn(this, coordinates);
        }
    }

    /**
     * A particle of grid.
     */
    internal class Region {

        public List<MoveableObject> members;

        public Region()
        {
            this.members = new List<MoveableObject>();
        }
    }
}
