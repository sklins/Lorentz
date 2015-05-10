using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Ololorentz {
    public static class Program {
        public static void RunAnimation(Scenario scenario) {
            var scene = new Scene(scenario);
            scene.Run();
        }

        private static IEnumerable<Type> GetScenarioTypes(Assembly asm) {
            foreach (Type t in asm.GetTypes()) {
                if (t.GetTypeInfo().IsSubclassOf(typeof(ScenarioBuilder)))
                    yield return t;
            }
        }

        public static void Main(string[] args) {
            Type[] scenarioTypes = GetScenarioTypes(Assembly.GetExecutingAssembly()).ToArray();

            var chooseScenarioForm = new ChooseScenarioForm(scenarioTypes);
            chooseScenarioForm.Show();
            Application.Run(chooseScenarioForm);
        }
    }
}
