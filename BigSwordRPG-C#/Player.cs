using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG_C_
{
    public enum Axis
    {
        HORIZONTAL = 0,
        VERTICAL,
        AXIS_COUNT
    }


    public class Player
    {
        private int[] _position;
        private Texture _texture;

        public int[] Position { get => _position; set => _position = value; }
        public Texture Texture { get => _texture; set => _texture = value; }

        public int Initialize()
        {
            Position = new int[2] { 10,10 };
            Texture = new Texture();
            Texture.Size = new int[2] { 2, 3 };
            Texture.PixelsBuffer = new List<Pixel>() { 
                new Pixel(160, 40), new Pixel(160, 40), new Pixel(160, 160), new Pixel(160, 160), new Pixel(160, 160), new Pixel(160, 160) 
            };
            GameManager.Instance.Renderer.DrawTexture(Position, Texture);
            GameManager.Instance.InputManager.RegisterAction(
                ConsoleKey.D, 
                new Action(
                    () => Move(1, Axis.HORIZONTAL)
                )
            );
            GameManager.Instance.InputManager.RegisterAction(
                 ConsoleKey.Q,
                 new Action(
                     () => Move(-1, Axis.HORIZONTAL)
                 )
             );
            GameManager.Instance.InputManager.RegisterAction(
                 ConsoleKey.Z,
                 new Action(
                     () => Move(-1, Axis.VERTICAL)
                 )
             );
            GameManager.Instance.InputManager.RegisterAction(
                 ConsoleKey.S,
                 new Action(
                     () => Move(1, Axis.VERTICAL)
                 )
             );
            return 0;
        }

        public void Move(int distance, Axis axis)
        {
            Position[0] += distance * (1 - (int)axis);
            Position[1] += distance * (int)axis;
            Console.SetCursorPosition(0, 0);
            Console.Write("Moving Char");
            GameManager.Instance.Renderer.MoveTextureBlackBackground(Position, Texture, distance, axis);

        }
    }
}