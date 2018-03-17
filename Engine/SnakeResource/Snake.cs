using SnakeGame.Interfaces;
using SnakeGame.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SnakeGame.SnakeResource {
    public class Snake : IAnimatable {
        int id;
        int score = 0;
        internal int width, height;

        Trigger moveTrigger;
        Engine game;

        Head head;
        Body body;

        public Snake(int x, int y, int width, int height, Engine engine, int timer, int id) {
            this.width = width - 20; this.height = height - 20;
            this.id = id;

            game = engine;
            head = new Head(x, y, this);
            body = new Body(head, this);

            moveTrigger = new Trigger(timer);
            moveTrigger.Triggered += Move;

            // Lägger till body i collidables så att kroppen ritas ut före huvudet.
            game.Add(body);
        }
        // Motverkar keySpam. Lagrar upp till 2 keys per tur.
        LinkedList<Direction> keyInputBuffer = new LinkedList<Direction>();
        private void Move() {
            // Ändrar riktning efter varje tur och tar bort 1 key i buffret. 
            if (keyInputBuffer.Count > 0) {
                head.ChangeDirection(keyInputBuffer.First.Value);
                keyInputBuffer.RemoveFirst();
            }
            body.Move();
            head.Move();
        }

        public void ChangeDirection(Direction d) {
            // Lägger inte till i buffer om buffret är fylld till 2 keys.
            if (keyInputBuffer.Count != 2) {
                keyInputBuffer.AddLast(d);
            }
        }
        #region Ritar ut alla poäng och figurer.
        public void Display(IRenderer renderer) {
            renderer.Draw(head.position, head.brush, Shape.circle);
        }
        public void DisplayScore(IRenderer renderer) {
            renderer.DrawScore(head.brush, score, id);
        }
        #endregion

        #region Poänghantering, ökar svansen på ormen och ökar hastighet.
        public void Increase(int score) {
            Increase(score, 0);
        }

        public void Increase(int score, int quantity) {
            this.score += score;
            game.InvokeScoreChanged();
            body.AddPart(quantity);
        }
        public void IncreaseSpeed() {
            moveTrigger.IncreaseSpeed();
        }
        #endregion

        #region Kollisionshantering.
        public void Collide(Collider c) {
            c.Collide(this, head.position);
        }
        public void Collide(ICollidable collidable) {
            collidable.Collide(this);
        }

        public void Collide(Snake opponent) {
            // Tittar om det sker en kollision mellan ormens huvud och motståndarens huvud.
            if (head.position.Equals(opponent.head.position) && opponent.head.direction.Equals(Inverse(head.direction))) {
                Collide(this); opponent.Collide(opponent);
            } else {
                Died();
                Settings.Effect(opponent, 5);
            }
        }

        private void Died() {
            game.Remove(this);
            game.RemoveAnimtable(this, score, id);
            game.Remove(body);
        }

        private Direction Inverse(Direction direction) {
            switch (direction) {
                case Direction.Up: return Direction.Down;
                case Direction.Right: return Direction.Left;
                case Direction.Down: return Direction.Up;
                case Direction.Left: return Direction.Right;
            }
            throw new IndexOutOfRangeException();
        }
        #endregion

        public void Tick() {
            moveTrigger.Tick();
        }
    }
}
