using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGUI.Menu {
    /// <summary>
    /// En komponent som gör det möjligt att välja antal deltagare som ska spela.
    /// </summary>
    class CountMenu : FlowLayoutPanel {
        NumericUpDown number;
        Label label;
        public CountMenu() {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            label = new Label();
            label.Text = "Players: ";
            label.Font = new Font("Arial", 16);

            number = new NumericUpDown();
            number.Width = 50;
            number.Value = 1;
            number.Maximum = 4;
            number.Minimum = 1;

            Controls.Add(label);
            Controls.Add(number);
        }

        public int PlayerCount { get => (int)number.Value; }

    }
}
