using BigSwordRPG.Assets;
using BigSwordRPG_C_;

using BigSwordRPG.Utils;
using BigSwordRPG.Game;

namespace BigSwordRPG
{
    internal class Program
    {
        
        private MenuScene testMenu;
        private CreateHero testCreateHeros;
        private CreateEnnemy createEnnemy;

        public Program()
        {
            
            testMenu = new MenuScene();
            testCreateHeros = new CreateHero();
            createEnnemy = new CreateEnnemy();
        }

        ~Program()
        {
            //throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 50, Console.LargestWindowHeight - 10);
            //LargestWindowWidth = 240
            //LargestWindowHeight = 63

            Program p = new Program();

            p.testMenu.Draw();            

            Console.Read();

            Dictionary<string, Hero> heroes = p.testCreateHeros.CreateDictionaryHero();
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



            }
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