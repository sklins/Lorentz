using System;
using Microsoft.Xna.Framework;

namespace Ololorentz {
    public struct SpacetimeEvent {
        public float Instant;
        public Vector3 Position;

        public SpacetimeEvent(float instant, Vector3 position) {
            this.Instant = instant;
            this.Position = position;
        }
    }

    public delegate SpacetimeEvent SpacetimeTransformation(SpacetimeEvent e);

    public static class LorentzUtils {
        private static SpacetimeEvent ApplyLorentzBoost(SpacetimeEvent e, float v, float c) {
            // Dimensionless velocity
            float beta = v / c;

            // The relativistic gamma factor
            float gamma = (float) (1 / Math.Sqrt(1 - Math.Pow((double)beta, 2)));

            // The time and X coordinates of the space-time event e
            float t = e.Instant, x = e.Position.X;

            // Transforming the coordinates (a hyperbolic rotation)
            float tNew = (t - beta * x / c) * gamma;
            float xNew = (x - v * t) * gamma;

            return new SpacetimeEvent(tNew, new Vector3(xNew, e.Position.Y, e.Position.Z));
        }

        /// <summary>Gives a space-time transformation corresponding to the
        /// Lorentz boost along the X axis.</summary>
        /// <param name="v">The velocity parameterizing the boost.</param>
        /// <param name="c">The speed of light.</param>
        public static SpacetimeTransformation LorentzBoost(float v, float c) {
            return (SpacetimeEvent e) => ApplyLorentzBoost(e, v, c);
        }

        /// <summary>Projects a space-time trajectory on the space-like hypersurface.</summary>
        /// <returns>The remote time, which gives <c>t</c> when projected on the hypersurface.</returns>
        /// <param name="f">The space-time trajectory.</param>
        /// <param name="tr">The space-time transformation to use in the projection.</param>
        /// <param name="t">The local time (parameterizing the hypersurface).</param>
        public static float FindProjectionRemoteTime(Trajectory f, SpacetimeTransformation tr, float t)
        {
            // This function spawns an equation to solve: h(z) = 0
            Fu h = z => tr(new SpacetimeEvent(z, f(z))).Instant - t;

            // Solving the equation above.
            return FuncUtils.FindRoot(h);
        }
    }
}
