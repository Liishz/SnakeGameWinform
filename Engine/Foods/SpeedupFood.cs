using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Interfaces;
using SnakeGame.SnakeResource;

namespace SnakeGame.Foods {
    class SpeedupFood : Food {
        public SpeedupFood(int x, int y, Engine game) : base(x, y, new SolidBrush(Color.Red), game) { }
        static Random random = new Random();

        public override void Collide(Collider c) {
            c.Collide(this, position);
        }

        public override void Collide(Snake s) {
            // Väljer ut en orm i animtables och ökar hastigheten på ormen.
            Snake[] snakeArray = new Snake[game.Animatables.Count];
            game.Animatables.CopyTo(snakeArray, 0);

            var index = random.Next(snakeArray.Count());
            Settings.SpeedUp(snakeArray[index]);

            game.Remove(this);
            game.DecreaseFoodCount();
        }

        public override void Display(IRenderer renderer) {
            renderer.Draw(base.position, base.brush, Shape.rectangle);
        }

    }
}
