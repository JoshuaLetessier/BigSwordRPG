using BigSwordRPG_C_.Game;
using BigSwordRPG_C_.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG.Assets
{
    public class MenuScene : Scene
    {
        private SelectMenu test;
        private OptionScene option;
        public List<SelectMenu> menu;
        private Difficulty difficulty;
        private Music music;

        private string filePath = "../../../Asset/Music/C418.mp3";

        public MenuScene()
        {
            test = new SelectMenu();
            music = new Music();
        }


        public override void Draw()
        {
            difficulty = new Difficulty();
            Console.Clear();
            Console.SetCursorPosition(0, 0);



            Task audioTask = Task.Run(() => music.ImporterMP3(filePath));

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

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}