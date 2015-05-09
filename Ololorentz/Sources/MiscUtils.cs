using System;
using System.Linq;

namespace Ololorentz {
    public sealed class AssertionException : Exception {
        public AssertionException(string format, params object[] args):
            base(String.Format(format, args)) {}
    }

    public static class MiscUtils {
        public static void Assert(bool expr, string format, params object[] args) {
            if (!expr)
                throw new AssertionException(format, args);
        }

        public static void Assert(bool expr) {
            Assert(expr, "");
        }

        public static void Swap<T>(ref T a, ref T b) {
            T t = a;
            a = b;
            b = t;
        }

        public static float Mean(params float[] args) {
            return args.Aggregate((a, b) => a + b) / args.Length;
        }
    }
}
