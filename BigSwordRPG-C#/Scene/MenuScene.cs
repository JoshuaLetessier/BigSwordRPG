using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG.Assets
{
    public class MenuScene : Scene
    {
        private SelectMenu test;
        private MapScene testmap;

        public MenuScene() 
        {
            test = new SelectMenu();
            testmap = new MapScene();
        }


        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader("../../../Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd().Replace("\\e", "\x1b");

            StreamReader srNouvellePartie = new StreamReader("../../../Asset/Image/nouvelle.txt");//Remettre le fichier dans Debug pour le déploiement
            string NouvellePartie = srNouvellePartie.ReadToEnd().Replace("\\e", "\x1b");

            StreamReader srContinuerPartie = new StreamReader("../../../Asset/Image/continuer.txt");//Remettre le fichier dans Debug pour le déploiement
            string ContinuerPartie = srContinuerPartie.ReadToEnd().Replace("\\e", "\x1b");

            StreamReader srOption = new StreamReader("../../../Asset/Image/option.txt");//Remettre le fichier dans Debug pour le déploiement
            string Option = srOption.ReadToEnd().Replace("\\e", "\x1b");

            StreamReader srQuitter = new StreamReader("../../../Asset/Image/quitter.txt");//Remettre le fichier dans Debug pour le déploiement
            string Quitter = srQuitter.ReadToEnd().Replace("\\e", "\x1b");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;

            List<SelectMenu> menu = new List<SelectMenu>
            {
                new SelectMenu { menuChoix = NouvellePartie, ToDo = testmap.Draw },
                new SelectMenu { menuChoix = ContinuerPartie, ToDo = ContinueGame},
                new SelectMenu { menuChoix = Option, ToDo = OptionGame},
                new SelectMenu { menuChoix = Quitter, ToDo = RetrunDesktop}
            };

            test.LoadAndDisplayMenu(menu);

            srName.Dispose();
            srNouvellePartie.Dispose();
            srContinuerPartie.Dispose();
            srOption.Dispose();
            srQuitter.Dispose();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public void RetrunDesktop()
        {
            Console.Clear();
            Environment.Exit(0);
        }

        public void OptionGame()
        {
            throw new NotImplementedException();
        }

        public void ContinueGame()
        {
            throw new NotImplementedException();
        }
    }
}