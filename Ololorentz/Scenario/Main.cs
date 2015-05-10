using System;
using System.Windows.Forms;

namespace Ololorentz {
    public static class Program {
        public static void RunAnimation(Scenario scenario) {
            var scene = new Scene(scenario);
            scene.Run();
        }

        public static void Main(string[] args) {
            var setupForm = new SetupForm(new PoleAndBarn());
            setupForm.Show();
            Application.Run();
        }
    }
}
