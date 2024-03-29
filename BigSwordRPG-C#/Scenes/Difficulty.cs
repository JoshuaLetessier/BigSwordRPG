using BigSwordRPG;
using BigSwordRPG.Core;

using BigSwordRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG.Assets
{
    public class Difficulty : Scene
    {
        private SelectMenu test;
        private MenuScene testMenu;
        private MapScene testMap;

        private string difficultyChoose;
        
        private string Easy;
        private string Middle;
        private string Hard;

        public string DifficultyChoose { get => difficultyChoose; set => difficultyChoose = value; }

#if DEBUG
        const string TEXTURE_PATH = "../../../Asset/Image/";
        const string CONFIG_PATH = "../../../Config/";
#else
        const string TEXTURE_PATH = "./Data/Assets/Textures/";
        const string CONFIG_PATH = "./Data/Config/";
#endif
        const string TEXTURE_EXTENSION = ".txt";
        const string CONFIG_EXTENSION = ".csv";
        public Difficulty()
        {
            string SizeReturn = ReturnSize();
            test = new SelectMenu();
        }
        
        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader($"{TEXTURE_PATH}nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srEasy = new StreamReader($"{TEXTURE_PATH}easy.txt");
            Easy = srEasy.ReadToEnd();

            StreamReader srMiddle = new StreamReader($"{TEXTURE_PATH}middle.txt");
            Middle = srMiddle.ReadToEnd();

            StreamReader srHard = new StreamReader($"{TEXTURE_PATH}hard.txt");
            Hard = srHard.ReadToEnd();

            StreamReader srReturn = new StreamReader($"{TEXTURE_PATH}Return.txt");
            string Return = srReturn.ReadToEnd();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;

            List<SelectMenu> optionMenu = new List<SelectMenu>()
            {
                new SelectMenu { menuChoix = Easy, ToDo =  DifficultyEasy},
                new SelectMenu { menuChoix = Middle, ToDo =  DifficultyMiddle},
                new SelectMenu { menuChoix = Hard, ToDo =  DifficultyHard},
                new SelectMenu { menuChoix = Return, ToDo = ReturnMenu }
            };

            test.HandleUserInput(optionMenu);

            srName.Dispose();
            srEasy.Dispose();
            srMiddle.Dispose();
            srHard.Dispose();
        }



        public void DifficultyEasy()
        {
            DifficultyChoose = Easy;
        }
        public void DifficultyMiddle()
        {
            DifficultyChoose = Middle;
        }
        public void DifficultyHard()
        {
            DifficultyChoose = Hard;
        }

        public void ReturnMenu()
        {
            GameManager.Instance.SwitchScene(PreviousScene);
        }

        public string ReturnSize()
        {
            StreamReader srSize = new StreamReader($"{CONFIG_PATH}Config.csv");
            string Size = srSize.ReadToEnd().Replace("\r\n", "");
            srSize.Dispose();

            return Size;
        }

        public override void Run()
        {
            Draw();
        }
    }
}
