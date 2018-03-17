using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Foods {
    class FoodFactory {
        public enum FoodType { Normal, SpeedUp, Valuable};
        public static Food CreateFood(FoodType type, int x, int y, Engine game) {
            switch (type) {
                case FoodType.Normal:
                    return new NormalFood(x, y, game);
                case FoodType.SpeedUp:
                    return new SpeedupFood(x, y, game);
                case FoodType.Valuable:
                    return new ValuableFood(x, y, game);
            }
            return default;
        }
    }
}
