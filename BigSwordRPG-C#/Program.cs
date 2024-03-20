using BigSwordRPG.Assets;
using BigSwordRPG_C_;

namespace BigSwordRPG
{
    internal class Program
    {
        private MapScene testmap;
        private MenuScene testMenu;


        public Program()
        {
            testmap = new MapScene();
            testMenu = new MenuScene();
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

            p.testMenu.Draw();

            List<SelectMenu> menu = new List<SelectMenu>
            {
                "Nouvelle Partie"
            };

            //p.testmap.Draw();


        }
    }
}