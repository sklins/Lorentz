using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ololorentz {
    public sealed class Polygon : IScenarioObject {
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

        private Vector3 TransformedNodePosition(Trajectory f, float t, SpacetimeTransformation tr) {
            float s = LorentzUtils.FindProjectionRemoteTime(f, tr, t);

            SpacetimeEvent transformed = tr(new SpacetimeEvent(s, f(s)));

            #if DEBUG
            Console.WriteLine("[{0}] -> {1}, {2}", s, transformed.Instant, transformed.Position);
            #endif

            return transformed.Position;
        }

        public IEnumerable<VertexPositionColor> GetTransformedTriangulation(
            float t, SpacetimeTransformation tr)
        {
            int n = Nodes.Length - 1;

            for (int i = 1; i < n; i++) {
                yield return new VertexPositionColor(TransformedNodePosition(Nodes[0], t, tr), Color);
                yield return new VertexPositionColor(TransformedNodePosition(Nodes[i], t, tr), Color);
                yield return new VertexPositionColor(TransformedNodePosition(Nodes[i + 1], t, tr), Color);
            }
        }
    }
}