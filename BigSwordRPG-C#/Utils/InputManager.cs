using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    public class InputManager
    {
        private Dictionary<ConsoleKey, List<Action>> actionsMap;
        public InputManager() { }
        ~InputManager() { }
        public int Initialize()
        {
            actionsMap = new Dictionary<ConsoleKey, List<Action>>();
            return 0;
        }

        public int Update() {
            ConsoleKey keyPressed = Console.ReadKey().Key;
            switch (keyPressed)
            {
                case ConsoleKey.Z:
                    Console.Write(keyPressed);
                    break;

            }


            return 0; 
        }

        public void RegisterAction(ConsoleKey consoleKey, Action action)
        {
            if(actionsMap.ContainsKey(consoleKey) == false) {
                actionsMap.Add(consoleKey, new List<Action>() { action });
                return;
            }
            actionsMap[consoleKey].Add(action);
        }

        public void UnregisterAction(ConsoleKey consoleKey, Action action)
        {
            if (actionsMap.ContainsKey(consoleKey) == false)
            {
                actionsMap.Add(consoleKey, new List<Action>() { action });
                return;
            }
            actionsMap[consoleKey].Add(action);
        }
    }
}