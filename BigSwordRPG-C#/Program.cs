using BigSwordRPG.Assets;
using BigSwordRPG_C_;

using BigSwordRPG.Utils;

namespace BigSwordRPG
{
    internal class Program
    {
        
        private MenuScene testMenu;

        public Program()
        {
            
            testMenu = new MenuScene();
        }

        ~Program()
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 100, Console.LargestWindowHeight - 10);
            //LargestWindowWidth = 240
            //LargestWindowHeight = 63

            Program p = new Program();

            p.testMenu.Draw();            

            Console.Read();

        }
    }
}