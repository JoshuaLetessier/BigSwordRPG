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
            Program p = new Program();

            p.testmap.Draw();

            //testmap.Draw();
            
        }
    }
}