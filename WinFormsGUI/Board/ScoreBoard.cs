using SnakeGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGUI.Board {
    /// <summary>
    /// Skriver ut matchresult.
    /// </summary>
    class ScoreBoard : Control {

        Engine game;
        public ScoreBoard(Engine game, int width, int height) : base() {
            this.game = game;

            DoubleBuffered = true;

            BackColor = Color.White;
            Width = width;
            Height = height;

            Paint += ScoreBoard_Paint;
        }

        void ScoreBoard_Paint(object sender, PaintEventArgs e) {
            var renderer = new GraphicsRenderer(e.Graphics);
            game.DisplayResult(renderer);
        }

    }
}
