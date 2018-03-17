using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsGUI {
    public class AvailableController {
        List<List<Keys>> keys = new List<List<Keys>>();

        Keys[] first = { Keys.Up, Keys.Right, Keys.Down, Keys.Left };
        Keys[] second = { Keys.W, Keys.D, Keys.S, Keys.A };
        Keys[] third = { Keys.T, Keys.H, Keys.G, Keys.F };
        Keys[] fourth = { Keys.I, Keys.L, Keys.K, Keys.J };
        public AvailableController() {
            keys.Add(new List<Keys>(first));
            keys.Add(new List<Keys>(second));
            keys.Add(new List<Keys>(third));
            keys.Add(new List<Keys>(fourth));
        }
        public List<List<Keys>> GetAvailableController() {
            return keys;
        }
    }
}
