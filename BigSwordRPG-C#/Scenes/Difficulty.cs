﻿using BigSwordRPG.Utils;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Utils;

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

        public Difficulty()
        {
            string SizeReturn = ReturnSize();
            test = new SelectMenu();
            testMenu = new MenuScene();
            testMap = new MapScene();
        }

        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader("../../../Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srEasy = new StreamReader("../../../Asset/Image/easy.txt");
            Easy = srEasy.ReadToEnd();

            StreamReader srMiddle = new StreamReader("../../../Asset/Image/middle.txt");
            Middle = srMiddle.ReadToEnd();

            StreamReader srHard = new StreamReader("../../../Asset/Image/hard.txt");
            Hard = srHard.ReadToEnd();

            StreamReader srReturn = new StreamReader("../../../Asset/Image/Return.txt");
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

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public void DifficultyEasy()
        {
            DifficultyChoose = Easy;
            GameManager.Instance.SwitchScene<MapScene>();
        }
        public void DifficultyMiddle()
        {
            DifficultyChoose = Middle;
            GameManager.Instance.SwitchScene<MapScene>();
        }
        public void DifficultyHard()
        {
            DifficultyChoose = Hard;
            GameManager.Instance.SwitchScene<MapScene>();
        }

        public void ReturnMenu()
        {
            testMenu.Draw();
        }

        public string ReturnSize()
        {
            StreamReader srSize = new StreamReader("../../../Config/Config.csv");
            string Size = srSize.ReadToEnd().Replace("\r\n", "");
            srSize.Dispose();

            return Size;
        }
    }
}
