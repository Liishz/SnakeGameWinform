using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGUI.Menu {
    /// <summary>
    /// Spelmenyn för att kunna starta och avsluta spelet.
    /// </summary>
    public class GameMenu : FlowLayoutPanel {
        public Button Start { get; }
        public Button Quit { get; }

        public GameMenu() {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            int W = 200, H = 50;

            Start = new Button();
            Start.Width = W;
            Start.Height = H;
            Start.Text = "New Game";
            Start.Margin = new Padding(5);
            Start.BackColor = Color.Black;
            Start.ForeColor = Color.White;

            Quit = new Button();
            Quit.Text = "Quit";
            Quit.Width = W;
            Quit.Height = H;
            Quit.Margin = new Padding(5);
            Quit.BackColor = Color.Black;
            Quit.ForeColor = Color.White;

            FlowDirection = FlowDirection.TopDown;

            Controls.Add(Start);
            Controls.Add(Quit);
        }
    }
}
