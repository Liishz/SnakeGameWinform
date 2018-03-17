using System;
using SnakeGame;
using System.Windows.Forms;
using WinFormsGUI.Board;

namespace WinFormsGUI {
    /// <summary>
    /// Användargränssnittet, lägger till alla nödvänliga kontroller t.e.x spelmenyn och val av antal spelare.
    /// </summary>
    public class GameForm : Form {
        int width = 800, height = 600;
        int FPS = 40;

        Engine game;

        Timer timer;

        GameBoard gameBoard;
        ScoreBoard scoreBoard;
        LeftBoard leftBoard;

        AvailableController availableController;

        public GameForm() : base() {
            AutoSize = true;

            Text = "Snake Game";
            availableController = new AvailableController();

            timer = new Timer();
            timer.Interval = 1000 / FPS;
            timer.Tick += Timer_Tick;

            var flow = new FlowLayoutPanel();
            flow.FlowDirection = FlowDirection.LeftToRight;
            flow.AutoSize = true;

            game = new Engine(width, height, timer.Interval);
            game.GameOver += Game_GameOver;

            leftBoard = new LeftBoard(game, 300, height);
            flow.Controls.Add(leftBoard);

            gameBoard = new GameBoard(game, width, height);
            flow.Controls.Add(gameBoard);

            scoreBoard = new ScoreBoard(game, width, height);
            flow.Controls.Add(scoreBoard);

            scoreBoard.Visible = false;

            Controls.Add(flow);

            // Lägger till alla möjliga par av tangentnedtryckning.
            game.AddKeyboard(availableController.GetAvailableController());

            leftBoard.Start.Click += Start_Click;
            leftBoard.Quit.Click += Quit_Click;

            // Lyssnar på event från tangentbordet.
            KeyDown += IsKeyPressed;
            KeyPreview = true;

        }

        private void Timer_Tick(object sender, EventArgs e) {
            game.Tick();
            gameBoard.Refresh();
            leftBoard.Refresh();
        }

        void Game_GameOver() {
            gameBoard.Visible = false;

            timer.Stop();

            scoreBoard.Visible = true;
            scoreBoard.Refresh();

            leftBoard.MenuVisible = true;
        }

        private void Start_Click(object sender, EventArgs e) {
            scoreBoard.Visible = false;

            gameBoard.Visible = true;
            // Antal deltagare som ska spela.
            game.NewGame(leftBoard.NumberOfPlayers);

            timer.Start();
            leftBoard.MenuVisible = false;
        }

        private void Quit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void IsKeyPressed(object sender, KeyEventArgs e) {
            game.KeyIsPressed(e.KeyData);
        }

    }
}
