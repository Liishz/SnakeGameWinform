using SnakeGame.Interfaces;
using SnakeGame.SnakeResource;
using SnakeGame.Structs;
using System;

namespace SnakeGame {
    public class Collider {
        ICollidable[,] CollisionMatrix;
        int Width, Height;

        public Collider(int witdh, int height) {
            Width = witdh; Height = height;
            CollisionMatrix = new ICollidable[Width, Height];
            Clear();
        }

        public void Clear() {
            for (var x = 0; x < Width; x++) {
                for (var y = 0; y < Height; y++) {
                    CollisionMatrix[x, y] = null;
                }
            }
        }
        public bool IsLocationEmpty(int x, int y) {
            if (CollisionMatrix[x, y] == null) {
                return true;
            }
            return false;
        }
        public void Collide(ICollidable c, Position position) {
            CollisionMatrix[position.x, position.y] = c;
        }
        // Kollisionshantering vid en kollision mellan två objekt. 
        public void Collide(Snake s, Position position) {
            if (CollisionMatrix[position.x, position.y] != null) {
                s.Collide(CollisionMatrix[position.x, position.y]);
            } else {
                CollisionMatrix[position.x, position.y] = s;
            }
        }
    }
}
