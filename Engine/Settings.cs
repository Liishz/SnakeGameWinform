using SnakeGame.SnakeResource;

namespace SnakeGame {
    /// <summary>
    /// Beteende som kan påverka ormarna.
    /// </summary>
    public static class Settings {
        public static void Effect(Snake s, int score) {
            s.Increase(score);
        }
        public static void Effect(Snake s, int score, int count) {
            s.Increase(score, count);
        }
        public static void SpeedUp(Snake s) {
            s.IncreaseSpeed();
        }
    }
}
