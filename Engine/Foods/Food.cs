using System;
using System.Collections.Generic;
using System.Drawing;
using SnakeGame.Interfaces;
using SnakeGame.SnakeResource;
using SnakeGame.Structs;

namespace SnakeGame.Foods {
    public abstract class Food : ICollidable  {

        protected Position position;
        protected Brush brush;
        protected Engine game;

        public Food(int x, int y, Brush brush, Engine game) {
            position = new Position(x, y);
            this.brush = brush;
            this.game = game;
        }
        public abstract void Collide(Collider c);
        public abstract void Collide(Snake s);
        public abstract void Display(IRenderer renderer);

    }
}
