using BigSwordRPG.Assets;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Utils;
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
        private Scene _currentScene;
        private Player _player;
        private TextureLoader textureLoader;
        private static GameManager _instance;

        public Renderer Renderer { get => _renderer; private set => _renderer = value; }
        public InputManager InputManager { get => _inputManager; set => _inputManager = value; }
        public Difficulties Difficulty { get => _difficulty; set => _difficulty = value; }

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }


        private GameManager() {
            Renderer = new Renderer();
            Renderer.Initialize();
            InputManager = new InputManager();
            InputManager.Initialize();
            CurrentScene = new MenuScene();
            textureLoader = new TextureLoader();

            StreamReader sr = new StreamReader("./Asset/Image/player.txt");//Remettre le fichier dans Debug pour le déploiement
            string s2 = sr.ReadToEnd();//.Replace("\\e","\x1b");
            
            Texture playerTexture = new Texture();
            playerTexture.Size = new int[2] { 21, 28 };
            playerTexture.PixelsBuffer = new List<Pixel>();

            Texture player = textureLoader.getTexture(s2, playerTexture);

            Player = new Player(new int[2] { 150, 60 }, player);
            return 0;
        }
        ~GameManager() { }

        public void Run()
        {
            _isRunning = true;
            while (_isRunning)
            {
                InputManager.Update();
                //Renderer.Update();
            }

        }
    }
}