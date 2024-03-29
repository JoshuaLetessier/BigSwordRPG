﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BigSwordRPG_C_.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG.Assets
{
    public class OptionScene : Scene
    {
        private SelectMenu test;
        private MenuScene testMenu;
        private ResizeWindow window;

#if DEBUG
        const string TEXTURE_PATH = "../../../Asset/Image/";
#else
        const string TEXTURE_PATH = "./Data/Assets/Textures/";
#endif
        const string TEXTURE_EXTENSION = ".txt";

        public OptionScene() 
        {
            test = new SelectMenu();
            testMenu = new MenuScene();
            window = new ResizeWindow();
        }



        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader($"{TEXTURE_PATH}nameGame{TEXTURE_EXTENSION}");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srResolution = new StreamReader($"{TEXTURE_PATH}resolution{TEXTURE_EXTENSION}");
            string Resolution = srResolution.ReadToEnd();

            StreamReader srLangues = new StreamReader($"{TEXTURE_PATH}langues{TEXTURE_EXTENSION}");
            string Langues = srLangues.ReadToEnd();

            StreamReader srReturn = new StreamReader($"{TEXTURE_PATH}Return{TEXTURE_EXTENSION}");
            string Return = srReturn.ReadToEnd();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;

            List<SelectMenu> optionMenu = new List<SelectMenu>()
            {
                new SelectMenu { menuChoix = Resolution, ToDo = AllResolution },
                new SelectMenu { menuChoix = Langues, ToDo = SetLangues },
                new SelectMenu { menuChoix = Return, ToDo = ReturnMenu }
            };
                        
            test.HandleUserInput(optionMenu);

            srName.Dispose();
            srResolution.Dispose();
            srLangues.Dispose();
            srReturn.Dispose();
        }

        public void AllResolution()
        {
            window.Draw();
        }

        public void SetLangues()
        {
            throw new NotImplementedException();
        }

        public void ReturnMenu()
        {
            testMenu.Draw();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
