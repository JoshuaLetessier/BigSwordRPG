using BigSwordRPG_C_.Game;
using BigSwordRPG_C_.Utils;
using BigSwordRPG.Utils;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using BigSwordRPG.Game;

namespace BigSwordRPG.Assets
{
    public class MenuScene : Scene
    {
        private SelectMenu test;
        private OptionScene option;
        public List<SelectMenu> menu;
        private Difficulty difficulty;
        private SaveManager _saveManager;

#if DEBUG
        const string AUDIO_PATH = "../../../Asset/Music/";
        const string TEXTURE_PATH = "../../../Asset/Image/";
#else
        const string AUDIO_PATH = "./Data/Assets/Musics/";
        const string TEXTURE_PATH = "./Data/Assets/Textures/";
#endif
        const string AUDIO_EXTENSION = ".mp3";
        const string TEXTURE_EXTENSION = ".txt";

        private string filePath = $"{AUDIO_PATH}C418{AUDIO_EXTENSION}";

        private SaveManager SaveManager { get => _saveManager; set => _saveManager = value; }

        public MenuScene()
        {
            test = new SelectMenu();
        }


        public override void Draw()
        {
            difficulty = new Difficulty();
            Console.Clear();
            Console.SetCursorPosition(0, 0);


            Task audioTask = Task.Run(() => GameManager.Instance.Music.ImporterMP3(filePath));

            StreamReader srName = new StreamReader($"{TEXTURE_PATH}nameGame{TEXTURE_EXTENSION}");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srNouvellePartie = new StreamReader($"{TEXTURE_PATH}nouvelle{TEXTURE_EXTENSION}");//Remettre le fichier dans Debug pour le déploiement
            string NouvellePartie = srNouvellePartie.ReadToEnd();

            StreamReader srContinuerPartie = new StreamReader($"{TEXTURE_PATH}continuer{TEXTURE_EXTENSION}");//Remettre le fichier dans Debug pour le déploiement
            string ContinuerPartie = srContinuerPartie.ReadToEnd();

            StreamReader srOption = new StreamReader($"{TEXTURE_PATH}option{TEXTURE_EXTENSION}");//Remettre le fichier dans Debug pour le déploiement
            string Option = srOption.ReadToEnd();

            StreamReader srQuitter = new StreamReader($"{TEXTURE_PATH}quitter{TEXTURE_EXTENSION}");//Remettre le fichier dans Debug pour le déploiement
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
            List<Item> item = new List<Item>();
            SaveManager = new SaveManager();
            SaveManager.Load(GameManager.Instance.Player.Heroes, item);

            MapScene mapScene = new MapScene();
            GameManager.Instance.SwitchScene(mapScene);
        }

        public override void Run()
        {
            Draw();
        }
    }
}