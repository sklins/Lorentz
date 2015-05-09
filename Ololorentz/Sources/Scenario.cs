using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ololorentz {
    public sealed class Scenario {
        public IScenarioObject[] Objects { get; set; }
        public float MinTime { get; set; } = 0.0f;
        public float MaxTime { get; set; } = 1.0f;
        public float TimeStep { get; set; } = 0.001f;

        public float SpeedOfLight { get; set; } = 2.0f;
        public float LorentzBoostSpeed { get; set; } = 0.0f;

        public SpacetimeTransformation LorentzBoost {
            get {
                return LorentzUtils.LorentzBoost(LorentzBoostSpeed, SpeedOfLight);
            }
        }

        public Vector3 CameraPosition { get; set; } = new Vector3(0, 5, -10.0f);
        public Vector3 CameraTarget { get; set; } = new Vector3(0, 0, 0.0f);

        public float FovAngle { get; set; } = MathHelper.ToRadians(90);
        public float NearPlane { get; set; } = 0.01f;
        public float FarPlane { get; set; } = 100f;

        public IEnumerable<VertexPositionColor> GetTriangulation(float t) {
            foreach (IScenarioObject p in Objects) {
                foreach (VertexPositionColor vpc in p.GetTriangulation(t))
                    yield return vpc;
            }
        }

        public IEnumerable<VertexPositionColor> GetTransformedTriangulation(
            float t, SpacetimeTransformation tr)
        {
            foreach (IScenarioObject p in Objects) {
                foreach (VertexPositionColor vpc in
                    p.GetTransformedTriangulation(t, tr))
                {
                    yield return vpc;
                }
            }
        }
    }
}
