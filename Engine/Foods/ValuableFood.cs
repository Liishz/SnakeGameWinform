using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Interfaces;
using SnakeGame.SnakeResource;

namespace SnakeGame.Foods {
    class ValuableFood : Food {
        public ValuableFood(int x, int y, Engine game) : base(x, y, new SolidBrush(Color.Yellow), game) { }

        public override void Collide(Collider c) {
            c.Collide(this, position);
        }

        public override void Display(IRenderer renderer) {
            renderer.Draw(base.position, base.brush, Shape.rectangle);
        }

        public override void Collide(Snake s) {
            Settings.Effect(s, IncreaseScore, IncreaseTail);
            game.Remove(this);
            game.DecreaseFoodCount();
        }

        private int IncreaseScore { get => 5; }
        private int IncreaseTail { get => 2; }

    }
}
