using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.Json.Nodes;
using System.Numerics;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG.GameObjects;
using BigSwordRPG.Game;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG.Core;

namespace BigSwordRPG.Assets
{
    public class MapScene : Scene
    {
        bool isMapDrawn = false;
        private List<Interest> _interactableInterests;
        private List<Interest> _onCollisionInterests;
        public List<Interest> InteractableInterests { get => _interactableInterests; set => _interactableInterests = value; }
        public List<Interest> OnCollisionInterests { get => _onCollisionInterests; set => _onCollisionInterests = value; }
        private string FilePath = "../../../Asset/Image/robot.mp3";
        public MapScene():base()
        {
            Interest testInterest = new DialogInterest(new int[2] { 140, 60 }, "old_lady");
            Interest testColInterest = new FightInterest(new int[2] { 160, 60 }, "forest_fight");
            InteractableInterests = new List<Interest>() { testInterest };
            OnCollisionInterests = new List<Interest>() { testColInterest };
            GameObjects.Add(testInterest);
            GameObjects.Add(testColInterest);
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
            RegisterAction(
                ConsoleKey.Escape,
                new Action(
                    () => { 
                        isMapDrawn = false;
                        GameManager.Instance.SwitchScene<MenuAccueil>(); }
                )
            );
        }

        public override void Draw()
        {
            GameManager.Instance.Music.CloseMusic();
            Task audioTask = Task.Run(() => GameManager.Instance.Music.ImporterMP3(FilePath));

            Console.SetBufferSize(854, 184);
            GameManager.Instance.Renderer.Background = new Background(new int[2] { 0, 0 }, TexturesLoader.GetTexture("map"));
            GameManager.Instance.Renderer.Background.Draw();
            

            base.Draw();
            GameManager.Instance.Renderer.Camera.ResetCursorPosition();

            GameManager.Instance.Renderer.Camera.SetCameraPosition(GameManager.Instance.Player.Position);
            GameManager.Instance.Player.Draw();
        }

        public override void Run()
        {
            if(isMapDrawn == false)
            {
                Draw();
                isMapDrawn = true;
            }
            while (true)
            {
                GameManager.Instance.InputManager.WaitForInput();
                GameManager.Instance.Renderer.Camera.SetCameraPosition(GameManager.Instance.Player.Position);
            }
        }

        private void TryInteracting()
        {
            Player player = GameManager.Instance.Player;
            for (int i = 0; i < InteractableInterests.Count; i++)
            {
                if (InteractableInterests[i].IsColliding(player))
                {
                    InteractableInterests[i].Interact();
                }
            }
        }

        public void MovePlayer(int distance, Axis axis)
        {
            Player player = GameManager.Instance.Player;
            for (int i = 0; i < OnCollisionInterests.Count; i++)
            {
                if (OnCollisionInterests[i].IsColliding(player))
                {
                    isMapDrawn = false;
                    player.Position[0] -= distance * (1 - (int)axis);
                    player.Position[1] -= distance * (int)axis;
                    OnCollisionInterests[i].Interact<FightScene>();
                }
            }
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