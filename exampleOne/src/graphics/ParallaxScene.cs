using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ExampleOne;

public class ParallaxScene
{
    public List<Layer> Layers = new();
    public class Layer
    {
        public string ID { get; set; }
        public Texture2D Texture { get; set; }
        public float Depth { get; set; }
        public Vector2 Position { get; set; }
        public float SpeedScalar { get; set; }

        public Layer(string id, Texture2D texture, float depth, Vector2 position = default, float speedScalar = 1f)
        {
            ID = id;
            Texture = texture;
            Depth = depth;
            Position = position;
            SpeedScalar = speedScalar;
        }
    }
    public void AddLayer(Layer newLayer)
    {
        Layers.Add(newLayer);
    }
}
