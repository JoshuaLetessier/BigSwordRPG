using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    public class InputManager
    {
        public InputManager() { }
        ~InputManager() { }
        public int Initialize()
        {
            return 0;
        }

        public int WaitForInput() {
            ConsoleKey keyPressed = Console.ReadKey(true).Key;
            Dictionary<ConsoleKey, List<Action>> ActionsMap = GameManager.Instance.CurrentScene.ActionsMap;
            if (ActionsMap.ContainsKey(keyPressed)) { 
                foreach(var action in ActionsMap[keyPressed]) 
                {
                    action.Invoke();
                }
            }
            return 0; 
        }
    }
}