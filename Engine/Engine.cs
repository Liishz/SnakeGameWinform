using SnakeGame.Interfaces;
using SnakeGame.Foods;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using SnakeGame.SnakeResource;
using System.Drawing;
using System.Linq;
using SnakeGame.Structs;

namespace SnakeGame {

    public enum Direction { Up, Right, Down, Left }
    public class Engine {

        public delegate void ScoreChangedHandler();
        public event ScoreChangedHandler ScoreChanged;

        public delegate void GameOverHandler();
        public event GameOverHandler GameOver;

        List<IDisplayable> displayables = new List<IDisplayable>();
        LinkedList<ICollidable> collidables = new LinkedList<ICollidable>();
        LinkedList<IAnimatable> animatables = new LinkedList<IAnimatable>();

        // Parar ihop en keys hashcode(int), tillhörande orm och riktning(SnakeSettings). 
        Dictionary<int, SnakeSettings> keyHandler = new Dictionary<int, SnakeSettings>();
        // Slumpar fram valfri mattyp.
        Dictionary<int, FoodFactory.FoodType> foodFactory = new Dictionary<int, FoodFactory.FoodType>() {
            { FoodFactory.FoodType.Normal.GetHashCode(), FoodFactory.FoodType.Normal },
            { FoodFactory.FoodType.Valuable.GetHashCode(), FoodFactory.FoodType.Valuable },
            { FoodFactory.FoodType.SpeedUp.GetHashCode(), FoodFactory.FoodType.SpeedUp }
        };

        // Tillgängliga par av keys, givet från gameForm.
        List<List<Keys>> availableKeys;
        List<KeyValuePair<int, int>> deadSnakes = new List<KeyValuePair<int, int>>();

        Random random = new Random();
        Food food;

        Collider collider;

        int width, height;
        int numberOfFoods, numberOfPlayers;
        int maxFood = 5, randomFood;

        int timer;

        public Engine(int width, int height, int timer) {
            this.width = width;
            this.height = height;
            this.timer = timer;

            collider = new Collider(this.width, this.height);
        }

        public LinkedList<IAnimatable> Animatables { get => animatables; }

        public void InvokeScoreChanged() {
            ScoreChanged?.Invoke();
        }

        public void AddKeyboard(List<List<Keys>> k) {
            availableKeys = k;                   
        }

        public void NewGame(int n) {
            int players = numberOfPlayers = n;
            int x, y;

            numberOfFoods = 0;

            deadSnakes.Clear();
            collider.Clear();

            int index = 0, directionIndex = 0, playerId = 0;

            // Placerar ut alla ormar slumpmässigt på spelbordet.
            while (players > 0) {
                x = 20 * random.Next((width / 20));
                y = 20 * random.Next((height / 20));

                if (collider.IsLocationEmpty(x, y)) {
                    var s = new Snake(x, y, width, height, this, timer, ++playerId);
                    
                    foreach(var key in availableKeys[index]) {
                        keyHandler.Add(key.GetHashCode(), new SnakeSettings { snake = s, direction = (Direction)directionIndex });
                        directionIndex++;
                    }
                    Add(s);
                    index++;
                    directionIndex = 0;
                    players--;
                }
            }
        }

        public void Tick() {
            SpawnFood();
            Animate();
            Collide();
        }

        void Collide() {
            collider.Clear();
            var collidableArray = new ICollidable[collidables.Count];
            collidables.CopyTo(collidableArray, 0);

            foreach (var collidable in collidableArray) {
                collidable.Collide(collider);
            }
        }

        public void DecreaseFoodCount() {
            numberOfFoods--;
        }

        private void SpawnFood() {
            int x, y;
            if(random.Next(25) == 0) {
                if (numberOfFoods < maxFood) {
                    x = 20 * random.Next((width / 20));
                    y = 20 * random.Next((height / 20));

                    if (collider.IsLocationEmpty(x, y)) {
                        randomFood = random.Next(foodFactory.Count);
                        food = FoodFactory.CreateFood(foodFactory[randomFood], x, y, this);

                        Add(food);
                        numberOfFoods++;
                    }
                }
            }

        }
        private void Reset() {
            displayables.Clear();
            collidables.Clear();
            keyHandler.Clear();
        }
        public void SetGameOver() {
            Reset();
            GameOver?.Invoke();
        }

        public void Add(Food food) {
            displayables.Add(food);
            collidables.AddFirst(food);
        }

        public void Add(Snake snake) {
            displayables.Add(snake);
            collidables.AddLast(snake);
            animatables.AddFirst(snake);
        }
        public void Add(ICollidable collidable) {
            collidables.AddFirst(collidable);
            displayables.Add(collidable);
        }

        public void Remove(ICollidable collidable) {
            collidables.Remove(collidable);
            displayables.Remove(collidable);
        }

        public void RemoveAnimtable(IAnimatable animatable, int score, int id) {
            deadSnakes.Add(new KeyValuePair<int, int>(id, score));
            
            animatables.Remove(animatable);
            numberOfPlayers--;
            if (numberOfPlayers == 0) {

                for (int i = 0; i < deadSnakes.Count; i++) {
                    for (int j = i + 1; j < deadSnakes.Count; j++) {
                        if (deadSnakes[i].Value < deadSnakes[j].Value) {
                            var x = deadSnakes[j];
                            deadSnakes[j] = deadSnakes[i];
                            deadSnakes[i] = x;
                        }
                    }
                }

                SetGameOver();
            }
        }

        private void Animate() {
            var animatableArray = new IAnimatable[animatables.Count];
            animatables.CopyTo(animatableArray, 0);

            foreach (var a in animatableArray) {
                a.Tick();
            }
        }

        public void Display(IRenderer renderer) {
            foreach (var displayable in displayables) {
                displayable.Display(renderer);
            }
        }
        public void DisplayResult(IRenderer renderer) {
            renderer.DrawResult(deadSnakes);
        }
        public void DisplayScore(IRenderer renderer) {
            foreach (var animatable in animatables) {
                animatable.DisplayScore(renderer);
            }
        }

        public void KeyIsPressed(Keys key) {
            if (keyHandler.ContainsKey(key.GetHashCode())) {
                ChangeSnakeDirection(keyHandler[key.GetHashCode()].snake, keyHandler[key.GetHashCode()].direction);
            }
            
        }
        private void ChangeSnakeDirection(Snake s, Direction d) {
            s.ChangeDirection(d);
        }
    }
}
