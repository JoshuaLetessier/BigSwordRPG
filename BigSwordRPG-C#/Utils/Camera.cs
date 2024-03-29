using BigSwordRPG.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG_C_.Utils
{
    public class Camera
    {
        private int[] _position;
        private int[] _size;
        public int[] Position { get => _position; set => _position = value; }
        public int[] Size { get => _size; set => _size = value; }


        public Camera()
        {
            Position = new int[2] { 0, 0 };
            Size = new int[2] { Console.WindowWidth, Console.WindowHeight };
        }

        public void SetCameraPosition(int[] centerPosition)
        {
            // Should be adjusted based on Window size directly
            if (centerPosition[0] + (Size[0] / 2) > Console.BufferWidth)
            {
                Position[0] = Console.BufferWidth - Size[0];
            }
            else if (centerPosition[0] - (Size[0] / 2) < 0)
            {
                Position[0] = 0;
            }
            else
            {
                Position[0] = centerPosition[0] - (Size[0] / 2);
            }

            if (centerPosition[1] + (Size[1] / 2) >= Console.BufferHeight)
            {
                Position[1] = Console.BufferHeight - Size[1];
            }
            else if (centerPosition[1] - (Size[1] / 2) < 0)
            {
                Position[1] = 0;
            }
            else
            {
                Position[1] = centerPosition[1] - (Size[1] / 2);
            }
            Console.SetWindowPosition(Position[0], Position[1]);
        }

        public void ResetCursorPosition()
        {
            Console.SetCursorPosition(Position[0], Position[1]);
        }

        public void ResizeCamera()
        {
            Size[0] = Console.WindowWidth;
            Size[1] = Console.WindowHeight;
        }
    }
}