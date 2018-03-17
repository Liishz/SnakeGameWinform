using SnakeGame.Structs;
using System.Collections.Generic;
using System.Drawing;

namespace SnakeGame.Interfaces {
    public enum Shape { circle, rectangle }
    public interface IRenderer { 
        void Draw(Position position, Brush brush, Shape shape);
        void DrawScore(Brush brush, int score, int id);
        void DrawResult(List<KeyValuePair<int, int>> snake);
    }
}
