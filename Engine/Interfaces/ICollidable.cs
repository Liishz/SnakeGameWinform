using SnakeGame.SnakeResource;

namespace SnakeGame.Interfaces {
    public interface ICollidable : IDisplayable {
        void Collide(Collider c);
        void Collide(Snake s);
    }

}
