using BigSwordRPG.Assets;
using BigSwordRPG.Utils;
using BigSwordRPG_C_.Game;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG_C_.Utils
{
    public class SelectMenu
    {
        public string menuChoix;
        public Action ToDo;
        private Music testMusic;

        public SelectMenu()
        {
            testMusic = new Music();
        }

        public void LoadAndDisplayMenu(List<SelectMenu> options, int selectedIndex = 0)
        {
            Console.SetCursorPosition(0, 8);

            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine(options[i].menuChoix);
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void HandleUserInput(List<SelectMenu> options)
        {
            int selectedIndex = 0;
            bool Boucle = true;
            while (Boucle)
            {
                LoadAndDisplayMenu(options, selectedIndex);

                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
                    testMusic.PlayBeepSelectMenu();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex + 1) % options.Count;
                    testMusic.PlayBeepSelectMenu();
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    testMusic.PlayEnterMenu();
                    options[selectedIndex].ToDo();
                    Boucle = false;
                }
            }
            GameManager.Instance.SwitchScene<MapScene>();
        }
    }
}