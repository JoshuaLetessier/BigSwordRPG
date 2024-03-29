using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG_C_.Utils;
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

            StreamReader srName = new StreamReader("/Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srFullscreen = new StreamReader("/Asset/Image/fullscreen.txt");
            string Fullscreen = srFullscreen.ReadToEnd();

            StreamReader srQuatreTier = new StreamReader("/Asset/Image/4 tiers.txt");
            string QuatreTier = srQuatreTier.ReadToEnd();

            StreamReader srTroisDemi = new StreamReader("/Asset/Image/3demi.txt");
            string TroisDemi = srTroisDemi.ReadToEnd();

            StreamReader srReturn = new StreamReader("/Asset/Image/Return.txt");
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
