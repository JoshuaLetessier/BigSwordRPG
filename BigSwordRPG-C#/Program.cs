using BigSwordRPG.Assets;
using BigSwordRPG.Game;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Utils;

namespace BigSwordRPG
{
    internal class Program
    {
        
        //private MenuScene testMenu;
        private CreateHero testCreateHeros;
        private CreateEnnemy createEnnemy;
        private SaveManager saveManager;
        private FightScene fightScene;
        private Player player;
        private Item potionMineur;
        public Potion healPotion;


        public Program()
        {
            GameManager tmpGameManager = GameManager.Instance;
            tmpGameManager = null;
            testCreateHeros = new CreateHero();
            createEnnemy = new CreateEnnemy();
            saveManager = new SaveManager();
            player = new Player();


            Dictionary<string, Hero> heroes = testCreateHeros.CreateDictionaryHero();
            Dictionary<string, Ennemy> ennemies = createEnnemy.CreateDictionaryEnnemies();

            fightScene = new FightScene(player.Heroes, ennemies, player);

            healPotion = new Potion();
        }

        ~Program()
        {
            //throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 50, Console.LargestWindowHeight - 10);

            Program p = new Program();           

            Dictionary<string, Hero> heroes = p.testCreateHeros.CreateDictionaryHero();
            Dictionary<string, Ennemy> ennemies = p.createEnnemy.CreateDictionaryEnnemies();
        }
    }
}

