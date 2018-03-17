using SnakeGame;
using System.Drawing;
using System.Windows.Forms;
using WinFormsGUI;
using WinFormsGUI.Menu;

namespace WinFormsGUI.Board {
    /// <summary>
    /// Vänstra panel. Har komponenter som spelmenyn och val av antal spelare.  
    /// </summary>
    public class LeftBoard : Control {
        Engine game;
        GameMenu menu;
        CountMenu countMenu;
        FlowLayoutPanel layout;
        public LeftBoard(Engine game, int width, int height) : base() {
            this.game = game;
            DoubleBuffered = true;

            layout = new FlowLayoutPanel();
            layout.FlowDirection = FlowDirection.TopDown;
            layout.AutoSize = true;

            BackColor = Color.White;
            Width = width;
            Height = height;

            menu = new GameMenu();
            menu.BringToFront();

            countMenu = new CountMenu();
            countMenu.Margin = new Padding(0, 0, 0, 40);
            countMenu.BringToFront();

            layout.Controls.Add(countMenu);
            layout.Controls.Add(menu);

            layout.Location = new Point((Width - menu.Width) / 2 + 100, (Height - menu.Height) / 2 - 100);
            Controls.Add(layout);

            Paint += Score_Paint;
        }

        public Button Start { get => menu.Start; }
        public Button Quit { get => menu.Quit; }
        public int NumberOfPlayers { get => countMenu.PlayerCount; }
        public bool MenuVisible { set => layout.Visible = !layout.Visible; }
        
        // Ritar ut poängen för varje spelare.
        void Score_Paint(object sender, PaintEventArgs e) {
            var renderer = new GraphicsRenderer(e.Graphics);
            game.DisplayScore(renderer);
        }


    }
}
