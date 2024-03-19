using System;
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

    }
}