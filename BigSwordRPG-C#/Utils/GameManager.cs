using BigSwordRPG.Assets;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG_C_;
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
        private static GameManager _instance;
        public Renderer Renderer { get => _renderer; private set => _renderer = value; }
        public InputManager InputManager { get => _inputManager; set => _inputManager = value; }
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
            CurrentScene = new MenuScene();

            StreamReader sr = new StreamReader("./Asset/Image/player.txt");//Remettre le fichier dans Debug pour le déploiement
            string s2 = sr.ReadToEnd();//.Replace("\\e","\x1b");
            string groundLevel;
            string foregroundColor;
            string backgroundColor;

            Texture playerTexture = new Texture();
            playerTexture.Size = new int[2] { 21, 28 };
            playerTexture.PixelsBuffer = new List<Pixel>();

            for (int i = 0; i < s2.Length; i++)
            {
                if (s2[i] == '\\' && s2[i + 3] != 'm')
                {
                    groundLevel = "";
                    foregroundColor = "";
                    backgroundColor = "";

                    groundLevel += s2[i + 3];
                    groundLevel += s2[i + 4];
                    if (groundLevel == "38")
                    {
                        i += 8;
                        while (s2[i] != ';')
                        {
                            foregroundColor += s2[i];
                            i++;
                        }
                        i += 6;
                        while (s2[i] != 'm')
                        {
                            backgroundColor += s2[i];
                            i++;
                        }
                        i++;
                        while (s2[i] == '▄')
                        {
                            playerTexture.PixelsBuffer.Add(new Pixel(int.Parse(foregroundColor), int.Parse(backgroundColor)));
                            i++;
                        }
                        i--;
                    }
                    else
                    {

                        i += 8;
                        while (s2[i] != 'm')
                        {
                            backgroundColor += s2[i];
                            i++;
                        }
                        i++;
                        if (i < s2.Length)
                        {
                            while (s2[i] == ' ')
                            {
                                playerTexture.PixelsBuffer.Add(new Pixel(int.Parse(backgroundColor), int.Parse(backgroundColor)));
                                i++;
                            }
                            i--;

                        }

                    }
                }
            }

            Player = new Player(new int[2] { 150, 60 }, playerTexture);
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