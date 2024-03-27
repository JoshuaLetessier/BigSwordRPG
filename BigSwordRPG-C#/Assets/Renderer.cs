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

namespace BigSwordRPG.Assets
{
    public struct COORD
    {
        public short X;
        public short Y;
    }

    public struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    public struct TextureRegion
    {
        public int offsetX;
        public int offsetY;
        public int sizeX;
        public int sizeY;
    }

    /*public struct CHAR_INFO
    {
      public char Char;
      public short Attributes;
    }*/

    public struct CHAR_INFO
    {
        public ushort UnicodeChar;
        public ushort Attributes;
    }

    public class Renderer
    {
        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32")]
        static extern bool ReadConsoleOutput(IntPtr hWnd, IntPtr copyBuffer, COORD copyBufferSize, COORD copyBufferPosition, SMALL_RECT targetRect); //ReadConsoleOutputCharacter

        [DllImport("User32")]
        static extern void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint flags);

        [DllImport("User32")]
        static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

        [DllImport("User32")]
        static extern bool ShowScrollBar(IntPtr hWnd, int bar, bool show);

        [DllImport("User32")]
        static extern bool SetWindowLongA(IntPtr hWnd, int longIndex, long newlong);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadConsoleOutput(IntPtr hConsoleOutput,
                                      [Out] CHAR_INFO[] lpBuffer,
                                      COORD dwBufferSize,
                                      COORD dwBufferCoord,
                                      ref SMALL_RECT lpReadRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetStdHandle(int nStdHandle);

        private char[][] _consoleBuffer;
        public char[][] ConsoleBuffer { get => _consoleBuffer; set => _consoleBuffer = value; }

        public Texture _backgroundTexture;

        public Renderer()
        {

        }
        
        public int Initialize()
        {
            //Console.WindowWidth = 5;
            IntPtr hConsole = GetStdHandle(-11); // Standard output handle

            Console.WriteLine(Console.LargestWindowWidth);
            IntPtr ConsoleHandle = GetConsoleWindow();
            List<CHAR_INFO> charInfoList = new List<CHAR_INFO>();
            COORD position = new COORD();
            COORD size = new COORD();
            //ReadConsoleOutput(ConsoleHandle, charInfoList, )
            SetWindowPos(ConsoleHandle, 0, 0, 0, 0, 0, 0);
            SetWindowPos(ConsoleHandle, 0, 0, 0, 2600, 3000, 0);
            long style = 0x000000L | 0x10000000L | 0x01000000L;
            SetWindowLongA(ConsoleHandle, -16, style);

            //Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            DrawObject(new[] { 10, 50 }, new[] { 6, 14 });
            DrawObject(new[] { 2, 3 }, new[] { 6, 14 });
            DrawObject(new[] { 50, 30 }, new[] { 6, 5 });
            DrawObject(new[] { 150, 20 }, new[] { 9, 3 });
            Console.SetCursorPosition(15, 15);
            Console.Write("\x1b[48;2;12;4;255mTestCharBG\x1b[48;2;0;0;0m");
            Console.SetCursorPosition(15, 15);
            Console.Write("\x1b[38;2;12;255;100mTestCharBG");

            /*SMALL_RECT readRegion = new SMALL_RECT
            {
                Left = 10,
                Top = 50,
                Right = 16, // Adjust these coordinates to specify the region you want to read
                Bottom = 64
            };*/

            /*COORD bufferSize = new COORD
            {
                X = (short)(readRegion.Right - readRegion.Left + 1),
                Y = (short)(readRegion.Bottom - readRegion.Top + 1)
            };*/

            CHAR_INFO[] buffer = new CHAR_INFO[bufferSize.X * bufferSize.Y];

            //bool success = ReadConsoleOutput(hConsole, buffer, bufferSize, new COORD { X = 0, Y = 0 }, ref readRegion);

            int errorCode = Marshal.GetLastWin32Error();
            Console.WriteLine("Error occurred. Error code: " + errorCode);

            return 0;
        }

        /// <summary>
        /// Redraws the whole visible buffer.
        /// </summary>
        public void RenderFrame()
        {

        }

        public void DrawObject(int[] position, int[] size)
        {
            string line;
            size[0] = Math.Min(size[0], Console.BufferWidth - position[0]);
            size[1] = Math.Min(size[1], Console.BufferHeight - position[1]);

            for (int i = 0; i < size[1]; i++)
            {
                Console.SetCursorPosition(position[0], position[1] + i);
                line = "";
                for (int j = 0; j < size[0]; j++)
                {
                    line += "H";
                }
                Console.Write(line);
            }
        }

        public void DrawTexture(int[] position, Texture texture) {
            //Console.SetCursorPosition(0, 0);
            //Console.Write("Draw Texture");
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
        }

        public void DrawTextureRegion(int[] position, Texture texture, TextureRegion textureRegion)
        {
            string line;
            int foregroundColor;
            int backgroundColor;
            for (int i = textureRegion.offsetY; i < textureRegion.offsetY + textureRegion.sizeY; i++)
            {
                line = "";
                for (int j = textureRegion.offsetX; j < textureRegion.offsetX + textureRegion.sizeX; j++)
                {
                    foregroundColor = texture.PixelsBuffer[(i) * texture.Size[0] + j].foregroundColor;
                    backgroundColor = texture.PixelsBuffer[(i) * texture.Size[0] + j].backgroundColor;
                    line += $"\x1b[38;5;{foregroundColor};48;5;{backgroundColor}m▄";
                }
                Console.SetCursorPosition(position[0], position[1] + i);
                Console.Write(line);
            }
        }   

        public void MoveTextureBlackBackground(int[] position, Texture texture, int offset, Axis axis)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Draw Texture");
            string line;
            int foregroundColor;
            int backgroundColor;
            // bufferCoords = pos1 - pos2
            // 
            for (int i = 0; i < offset * (int)axis; i++)
            {
                line = "";
                for (int k = 0; k < texture.Size[0]; k++)
                {
                    line += $"\x1b[38;5;{0};48;5;{0}m▄";
                }
                Console.SetCursorPosition(position[0], position[1] + i - offset * (int)axis);
                Console.Write(line);
            }
            for (int i = 0; i < texture.Size[1]; i++)
            {
                line = "";
                Console.SetCursorPosition(position[0], position[1] + i);
                for (int k = 0;  k < offset * (1 - (int)axis); k++)
                {
                    Console.CursorLeft -= offset * (1 - (int)axis);
                    line += $"\x1b[38;5;{0};48;5;{0}m▄";
                }
                for (int j = 0; j < texture.Size[0]; j++)
                {
                    foregroundColor = texture.PixelsBuffer[i * texture.Size[0] + j].foregroundColor;
                    backgroundColor = texture.PixelsBuffer[i * texture.Size[0] + j].backgroundColor;
                    line += $"\x1b[38;5;{foregroundColor};48;5;{backgroundColor}m▄";
                }
                for (int k = 0; k > offset * (1 - (int)axis); k--)
                {
                    line += $"\x1b[38;5;{0};48;5;{0}m▄";
                }
                Console.Write(line);
            }
            for (int i = 0; i > offset * (int)axis; i--)
            {
                line = "";
                for (int k = 0; k < texture.Size[0]; k++)
                {
                    line += $"\x1b[38;5;{0};48;5;{0}m▄";
                }
                Console.SetCursorPosition(position[0], position[1] + texture.Size[1] + i);
                Console.Write(line);
            }
            Console.SetCursorPosition(0, 0);
        }

        public void MoveTexture(int[] position, Texture texture, int offset, Axis axis)
        {
            DrawTexture(position, texture);
            GameObject gameObject = new GameObject();
            TextureRegion textureRegion = new TextureRegion();
            if(axis == Axis.HORIZONTAL)
            {
                textureRegion.offsetX = offset > 0 ? position[0] - gameObject.Position[0] : position[0] - gameObject.Position[0] + texture.Size[0];
                textureRegion.offsetY = position[1] - gameObject.Position[1];
                textureRegion.sizeX = offset;
                textureRegion.sizeY = texture.Size[1];
            } else
            {
                textureRegion.offsetX = position[0] - gameObject.Position[0];
                textureRegion.offsetY = offset > 0 ? position[1] - gameObject.Position[1] : position[1] - gameObject.Position[1] + texture.Size[1];
                textureRegion.sizeX = texture.Size[0];
                textureRegion.sizeY = offset;
            }
            
            DrawTextureRegion(position, gameObject.Texture, textureRegion);
        }

    }
}