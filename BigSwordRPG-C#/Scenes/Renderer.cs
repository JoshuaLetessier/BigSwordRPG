using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG_C_;
using BigSwordRPG.GameObjects;
using BigSwordRPG_C_.Utils;

namespace BigSwordRPG.Assets
{
    public class Renderer
    {
        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("User32")]
        static extern void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint flags);

        [DllImport("User32")]
        static extern bool ShowScrollBar(IntPtr hWnd, int bar, bool show);

        [DllImport("User32")]
        static extern bool SetWindowLongA(IntPtr hWnd, int longIndex, long newlong);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        private IntPtr _consoleHandle;
        private Camera _camera;
        private Background _background;
        public Camera Camera { get => _camera; set => _camera = value; }
        public Background Background { get => _background; set => _background = value; }
        private IntPtr ConsoleHandle { get => _consoleHandle; set => _consoleHandle = value; }

        public Renderer() { }
        
        public int Initialize()
        {
            ConsoleHandle = GetConsoleWindow();
            SetWindowPos(ConsoleHandle, 0, 0, 0, 0, 0, 0);
            SetWindowPos(ConsoleHandle, 0, 0, 0, 1944, 1055, 0);
            long style = 0x000000L | 0x10000000L | 0x01000000L;
            SetWindowLongA(ConsoleHandle, -16, style);

            IntPtr hConsole = GetStdHandle(-11);
            uint mode;
            GetConsoleMode(hConsole, out mode);
            const uint ENABLE_EXTENDED_FLAGS = 0x0080;
            mode &= ~ENABLE_EXTENDED_FLAGS; // Disable auto-scrolling
            mode |= 0x0008; // Disable newline auto return
            mode |= 0x0004; // Enable Virtual Terminal Processing / ANSI handling
            SetConsoleMode(hConsole, mode);

            Console.CursorVisible = false;
            Camera = new Camera();

            return 0;
        }

        public void ResizeWindow(int[] newResolution)
        {
            if(true) {  //Should check that the resolution isn't too big otherwise Console.SetWindowPosition will crash
                SetWindowPos(ConsoleHandle, 0, 0, 0, 0, 0, 0);
                SetWindowPos(ConsoleHandle, 0, 0, 0, newResolution[0], newResolution[1], 0);
                Camera.ResizeCamera();
            }
        }

        public void DrawTexture(int[] position, Texture texture) {
            string line;
            int foregroundColor;
            int backgroundColor;
            for (int i = 0; i < texture.Size[1]; i++)
            {
                line = "";
                for (int j = 0; j < texture.Size[0]; j++)
                {
                    foregroundColor = texture.PixelsBuffer[i * texture.Size[0] + j].foregroundColor;
                    backgroundColor = texture.PixelsBuffer[i * texture.Size[0] + j].backgroundColor;
                    line += $"\x1b[38;5;{foregroundColor};48;5;{backgroundColor}m▄";
                }
                Console.SetCursorPosition(position[0], position[1] + i);
                Console.Write(line);
            }
            Camera.ResetCursorPosition();
        }

        public void DrawTextureRegion(int[] position, Texture texture, TextureRegion textureRegion)
        {
            string line;
            int foregroundColor;
            int backgroundColor;
           
            for (int i = 0; i < textureRegion.sizeY; i++)
            {
                line = "";
                for (int j = 0; j < textureRegion.sizeX; j++)
                {
                    foregroundColor = texture.PixelsBuffer[(textureRegion.offsetY + i) * texture.Size[0] + (j + textureRegion.offsetX)].foregroundColor;
                    backgroundColor = texture.PixelsBuffer[(textureRegion.offsetY + i) * texture.Size[0] + (j + textureRegion.offsetX)].backgroundColor;
                    line += $"\x1b[38;5;{foregroundColor};48;5;{backgroundColor}m▄";
                }
                Console.SetCursorPosition(position[0], position[1] + i);
                Console.Write(line);
            }
            Camera.ResetCursorPosition();
        }   

        public void MoveTexture(int[] position, Texture texture, int offset, Axis axis)
        {
            DrawTexture(position, texture);
            TextureRegion textureRegion = new TextureRegion();
            if (axis == Axis.HORIZONTAL)
            {
                textureRegion.offsetX = offset > 0 ? position[0] - Background.Position[0] - offset : position[0] - Background.Position[0] + texture.Size[0];
                textureRegion.offsetY = position[1] - Background.Position[1];
                textureRegion.sizeX = offset & -offset;
                textureRegion.sizeY = texture.Size[1];
            } else
            {
                textureRegion.offsetX = position[0] - Background.Position[0];
                textureRegion.offsetY = offset > 0 ? position[1] - Background.Position[1] - offset : position[1] - Background.Position[1] + texture.Size[1];
                textureRegion.sizeX = texture.Size[0];
                textureRegion.sizeY = offset & -offset;
            }
            int[] backgroundRegionPosition = new int[2] { 
                position[0] + ((offset < 0 ? texture.Size[0] : -offset)) * (int)(1-axis),
                position[1] + ((offset < 0 ? texture.Size[1]: -offset)) * (int)axis 
            };
            DrawTextureRegion(backgroundRegionPosition, Background.Texture, textureRegion); // DrawTextureRegion already calls Camera.ResetCursorPosition();
        }

        public void HideGameObject(GameObject gameObjectToHide)
        {
            TextureRegion textureRegion = new TextureRegion();
            textureRegion.offsetX = gameObjectToHide.Position[0]; //+Backgorund pos which is 0
            textureRegion.offsetY = gameObjectToHide.Position[1]; //+Backgorund pos which is 0
            textureRegion.sizeX = gameObjectToHide.Texture.Size[0];
            textureRegion.sizeY = gameObjectToHide.Texture.Size[1];
            DrawTextureRegion(new int[2] { gameObjectToHide.Position[0], gameObjectToHide.Position[1] }, GameManager.Instance.Renderer.Background.Texture, textureRegion);
        }

        public void ResetCursorColors()
        {
            Console.Write("\x1b[0m");
        }

        public bool IsInBuffer(int[] position, int[] size)
        {
            return position[0] >= 0 && position[1] >= 0 && position[0] + size[0] < Console.BufferWidth && position[1] + size[0] < Console.BufferHeight;
        }   
    }
}