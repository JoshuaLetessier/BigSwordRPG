﻿using BigSwordRPG.Utils;
using BigSwordRPG_C_.Utils;

namespace BigSwordRPG.Assets
{
    public class MenuScene : Scene
    {
        private SelectMenu test;
        private OptionScene option;
        public List<SelectMenu> menu;
        private Difficulty difficulty;

        public MenuScene()
        {
            test = new SelectMenu();

        }


        public override void Draw()
        {
            difficulty = new Difficulty();
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader("../../../Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srNouvellePartie = new StreamReader("../../../Asset/Image/nouvelle.txt");//Remettre le fichier dans Debug pour le déploiement
            string NouvellePartie = srNouvellePartie.ReadToEnd();

            StreamReader srContinuerPartie = new StreamReader("../../../Asset/Image/continuer.txt");//Remettre le fichier dans Debug pour le déploiement
            string ContinuerPartie = srContinuerPartie.ReadToEnd();

            StreamReader srOption = new StreamReader("../../../Asset/Image/option.txt");//Remettre le fichier dans Debug pour le déploiement
            string Option = srOption.ReadToEnd();

            StreamReader srQuitter = new StreamReader("../../../Asset/Image/quitter.txt");//Remettre le fichier dans Debug pour le déploiement
            string Quitter = srQuitter.ReadToEnd();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;

            menu = new List<SelectMenu>
            {
                new SelectMenu { menuChoix = NouvellePartie, ToDo = difficulty.Draw },
                new SelectMenu { menuChoix = ContinuerPartie, ToDo = ContinueGame},
                new SelectMenu { menuChoix = Option, ToDo = OptionGame},
                new SelectMenu { menuChoix = Quitter, ToDo = RetrunDesktop}
            };

            test.HandleUserInput(menu);

            srName.Dispose();
            srNouvellePartie.Dispose();
            srContinuerPartie.Dispose();
            srOption.Dispose();
            srQuitter.Dispose();
        }


        public override void Run()
        {
            Draw();
        }

        public void NouvelleGame()
        {

        }

        public void RetrunDesktop()
        {
            Console.Clear();
            Environment.Exit(0);
        }

        public void OptionGame()
        {

            option = new OptionScene();
            option.Draw();
        }

        public void ContinueGame()
        {
            throw new NotImplementedException();
        }
    }
}