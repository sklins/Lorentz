using System;
using System.Reflection;
using System.Windows.Forms;

namespace Ololorentz {
    public sealed class ChooseScenarioForm : Form {
        private ListBox scenariosList = new ListBox();
        private ScenarioBuilder[] scenarioBuilders;

        public ChooseScenarioForm(Type[] scenarios) {
            this.Text = "Choose scenario..";
            InitializeModel(scenarios);
            InitializeComponent();
        }

        private void InitializeModel(Type[] scenarios) {
            scenarioBuilders = new ScenarioBuilder[scenarios.Length];
            for (int i = 0; i < scenarios.Length; i++) {
                ConstructorInfo ci = scenarios[i].GetConstructor(new Type[] {});
                scenarioBuilders[i] = ci.Invoke(new object[] {}) as ScenarioBuilder;
                scenariosList.Items.Add(scenarioBuilders[i].ScenarioTitle);
            }
        }

        private void InitializeComponent() {
            scenariosList.Dock = DockStyle.Fill;
            Controls.Add(scenariosList);

            scenariosList.DoubleClick += (sender, e) => {
                int ind = scenariosList.SelectedIndex;
                if (ind < 0 || ind >= scenarioBuilders.Length)
                    return;
                new SetupForm(scenarioBuilders[ind]).ShowDialog();
            };
        }
    }
}
