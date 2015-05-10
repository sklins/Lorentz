using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ololorentz {
    public sealed class SetupForm : Form {
        #pragma warning disable
        private readonly ScenarioBuilder scenarioBuilder;
        #pragma warning restore

        public SetupForm(ScenarioBuilder scenarioBuilder) {
            this.scenarioBuilder = scenarioBuilder;
            this.Text = scenarioBuilder.ScenarioTitle;
            this.Width = 640;
            this.Height = 320;
            InitializeModel();
            InitializeComponent();
        }

        private sealed class ParamControlSet {
            public TextBox TextBox { get; set; }
            public CheckBox CheckBox { get; set; }
            public Label Label { get; set; }
        }

        private Dictionary<string, ParamControlSet> paramControls =
            new Dictionary<string, ParamControlSet>();

        private Button runBtn = new Button();
        private Button closeBtn = new Button();

        private void InitializeModel() {
            Dictionary<string, ScenarioParamInfo> p = scenarioBuilder.GetScenarioParameters();
            foreach (string k in p.Keys) {
                var label = new Label();
                label.Text = String.Format("{1} ({0}): ", p[k].Type.Name.ToLower(), k);

                TextBox textbox = null;
                CheckBox checkbox = null;

                if (p[k].Type == typeof(bool)) {
                    checkbox = new CheckBox();
                    checkbox.Text = "";
                    checkbox.Checked = (bool) ScenarioParamParser.Parse(typeof(bool), p[k].DefaultValue);
                } else {
                    textbox = new TextBox();
                    textbox.Text = p[k].DefaultValue;
                }

                paramControls.Add(k, new ParamControlSet() {
                    TextBox = textbox,
                    CheckBox = checkbox,
                    Label = label
                });
            }
        }

        private const int Separator = 10;
        private const int LabelWidth = 250;

        private void InitializeComponent() {
            int i = 0;
            foreach (ParamControlSet pcs in paramControls.Values) {
                Label l = pcs.Label;
                TextBox tb = pcs.TextBox ?? new TextBox();
                CheckBox cb = pcs.CheckBox;

                l.Left = Separator;
                l.Top = Separator + (tb.Height + Separator) * i;
                l.Width = LabelWidth;
                Controls.Add(l);

                if (cb != null) {
                    cb.Left = Separator * 2 + LabelWidth;
                    cb.Top = Separator + (tb.Height + Separator) * (i++);
                    Controls.Add(cb);
                } else {
                    tb.Left = Separator * 2 + LabelWidth;
                    tb.Top = Separator + (tb.Height + Separator) * (i++);
                    tb.Width = this.Width - 3 * Separator - LabelWidth;
                    Controls.Add(tb);
                }
            }

            runBtn.Left = Separator;
            runBtn.Top = Separator + (new TextBox().Height + Separator) * i;
            runBtn.Text = "Run";
            Controls.Add(runBtn);

            closeBtn.Left = Separator * 2 + runBtn.Width;
            closeBtn.Top = runBtn.Top;
            closeBtn.Text = "Close";
            Controls.Add(closeBtn);

            closeBtn.Click += (sender, e) => {
                Close();
            };

            runBtn.Click += (sender, e) => {
                RunScenario();
            };
        }

        private void RunScenario() {
            var props = new Dictionary<string, string>();
            foreach (string k in paramControls.Keys) {
                if (paramControls[k].CheckBox != null) {
                    props[k] = paramControls[k].CheckBox.Checked ? "true" : "false";
                } else {
                    props[k] = paramControls[k].TextBox.Text;
                }
            }

            try {
                scenarioBuilder.SetScenarioParameters(props);
            } catch (Exception e) {
                MessageBox.Show("Parameter parsing exception:\n" + e.ToString());
                return;
            }

            var scenario = scenarioBuilder.BuildScenario();
            Program.RunAnimation(scenario);
        }
    }
}
