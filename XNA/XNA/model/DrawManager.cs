using System.Collections.Generic;
using XNA.model.@base;

namespace XNA.model
{
    public class DrawManager
    {

        public enum DrawLayerType
        {
            Background = 8,
            BackWall = 7,
            Bullet = 6,
            StaticBlock = 5,
            BackItem = 4,
            Character = 3,
            PickableItem = 2,
            Interface = 1
        }

        private readonly Dictionary<DrawLayerType, List<DrawableObject>> _layers;

        private List<DrawLayerType> _excludeLayers;

        public DrawManager()
        {
            _layers = new Dictionary<DrawLayerType, List<DrawableObject>>();
        }

        public void Draw()
        {
            foreach (var keyValuePair in _layers)
            {
                if (_excludeLayers != null && _excludeLayers.IndexOf(keyValuePair.Key) != -1) continue;
                foreach (var drawableObject in keyValuePair.Value)
                {
                    drawableObject.Draw();
                }
            }
        }

        public void AddObjectForDraw(DrawableObject drawableObject)
        {
            if (!_layers.ContainsKey(drawableObject.DrawLayerType))
            {
                _layers[drawableObject.DrawLayerType] = new List<DrawableObject>();
            }
            _layers[drawableObject.DrawLayerType].Add(drawableObject);
        }

        public void RemoveObjectForDraw(DrawableObject drawableObject)
        {
            _layers[drawableObject.DrawLayerType].Remove(drawableObject);
        }

        public void LayerVisibleOff(DrawLayerType drawLayerType)
        {
            if (_excludeLayers == null)
            {
                _excludeLayers = new List<DrawLayerType>();
            }
            if (_excludeLayers.IndexOf(drawLayerType) == -1)
            {
                _excludeLayers.Add(drawLayerType);
            }
        }

        public void LayerVisibleOn(DrawLayerType drawLayerType)
        {
            if (_excludeLayers != null && _excludeLayers.IndexOf(drawLayerType) != -1)
            {
                _excludeLayers.Remove(drawLayerType);
            }
        }

    }
}
