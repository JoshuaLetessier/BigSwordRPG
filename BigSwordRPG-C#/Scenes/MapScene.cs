using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.Json.Nodes;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Utils;
using BigSwordRPG.Utils;
using BigSwordRPG.GameObjects;

namespace BigSwordRPG.Assets
{
    public class MapScene : Scene
    {
        private Camera testCam;
        private Player testPlayer;
        private GameManager gameManager;
        private Music music;
        private TextureLoader textureLoader;

        public MapScene()
        {
            testCam = new Camera();
            textureLoader = new TextureLoader();

            GameManager.Instance.InputManager.RegisterAction(
                ConsoleKey.Escape,
                new Action(
                    () => GameManager.Instance.SwitchScene<MenuAccueil>()
                )
            );
        }

        public override void Draw()
        {
            music.CloseMusic();
            Task audioTask = Task.Run(() => music.ImporterMP3(FilePath));

            Console.SetCursorPosition(0, 0);

            Console.SetBufferSize(854, 184);

            StreamReader sr = new StreamReader("./Asset/Image/map.txt");//Remettre le fichier dans Debug pour le déploiement
            //StreamReader sr = new StreamReader("map.txt");//Remettre le fichier dans Debug pour le déploiement
            string s2 = sr.ReadToEnd();//.Replace("\\e","\x1b");

            Texture mapTexture = new Texture();
            mapTexture.Size = new int[2] { 854, 184 };
            mapTexture.PixelsBuffer = new List<Pixel>();

            Texture maMapTexture = textureLoader.getTexture(s2, mapTexture);

            Console.Write(maMapTexture);
            Console.SetBufferSize(854, 184);
            GameManager.Instance.Renderer.Background = new Background(new int[2] { 0, 0 }, maMapTexture);
            GameManager.Instance.Renderer.DrawTexture(new int[2] { 0, 0 }, maMapTexture);
            sr.Dispose();

            //testCam.setPositionCamera();
            GameManager.Instance.Player.Draw();
            testCam.ResetCursorPosition();

            testCam.SetCameraPosition(GameManager.Instance.Player.Position);
            //testCam.setPositionCamera();
            GameManager.Instance.Player.Draw();
        }

        public override void Run()
        {
            Draw();
            while(true)
            {
                GameManager.Instance.InputManager.WaitForInput();
                testCam.SetCameraPosition(GameManager.Instance.Player.Position);
            }
        }
    }
}