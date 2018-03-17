using System;

namespace SnakeGame {
    class Trigger {
        int countDown = 0;
        double baseSpeed, threshold = 250, count, speed; 

        Boolean powerUp;

        // 1 tick 62.5 millisec
        public Trigger(int timer) {
            // 25 (deltaTid) * 2.5 = 62.5 (BasSpeed)
            baseSpeed = timer * 2.5;          

            speed = baseSpeed;

            count = threshold;
            // Ökar hastigheten på ormen.
            powerUp = false;
        }

        public delegate void TriggerEventHandler();
        public event TriggerEventHandler Triggered;

        public void IncreaseSpeed() {
            if (!powerUp) {
                // Halverar tiden, ormen rör sig oftare.
                speed = baseSpeed * 2;
                count = threshold;
                countDown = 0;
                powerUp = true;
            } else {
                countDown -= 40;
            }
        }
        public void Tick() {
            // 125 ms = 5 tick. 250 = 10 tick, = 500 = 20 tick; 
            if (powerUp) {
                countDown++;
                // Den ökade hastigheten varar i 10 sek.
                if (countDown == 40 * 10) {
                    speed = baseSpeed;
                    powerUp = false;
                    countDown = 0;
                }
            }
            // Ormen får röra sig efter antal ticks.
            if (count == 0) {
                count = threshold;
                Triggered?.Invoke();
            }
            count -= speed;
        }

        public void Move(object sender, EventArgs e) {
            Triggered?.Invoke();
        }
    }
}
