using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.Json.Nodes;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Utils;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG.Utils;
using BigSwordRPG.GameObjects;

namespace BigSwordRPG.Assets
{
    public class MapScene : Scene
    {
        private Camera testCam;
        private Player testPlayer;

        public MapScene()
        {
            testPlayer = new Player(new int[2] { 150, 60 });
            testCam = new Camera("Fullscreen");
        }

        public override void Draw()
        {
           
            Console.SetCursorPosition(0, 0);

            Console.SetBufferSize(854, 184);

            StreamReader sr = new StreamReader("../../../Asset/Image/map.txt");//Remettre le fichier dans Debug pour le déploiement
            //StreamReader sr = new StreamReader("map.txt");//Remettre le fichier dans Debug pour le déploiement
            string s2 = sr.ReadToEnd();//.Replace("\\e","\x1b");
            string groundLevel;
            string foregroundColor;
            string backgroundColor;
            Texture mapTexture = new Texture();
            mapTexture.Size = new int[2] { 854, 184 };
            mapTexture.PixelsBuffer = new List<Pixel>();
            for (int i = 0; i < s2.Length; i++)
            {
                if (s2[i] == '\\' && s2[i + 3] != 'm')
                {
                    groundLevel = "";
                    foregroundColor = "";
                    backgroundColor = "";

                    groundLevel += s2[i+3];
                    groundLevel += s2[i+4];
                    if(groundLevel == "38")
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
                            mapTexture.PixelsBuffer.Add(new Pixel(int.Parse(foregroundColor), int.Parse(backgroundColor)));
                            i++;
                        }
                        i--;
                    } else
                    {

                        i += 8;
                        while (s2[i] != 'm')
                        {
                            backgroundColor += s2[i];
                            i++;
                        }
                        i++;
                        while (s2[i] == ' ')
                        {
                            mapTexture.PixelsBuffer.Add(new Pixel(int.Parse(backgroundColor), int.Parse(backgroundColor)));
                            i++;
                        }
                        i--;
                       
                    }
                }
            }
            Console.Write(mapTexture);
            Console.SetBufferSize(854, 184);
            GameManager.Instance.Renderer.Background = new Background(new int[2] { 0, 0 }, mapTexture);
            GameManager.Instance.Renderer.DrawTexture(new int[2] { 0, 0 }, mapTexture);
            sr.Dispose();

            //testCam.setPositionCamera();
            GameManager.Instance.Player.Draw();
        }

        public override void Run()
        {
            Draw();
            while(true)
            {
                GameManager.Instance.InputManager.WaitForInput();
                testCam.setPositionCamera(GameManager.Instance.Player.Position);
            }
        }
    }
}