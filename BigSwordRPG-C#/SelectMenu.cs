namespace BigSwordRPG_C_
{
    public class SelectMenu
    {
        public string menuChoix;
        public Action ToDo;

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
    }
}