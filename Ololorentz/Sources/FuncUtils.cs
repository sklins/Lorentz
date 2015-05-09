using System;

namespace Ololorentz {
    public delegate float Fu(float t);

    public sealed class FuncException : Exception {
        public FuncException(string format, params object[] args):
            base(String.Format(format, args)) {}
    }

    public static class FuncUtils {
        private const float Eps = 1e-5f;

        public static float FindRoot(Fu f, float t1, float t2) {
            if (t2 < t1) {
                MiscUtils.Swap(ref t1, ref t2);
            }

            float f1 = f(t1), f2 = f(t2);

            if (f1 < 0 && f2 < 0 || f1 > 0 && f2 > 0 || f1 == 0 || f2 == 0) {
                throw new FuncException(
                    "Function, evaluated on the endpoints of the given interval[{0}, {1}]," +
                       "must give opposite signes; given: {2}, {3}",
                    t1, t2, f1, f2);
            }

            float minus = 1;

            if (f1 > 0 && f2 < 0) {
                minus = -1;
                MiscUtils.Swap(ref f1, ref f2);
            }

            MiscUtils.Assert(f1 < 0 && f2 > 0);

            while (Math.Abs(f2 - f1) > Eps) {
                float t0 = MiscUtils.Mean(t1, t2);
                float f0 = minus * f(t0);

                if (f0 > 0) {
                    t2 = t0;
                    f2 = f0;
                } else if (f0 < 0) {
                    t1 = t0;
                    f1 = t0;
                } else {
                    return t0;
                }
            }

            return MiscUtils.Mean(t1, t2);
        }
    }
}
