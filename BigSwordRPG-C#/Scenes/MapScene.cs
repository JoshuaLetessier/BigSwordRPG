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
using System.Numerics;

namespace BigSwordRPG.Assets
{
    public class MapScene : Scene
    {
        private List<Interest> _interests;
        public List<Interest> Interests { get => _interests; set => _interests = value; }

        public MapScene():base()
        {
            Interest testInterest = new Interest(new int[2] { 140, 60 });
            Interests = new List<Interest>() { testInterest };
            GameObjects.Add(testInterest);
            Draw();
            RegisterAction(
                ConsoleKey.D,
                new Action(
                    () => MovePlayer(1, Axis.HORIZONTAL)
                )
            );
            RegisterAction(
                 ConsoleKey.Q,
                 new Action(
                     () => MovePlayer(-1, Axis.HORIZONTAL)
                 )
             );
            RegisterAction(
                 ConsoleKey.Z,
                 new Action(
                     () => MovePlayer(-1, Axis.VERTICAL)
                 )
             );
            RegisterAction(
                 ConsoleKey.S,
                 new Action(
                     () => MovePlayer(1, Axis.VERTICAL)
                 )
             );
            RegisterAction(
                ConsoleKey.E,
                new Action(
                    () => TryInteracting()
             )
        );
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

            base.Draw();
            GameManager.Instance.Renderer.Camera.ResetCursorPosition();

            GameManager.Instance.Renderer.Camera.SetCameraPosition(GameManager.Instance.Player.Position);
            GameManager.Instance.Player.Draw();
        }

        public override void Run()
        {
            while(true)
            {
                GameManager.Instance.InputManager.WaitForInput();
                GameManager.Instance.Renderer.Camera.SetCameraPosition(GameManager.Instance.Player.Position);
            }
        }

        private void TryInteracting()
        {
            Player player = GameManager.Instance.Player;
            for (int i = 0; i < Interests.Count; i++)
            {
                if (Interests[i].IsColliding(player))
                {
                    Interests[i].Interact();
                }
            }
        }

        public void MovePlayer(int distance, Axis axis)
        {
            Player player = GameManager.Instance.Player;

            int[] newPosition = new int[2] {
                player.Position[0] + distance * (1 - (int)axis),
                player.Position[1] + distance * (int)axis
            };
            if (GameManager.Instance.Renderer.IsInBuffer(newPosition, player.Texture.Size))
            {
                player.Position[0] = newPosition[0];
                player.Position[1] = newPosition[1];
                GameManager.Instance.Renderer.MoveTexture(player.Position, player.Texture, distance, axis);
            }
        }
    }
}