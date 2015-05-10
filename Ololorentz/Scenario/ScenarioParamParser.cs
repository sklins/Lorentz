using System;
using System.Reflection;

namespace Ololorentz {
    public static class ScenarioParamParser {
        public static object Parse(Type t, string s) {
            if (t == typeof(string)) {
                return s;
            } else if (t == typeof(bool)) {
                if (s == "true" || s == "+" || s == "1" || s == "yes" || s == "y")
                    return true;
                else if (s == "false" || s == "-" || s == "0" || s == "no" || s == "n")
                    return false;
                else throw new AssertionException("Unsupported boolean literal: {0}", s);
            } else if (t == typeof(float)) {
                return Convert.ToSingle(s);
            } else {
                MethodInfo mi = t.GetMethod("Parse");
                MiscUtils.Assert(mi != null, "No known conversion from string to {0}", t.Name);
                object res = mi.Invoke(null, new object[] {s});
                return res;
            }
        }
    }
}
