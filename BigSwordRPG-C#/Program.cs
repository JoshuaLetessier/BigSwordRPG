using BigSwordRPG.Assets;
using BigSwordRPG;
using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG.Core;

namespace BigSwordRPG
{
    internal class Program
    {

        //private MenuScene testMenu;
        private CreateHero testCreateHeros;
        private CreateEnnemy createEnnemy;
        private SaveManager saveManager;

        public Program()
        {
            GameManager tmpGameManager = GameManager.Instance;
            tmpGameManager = null;
            //testCreateHeros = new CreateHero();
            //createEnnemy = new CreateEnnemy();
            //saveManager = new SaveManager();
        }

        ~Program()
        {
            //throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            //Console.SetWindowSize(Console.LargestWindowWidth - 50, Console.LargestWindowHeight - 10);

            Program p = new Program();

            //Dictionary<string, Hero> heroes = p.testCreateHeros.CreateDictionaryHero();
            //Dictionary<string, Ennemy> ennemies = p.createEnnemy.CreateDictionaryEnnemies();
        }
    }
}
