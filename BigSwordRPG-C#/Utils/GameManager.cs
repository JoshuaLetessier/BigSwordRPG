using BigSwordRPG.Game;
using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace BigSwordRPG.Utils
{
    public enum Difficulties
    {
        EASY = 1,
        MIDDLE,
        HARD
    }
    public class GameManager
    {
        private Difficulties _difficulty;
        



        private GameManager() { }
        ~GameManager() { }

        public static GameManager Instance { get { return Instance; } }
        public Difficulties Difficulty { get => _difficulty; private set => _difficulty = value; }

       
    }
}