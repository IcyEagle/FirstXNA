﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XNA.model.grid
{
    /**
     * Each object which should be tracked in grid should be derived from this class.
     */
    public class ActiveObject
    {
        private static int objectCounter = 0;

        // for Grid class as identifier.
        internal int objectID;

        // TEMP
        public Object master;

        public ActiveObject(Object master)
        {
            this.objectID = ++ActiveObject.objectCounter;

            this.master = master;
        }

        public void UpdatePosition(Vector2 coordinates)
        {
            GameModel.instance.grid.moveTo(this, coordinates);
        }
    }
}