using SnakeGame.Interfaces;
using SnakeGame.Structs;
using System;
using System.Drawing;

namespace SnakeGame.SnakeResource {
    class Head {
        internal Position position;
        internal Brush brush;
        internal Direction direction;

        static Random random = new Random();

        Snake snake;
        public Head(int x, int y, Snake snake) {
            this.snake = snake;

            position = new Position(x, y);
            brush = new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256)));

            direction = Direction.Right;
        }

        internal void Move() {
            // Gör det möjligt för ormen att åka igenom spelet till andra sidan utan att dö.
            switch (direction) {
                case Direction.Up:
                    position.y = (position.y == (snake.height - snake.height)) ? position.y = snake.height : position.y -= 20;
                    break;
                case Direction.Right:
                    position.x = (position.x < snake.width) ? position.x += 20 : position.x = snake.width - snake.width;
                    break;
                case Direction.Down:
                    position.y = (position.y < snake.height) ? position.y += 20 : position.y = snake.height - snake.height;
                    break;
                case Direction.Left:
                    position.x = (position.x == (snake.width - snake.width)) ? position.x = snake.width : position.x -= 20;
                    break;
            }
        }
        // Byter riktning om den nuvarande riktning inte är motsatsen till den önskade riktning.
        internal void ChangeDirection(Direction direction) {
            if (this.direction != Direction.Down && direction == Direction.Up) {               
                this.direction = direction;
            } 
            else if (this.direction != Direction.Left && direction == Direction.Right) {
                this.direction = direction;
            } 
            else if (this.direction != Direction.Up && direction == Direction.Down) {
                this.direction = direction;
            } 
            else if (this.direction != Direction.Right && direction == Direction.Left) {
                this.direction = direction;
            }
        }
    }
}
