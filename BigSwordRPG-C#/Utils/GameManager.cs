using BigSwordRPG.Assets;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    using Difficulty = Difficulties;
    public enum Difficulties
    {
        EASY,
        MEDIUM,
        HARD,
        DIFFICULTIES_COUNT
    }
    public class GameManager
    {
        private Difficulties _difficulty;
        private Renderer _renderer;
        private InputManager _inputManager;
        private Music _music;
        private Scene _currentScene;
        private Player _player;
        private static GameManager _instance;
        public Renderer Renderer { get => _renderer; private set => _renderer = value; }
        public InputManager InputManager { get => _inputManager; set => _inputManager = value; }
        public Music Music { get => _music; set => _music = value; }
        public Difficulties Difficulty { get => _difficulty; set => _difficulty = value; }
        public Scene CurrentScene { get => _currentScene; set => _currentScene = value; }
        public Player Player { get => _player; set => _player = value; }

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                    _instance.Initialize();
                    _instance.Run();
                }
                return _instance;
            }
        }


        private GameManager() { }
        ~GameManager() { }

        // Shouldn't be deleted
        private int Initialize() // Shouldn't be deleted
        {
            Renderer = new Renderer();
            Renderer.Initialize();
            InputManager = new InputManager();
            InputManager.Initialize();
            Music = new Music();
            CurrentScene = new MenuScene();
            Player = new Player(new int[2] { 150, 60 });
            return 0;
        }

        private void Run()
        {
            CurrentScene.Run();
        }

        public void SwitchScene<NewSceneType>() where NewSceneType : Scene, new()
        {
            Scene tempScene = CurrentScene;
            CurrentScene = new NewSceneType();
            CurrentScene.Initialize(tempScene);
            CurrentScene.Run();
        }

        public void SwitchScene(Scene newScene)
        {
            newScene.PreviousScene = CurrentScene;
            CurrentScene = newScene;
            CurrentScene.Run();
        }
    }
}