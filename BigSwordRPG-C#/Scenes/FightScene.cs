using BigSwordRPG.Game;
using BigSwordRPG.Utils;

namespace BigSwordRPG.Assets
{
    public class FightScene : Scene
    {
        private Dictionary<string, Hero>? heroesInCombat = null;
        public FightScene() { }

        public bool Initialize(Dictionary<string, Hero> heroes, List<Ennemy> ennemies)
        {
            int count = 0;
            foreach (var key in heroes.Keys)
            {
                if (heroes[key].Health == 0) { ++count; }
            }
            if (count == heroes.Count) { return false; }

            heroesInCombat = heroes;

            return true;
        }

        public override void Update()
        {
            Console.WriteLine("FIGHT !!!");

        }

        private void Round(Hero actHero)
        {
            Console.Clear();
            Console.WriteLine($"Au tour de {actHero.Name} ! \n");
            // S'il n'a pas d'abilité selectionné, prends la première
            int indexAbility = 0;
            ConsoleKey pressedKey;

            do
            {
                foreach (var ability in actHero.Abilities)
                {
                    bool isSelected = ability == actHero.Abilities[indexAbility];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{ability}");
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && indexAbility + 1 < actHero.Abilities.Count)
                    indexAbility++;

                else if (pressedKey == ConsoleKey.UpArrow && indexAbility - 1 >= 0)
                    indexAbility--;

            } while (pressedKey != ConsoleKey.Enter);


        }

        private static void ChangeLineColor(bool shouldHighlight)
        {
            Console.BackgroundColor = shouldHighlight ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = shouldHighlight ? ConsoleColor.Black : ConsoleColor.White;
        }

        private void Round(Ennemy actEnnemy)
        {
            Console.WriteLine($"{actEnnemy.Name} utilise :");
            switch (GameManager.Instance.Difficulty)
            {
                case Difficulties.EASY:
                    RandomAction(actEnnemy);
                    break;
                case Difficulties.MIDDLE:
                    Console.WriteLine();
                    break;
                case Difficulties.HARD:
                    Console.WriteLine();
                    break;
            }
        }

        private void RandomAction(Ennemy actEnnemy)
        {
            var rand = new Random();

            actEnnemy.UseRandomAbilities();

            List<string> _heroesNames = new List<string>();
            foreach (var heroes in heroesInCombat.Values) { _heroesNames.Add(heroes.Name); }

            int nameIndex = 0;
            
            if(heroesInCombat.Count != 1) { nameIndex = rand.Next(heroesInCombat.Count); }

            heroesInCombat[_heroesNames[nameIndex]].TakeDammage(actEnnemy.Damage);
        }


    }
}