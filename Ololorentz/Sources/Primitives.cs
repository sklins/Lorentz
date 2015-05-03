using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ololorentz {
    public delegate Vector3 Trajectory(float t);

    public sealed class Polygon {
        public Trajectory[] Nodes { get; private set; }
        public Color Color { get; private set; }

        public Polygon(Trajectory[] nodes, Color color) {
            this.Nodes = nodes;
            this.Color = color;
        }

        public IEnumerable<VertexPositionColor> GetTriangulation(float t) {
            int n = Nodes.Length - 1;
            for (int i = 1; i < n; i++) {
                yield return new VertexPositionColor(Nodes[0](t), Color);
                yield return new VertexPositionColor(Nodes[i](t), Color);
                yield return new VertexPositionColor(Nodes[i + 1](t), Color);
            }
        }
    }

    public sealed class Scenario {
        public Polygon[] Polygons { get; set; }
        public float MinTime { get; set; } = 0.0f;
        public float MaxTime { get; set; } = 1.0f;
        public float TimeStep { get; set; } = 0.001f;

        public Vector3 CameraPosition { get; set; } = new Vector3(0, 5, -10.0f);
        public Vector3 CameraTarget { get; set; } = new Vector3(0, 0, 0.0f);

        public float FovAngle { get; set; } = MathHelper.ToRadians(90);
        public float NearPlane { get; set; } = 0.01f;
        public float FarPlane { get; set; } = 100f;

        public IEnumerable<VertexPositionColor> GetTriangulation(float t) {
            foreach (Polygon p in Polygons) {
                foreach (VertexPositionColor vpc in p.GetTriangulation(t))
                    yield return vpc;
            }
        }
    }
}
