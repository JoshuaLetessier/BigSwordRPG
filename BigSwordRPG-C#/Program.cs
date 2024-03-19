using BigSwordRPG.Utils;

namespace BigSwordRPG
{
    internal class Program
    {
        public Program()
        {
            throw new NotImplementedException();
        }

        ~Program()
        {
            throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            GameManager.Instance.Initialize();
            GameManager.Instance.Run();
        }
    }
}