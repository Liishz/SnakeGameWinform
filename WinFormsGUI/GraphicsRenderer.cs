using System;
using System.Collections.Generic;
using System.Drawing;
using SnakeGame;
using SnakeGame.Interfaces;
using SnakeGame.Structs;

namespace WinFormsGUI {
    public class GraphicsRenderer : IRenderer {
        Graphics graphics;
        Font font = new Font("Arial", 15);

        public GraphicsRenderer(Graphics graphics) {
            this.graphics = graphics;
        }

        public void DrawScore(Brush brush, int score, int id) {
            var pointF = new PointF(100 / 2, 50 * id);
            DrawScore(pointF, brush, score, id);
        }
        private void DrawScore(PointF position, Brush brush, int score, int id) {
            var s = $"Player: {id} Score: {score}";
            graphics.DrawString(s, font, brush, position);
        }

        public void DrawResult(List<KeyValuePair<int,int>> snake) {
            string text;
            int rank = 1;

            // rangordnar spelarna från högst till lägst.  
            graphics.DrawString("Match Results", new Font("Arial", 20), new SolidBrush(Color.Black), new PointF(250, 30));
            foreach (var s in snake) {
                if(rank == 1) {
                    text = $"Player: {s.Key} Score: {s.Value}";
                    graphics.DrawString(text, font, new SolidBrush(Color.Gold), new PointF(250, 90 * rank++));
                } else if(rank == 2) {
                    text = $"Player: {s.Key} Score: {s.Value}";
                    graphics.DrawString(text, font, new SolidBrush(Color.Silver), new PointF(250, 70 * rank++));
                } else {
                    text = $"Player: {s.Key} Score: {s.Value}";
                    graphics.DrawString(text, font, new SolidBrush(Color.Brown), new PointF(250, 60 * rank++));
                }
            }
        }

        // circle är ormar och rectangle är mat.
        public void Draw(Position position, Brush brush, Shape shape) {
            switch (shape) {
                case Shape.circle: graphics.FillEllipse(brush, new Rectangle(position.x, position.y, 20, 20)); break;
                case Shape.rectangle: graphics.FillRectangle(brush, new Rectangle(position.x, position.y, 20, 20)); break;
            }
        }

    }
}
