using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    public class GameManager
    {
        private GameManager() { }
        ~GameManager() { }

        public static GameManager Instance { get { return Instance; } }
    }
}