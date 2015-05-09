using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ololorentz {
    public delegate Vector3 Trajectory(float t);

    public interface IScenarioObject {
        IEnumerable<VertexPositionColor> GetTriangulation(float t);
        IEnumerable<VertexPositionColor> GetTransformedTriangulation(
            float t, SpacetimeTransformation tr);
    }

}
