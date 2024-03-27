using BigSwordRPG_C_.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ResizeWindow()
        {
            test = new SelectMenu();
            testMenu = new MenuScene();
        }

        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader("../../../Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srFullscreen = new StreamReader("../../../Asset/Image/fullscreen.txt");
            string Fullscreen = srFullscreen.ReadToEnd();

            StreamReader srQuatreTier = new StreamReader("../../../Asset/Image/4 tiers.txt");
            string QuatreTier = srQuatreTier.ReadToEnd();

            StreamReader srTroisDemi = new StreamReader("../../../Asset/Image/3demi.txt");
            string TroisDemi = srTroisDemi.ReadToEnd();

            StreamReader srReturn = new StreamReader("../../../Asset/Image/Return.txt");
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

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public void ReturnMenu()
        {
            testMenu.Draw();
        }

        public void MakeFullscreen()
        {
            Console.SetWindowPosition(Console.WindowLeft, Console.WindowTop);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Draw();
        }

        public void MakeQuatreTier()
        {
            Console.SetWindowSize(100, 35);
            Draw();
        }

        public void MakeTroisDemi()
        {
            Console.SetWindowSize(90, 28);
            Draw();
        }
    }
}
