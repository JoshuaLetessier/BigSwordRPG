using BigSwordRPG.Assets;

namespace BigSwordRPG
{
    internal class Program
    {
        private MapScene testmap;


        public Program()
        {
            testmap = new MapScene();
        }

        ~Program()
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.SetBufferSize(854, 480);
            Console.SetWindowSize(Console.LargestWindowWidth - 10, Console.LargestWindowHeight - 10);
            Program p = new Program();

            p.testmap.Draw();

            //testmap.Draw();
            
        }
    }
}