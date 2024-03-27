using BigSwordRPG.Assets;
using BigSwordRPG_C_;

using BigSwordRPG.Utils;
using BigSwordRPG.Game;
using BigSwordRPG_C_.Utils;

namespace BigSwordRPG
{
    internal class Program
    {
        
        private MenuScene testMenu;
        private CreateHero testCreateHeros;
        private CreateEnnemy createEnnemy;
        private SaveManager saveManager;

        public Program()
        {
            
            testMenu = new MenuScene();
            testCreateHeros = new CreateHero();
            createEnnemy = new CreateEnnemy();
            saveManager = new SaveManager();
        }

        ~Program()
        {
            //throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 100, Console.LargestWindowHeight - 10);
            //LargestWindowWidth = 240
            //LargestWindowHeight = 63

            Program p = new Program();

            //p.testMenu.Draw();            

            //Console.Read();

           

            Dictionary<string, Hero> heroes = p.testCreateHeros.CreateDictionaryHero();
            Dictionary<string, Ennemy> ennemies = p.createEnnemy.CreateDictionaryEnnemies();
            

            //p.saveManager.Save(heroes);

            /*

                        foreach (KeyValuePair<string, Hero> kvp in heroes)
                        {
                            string heroName = kvp.Key;
                            Hero hero = kvp.Value;

                            Console.WriteLine($"Hero: {heroName}, Health: {hero.Health}");
                            Console.WriteLine("Abilities:");

                            foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
                            {
                                string abilityName = abilityKvp.Key;
                                Abilities ability = abilityKvp.Value;

                                Console.WriteLine($"- {abilityName}");
                            }

                            Console.WriteLine();
                        }

                        foreach (KeyValuePair<string, Ennemy> kvp in ennemies)
                        {
                            string heroName = kvp.Key;
                            Ennemy hero = kvp.Value;

                            Console.WriteLine($"Ennemy: {heroName}, Health: {hero.Health}");
                            Console.WriteLine("Abilities:");

                            foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
                            {
                                string abilityName = abilityKvp.Key;
                                Abilities ability = abilityKvp.Value;

                                Console.WriteLine($"- {abilityName}");
                            }

                            Console.WriteLine();



                        }*/
        }
    }
}


//mettre dans main pour test génération heros et enemies
/*Dictionary<string, Hero> heroes = p.testCreateHeros.CreateDictionaryHero();
Dictionary<string, Ennemy> ennemies = p.createEnnemy.CreateDictionaryEnnemies();



foreach (KeyValuePair<string, Hero> kvp in heroes)
{
    string heroName = kvp.Key;
    Hero hero = kvp.Value;

    Console.WriteLine($"Hero: {heroName}, Health: {hero.Health}");
    Console.WriteLine("Abilities:");

    foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
    {
        string abilityName = abilityKvp.Key;
        Abilities ability = abilityKvp.Value;

        Console.WriteLine($"- {abilityName}");
    }

    Console.WriteLine();
}

foreach (KeyValuePair<string, Ennemy> kvp in ennemies)
{
    string heroName = kvp.Key;
    Ennemy hero = kvp.Value;

    Console.WriteLine($"Ennemy: {heroName}, Health: {hero.Health}");
    Console.WriteLine("Abilities:");

    foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
    {
        string abilityName = abilityKvp.Key;
        Abilities ability = abilityKvp.Value;

        Console.WriteLine($"- {abilityName}");
    }

    Console.WriteLine();
}*/