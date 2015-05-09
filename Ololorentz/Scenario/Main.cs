using System;

namespace Ololorentz {
    public static class Program {
        private static void RunAnimation(Scenario scenario) {
            var scene = new Scene(scenario);
            scene.Run();
        }

        public static void Main(string[] args) {
            RunAnimation(PoleAndBarn.GetScenario());
        }
    }
}
