
namespace SnakeGame.Interfaces {

    public interface IAnimatable : ICollidable, IDisplayScore {
        void Tick();
    }
}
