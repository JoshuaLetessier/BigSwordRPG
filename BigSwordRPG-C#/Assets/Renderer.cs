using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace BigSwordRPG.Assets
{
    public class Renderer
    {
        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("User32")]
        static extern void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint flags);

        [DllImport("User32")]
        static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);

        [DllImport("User32")]
        static extern bool ShowScrollBar(IntPtr hWnd, int bar, bool show);

        [DllImport("User32")]
        static extern bool SetWindowLongA(IntPtr hWnd, int longIndex, long newlong);

        public Renderer()
        {

        }

        public int Initialize()
        {
            //Console.WindowWidth = 5;
            Console.WriteLine(Console.LargestWindowWidth);
            /*IntPtr ConsoleHandle = GetConsoleWindow();
            SetWindowPos(ConsoleHandle, 0, 0, 0, 0, 0, 0);
            SetWindowPos(ConsoleHandle, 0, 0, 0, 2000, 1080, 0);
            long style = 0x000000L | 0x10000000L | 0x01000000L;
            SetWindowLongA(ConsoleHandle, -16, style);*/

            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            DrawObject(new[] { 10, 50 }, new[] { 6, 14 });
            DrawObject(new[] { 2, 3 }, new[] { 6, 14 });
            DrawObject(new[] { 50, 30 }, new[] { 6, 5 });
            DrawObject(new[] { 150, 20 }, new[] { 9, 3 });
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
    }
}