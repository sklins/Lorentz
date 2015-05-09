using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ololorentz {
    public static class Tetrahedron {
        public static Scenario GetScenario() {
            Vector3
            _a = new Vector3(-5, -5, -3),
            _b = new Vector3(5, -5, -3),
            _c = new Vector3(0, 5, -3),
            _d = new Vector3(0, 0, 6);

            Func<float, Matrix> f = t => Matrix.CreateRotationY(t * 2 * MathHelper.ToRadians(360)) *
                Matrix.CreateRotationX(t * 0.1f * MathHelper.ToRadians(360)) *
                Matrix.CreateTranslation((0.5f - t) * 20, 0, (0.5f - t) * (0.5f - t) * 100);

            Trajectory
            a = t => Vector3.Transform(_a, f(t)),
            b = t => Vector3.Transform(_b, f(t)),
            c = t => Vector3.Transform(_c, f(t)),
            d = t => Vector3.Transform(_d, f(t));

            Polygon s_abc = new Polygon(new Trajectory[] {a, b, c}, Color.Green);
            Polygon s_abd = new Polygon(new Trajectory[] {a, b, d}, Color.Yellow);
            Polygon s_acd = new Polygon(new Trajectory[] {a, c, d}, Color.Red);
            Polygon s_bcd = new Polygon(new Trajectory[] {b, c, d}, Color.Purple);

            return new Scenario() {
                Objects = new ISceneObject[] {s_abc, s_abd, s_acd, s_bcd}
            };
        }
    }
}
