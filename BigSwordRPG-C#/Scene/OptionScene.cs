﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BigSwordRPG_C_;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG.Assets
{
    public class OptionScene : Scene
    {
        private SelectMenu test;
        private MenuScene testMenu;

        public OptionScene() 
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

            StreamReader srResolution = new StreamReader("../../../Asset/Image/resolution.txt");
            string Resolution = srResolution.ReadToEnd();

            StreamReader srLangues = new StreamReader("../../../Asset/Image/langues.txt");
            string Langues = srLangues.ReadToEnd();

            StreamReader srReturn = new StreamReader("../../../Asset/Image/Return.txt");
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
                        
            HandleUserInput(optionMenu);

            srName.Dispose();
            srResolution.Dispose();
            srLangues.Dispose();
        }

        public void HandleUserInput(List<SelectMenu> options)
        {
            int selectedIndex = 0;
            bool Boucle = true;

            while (Boucle)
            {
                // Redessine le menu avec la nouvelle sélection
                test.LoadAndDisplayMenu(options, selectedIndex);

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex + 1) % options.Count;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    options[selectedIndex].ToDo();
                    Boucle = false;
                }
            }
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public void AllResolution()
        {
            throw new NotSupportedException();
        }

        public void SetLangues()
        {
            throw new NotImplementedException();
        }

        public void ReturnMenu()
        {
            testMenu.Draw();
        }
    }
}
