using BigSwordRPG.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    public enum Difficulties
    {
        EASY,
        MEDIUM,
        HARD
    }
    public class GameManager
    {
        private Difficulties _difficulty;
        private Renderer _renderer;
        private InputManager _inputManager;
        private bool _isRunning;
        private static GameManager _instance;
        public Renderer Renderer { get => _renderer; private set => _renderer = value; }
        public InputManager InputManager { get => _inputManager; set => _inputManager = value; }
        public Difficulties Difficulty { get => _difficulty; set => _difficulty = value; }

        public static GameManager Instance { 
            get { 
                if( _instance == null )
                {
                    _instance = new GameManager();
                }
                return _instance; 
            }
        }


        private GameManager() { }
        ~GameManager() { }

        public int Initialize() {
            Renderer = new Renderer();
            Renderer.Initialize();
            InputManager = new InputManager();
            InputManager.Initialize();
            return 0;
        }

        public void Run()
        {
            _isRunning = true;
            while(_isRunning)
            {
                InputManager.Update();
                //Renderer.Update();
            }

        }
    }
}