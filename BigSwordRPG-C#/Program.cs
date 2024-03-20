using BigSwordRPG.Assets;
using BigSwordRPG_C_;

namespace BigSwordRPG
{
    internal class Program
    {
        private MapScene testmap;
        private MenuScene testMenu;
        private SelectMenu test;


        public Program()
        {
            testmap = new MapScene();
            testMenu = new MenuScene();
            test = new SelectMenu();
        }

        ~Program()
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 10, Console.LargestWindowHeight - 10);
            Program p = new Program();
            
            //faire list pour menu mais a voir si je doit la faire dans le draw du menu ou pas

            //p.testMenu.Draw();

            List<SelectMenu> menu = new List<SelectMenu>
            {
                new SelectMenu { menuChoix = "startGame", ToDo = p.testmap.Draw },
                new SelectMenu { menuChoix = "Continue", ToDo = p.testmap.Draw}
            };

            p.test.LoadAndDisplayMenu(menu);


            //p.testmap.Draw();
            Console.Read();

        }
    }
}