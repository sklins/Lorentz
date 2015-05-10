using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ololorentz {
    public sealed class ScenarioParameterAttribute : Attribute {
        public string DefaultValue { get; set; }
    }

    public sealed class ScenarioParamInfo {
        public Type Type { get; set; }
        public string DefaultValue { get; set; }
    }

    public abstract class ScenarioBuilder {
        public abstract string ScenarioTitle { get; }
        public abstract Scenario BuildScenario();

        private IEnumerable<PropertyInfo> GetScenarioPropertyInfos() {
            // ScenarioBuilder is a polymorphic type.
            // Extracting the monomorphic specification t through reflection:
            Type t = this.GetType();

            foreach (PropertyInfo pi in t.GetProperties()) {
                if (pi.GetCustomAttribute<ScenarioParameterAttribute>() != null) {
                    // Attribute is present, we are dealing with a scenario parameter here
                    yield return pi;
                }
            }
        }

        public Dictionary<string, ScenarioParamInfo> GetScenarioParameters() {
            var res = new Dictionary<string, ScenarioParamInfo>();
            foreach (PropertyInfo pi in GetScenarioPropertyInfos()) {
                res.Add(pi.Name, new ScenarioParamInfo() {
                    Type = pi.PropertyType,
                    DefaultValue = pi.GetCustomAttribute<ScenarioParameterAttribute>().DefaultValue
                });
            }
            return res;
        }

        public void SetScenarioParameters(Dictionary<string, string> p) {
            foreach (PropertyInfo pi in GetScenarioPropertyInfos()) {
                if (p.ContainsKey(pi.Name)) {
                    pi.SetValue(this, ScenarioParamParser.Parse(pi.PropertyType, p[pi.Name]));
                }
            }
        }
    }
}
