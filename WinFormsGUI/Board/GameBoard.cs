using SnakeGame;
using SnakeGame.SnakeResource;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsGUI.Board {
    public class GameBoard : Control {
        Engine game;

        public GameBoard(Engine game, int width, int height) : base() {
            this.game = game;

            DoubleBuffered = true;

            BackColor = Color.White;
            Width = width;
            Height = height;

            Paint += GameBoard_Paint;
        }
        // Ritar ut figurerna på bordet.
        void GameBoard_Paint(object sender, PaintEventArgs e) {
            var renderer = new GraphicsRenderer(e.Graphics);
            game.Display(renderer);
        }

    }
}