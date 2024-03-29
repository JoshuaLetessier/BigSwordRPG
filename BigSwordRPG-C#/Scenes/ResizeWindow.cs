using BigSwordRPG.Game;
using BigSwordRPG.Core;
using BigSwordRPG.Utils;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG.Assets
{
    public class ResizeWindow : Scene
    {
        private SelectMenu test;
        private MenuScene testMenu;
        private string sizeWindow;

        private string filePath = "/Config/Config.csv";

        [DllImport("kernel32")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("User32")]
        static extern void SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int width, int height, uint flags);

        IntPtr ConsoleHandle = GetConsoleWindow();

#if DEBUG
        const string TEXTURE_PATH = "../../../Asset/Image/";
#else
        const string TEXTURE_PATH = "./Data/Assets/Textures/";
#endif
        const string TEXTURE_EXTENSION = ".txt";

        public string SizeWindow { get => sizeWindow; set => sizeWindow = value; }

        public ResizeWindow()
        {
            test = new SelectMenu();
            testMenu = new MenuScene();
        }

        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader($"{TEXTURE_PATH}nameGame{TEXTURE_EXTENSION}");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srFullscreen = new StreamReader($"{TEXTURE_PATH}fullscreen{TEXTURE_EXTENSION}");
            string Fullscreen = srFullscreen.ReadToEnd();

            StreamReader srQuatreTier = new StreamReader($"{TEXTURE_PATH}4 tiers{TEXTURE_EXTENSION}");
            string QuatreTier = srQuatreTier.ReadToEnd();

            StreamReader srTroisDemi = new StreamReader($"{TEXTURE_PATH}3demi{TEXTURE_EXTENSION}");
            string TroisDemi = srTroisDemi.ReadToEnd();

            StreamReader srReturn = new StreamReader($"{TEXTURE_PATH}Return{TEXTURE_EXTENSION}");
            string Return = srReturn.ReadToEnd();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;

            List<SelectMenu> optionMenu = new List<SelectMenu>()
            {
                new SelectMenu { menuChoix = Fullscreen, ToDo = MakeFullscreen },
                new SelectMenu { menuChoix = QuatreTier, ToDo = MakeQuatreTier },
                new SelectMenu { menuChoix = TroisDemi, ToDo = MakeTroisDemi },
                new SelectMenu { menuChoix = Return, ToDo = ReturnMenu }
            };

            test.HandleUserInput(optionMenu);

            srName.Dispose();
            srFullscreen.Dispose();
            srQuatreTier.Dispose();
            srTroisDemi.Dispose();
            srReturn.Dispose();
        }

        public void ReturnMenu()
        {
            testMenu.Draw();
        }

        public void MakeFullscreen()
        {
            GameManager.Instance.Renderer.ResizeWindow(new int[2] {1920, 1040});
            //LoadResolution();
            Draw();
        }

        public void MakeQuatreTier()
        {
            GameManager.Instance.Renderer.ResizeWindow(new int[2] { 800, 600 });
            //LoadResolution();
            Draw();
        }

        public void MakeTroisDemi()
        {
            GameManager.Instance.Renderer.ResizeWindow(new int[2] { 720, 480 });
            //LoadResolution();
            Draw();
        }

        public void LoadResolution()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            FileInfo fileInfo = new FileInfo(filePath);
            using (StreamWriter streamWriter = fileInfo.CreateText())
            {
                streamWriter.WriteLine(SizeWindow);
            }
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
