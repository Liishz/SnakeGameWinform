using SnakeGame.Interfaces;
using SnakeGame.Structs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeGame.SnakeResource {
    class Body : ICollidable {
        Snake snake;
        Head head;
        Part part;

        // Kroppen på ormen.
        internal LinkedList<Part> parts = new LinkedList<Part>();
        public Body(Head head, Snake snake) {
            this.head = head;
            this.snake = snake;

            // Standard storlek på kroppen (2 delar).
            if(head.position.x == (snake.width - snake.width)) {
                part.position = new Position(head.position.x = snake.width, head.position.y);
            } else {
                part.position = new Position(head.position.x - 20, head.position.y);
            }
            part.brush = head.brush;
            parts.AddLast(part);

        }

        internal void Move() {
            // Flyttar sista delen i den position som huvudet tidigare var. 
            part.position = head.position;
            part.brush = head.brush;

            parts.RemoveLast();
            parts.AddFirst(part);
        }

        internal void AddPart(int quantity) {
            while (quantity > 0) {
                var lastPart = parts.Last();
                // Vid tillägg av en ny del, förhindrar att tillagda delen inte hamnar utanför spelplanet. 
                switch (head.direction) {
                    case Direction.Up:
                        // Max Height 600, Max Width 800
                        if (lastPart.position.y == snake.height) {
                            part.position = new Position(lastPart.position.x, lastPart.position.y = (snake.height - snake.height));
                        } else {
                            part.position = new Position(lastPart.position.x, lastPart.position.y + 20);
                        }
                        parts.AddLast(part);
                        break;
                    case Direction.Right:
                        if (lastPart.position.x == (snake.width - snake.width)) {
                            part.position = new Position(lastPart.position.x = snake.width, lastPart.position.y);
                        } else {
                            part.position = new Position(lastPart.position.x - 20, lastPart.position.y);
                        }
                        parts.AddLast(part);
                        break;
                    case Direction.Down:
                        if (lastPart.position.y == (snake.height - snake.height)) {
                            part.position = new Position(lastPart.position.x, lastPart.position.y = snake.height);
                        } else {
                            part.position = new Position(lastPart.position.x, lastPart.position.y - 20);
                        }
                        parts.AddLast(part);
                        break;
                    case Direction.Left:
                        if (lastPart.position.x == snake.width) {
                            part.position = new Position(lastPart.position.x = (snake.width - snake.width), lastPart.position.y);
                        } else {
                            part.position = new Position(lastPart.position.x + 20, lastPart.position.y);
                        }
                        parts.AddLast(part);
                        break;
                }
                quantity--;
            }
        }
        // Ritar ut alla delar i kollision matrisen.
        public void Collide(Collider c) {
            foreach (var p in parts) {
                c.Collide(this, p.position);
            }
        }
        // Om en annan orm kolliderar med en av kroppen.
        public void Collide(Snake s) {
            s.Collide(snake);
        }
        public void Display(IRenderer renderer) {
            foreach (var p in parts) {
                renderer.Draw(p.position, p.brush, Shape.circle);
            }
        }
    }
}
