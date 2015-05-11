using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Ololorentz {
    public sealed class HelpForm : Form {
        private readonly TextBox helpTextBox =
            new TextBox();

        public HelpForm() {
            InitializeComponent();
        }

        private void InitializeComponent() {
            this.Width = 480;
            this.Height = 640;
            this.Text = "Help";

            helpTextBox.Multiline = true;
            helpTextBox.Dock = DockStyle.Fill;
            helpTextBox.ReadOnly = true;

            Controls.Add(helpTextBox);

            Stream resourceStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Help.txt");

            using (var reader = new StreamReader(resourceStream)) {
                helpTextBox.Text = reader.ReadToEnd();
            }

            helpTextBox.HideSelection = true;
        }
    }
}
