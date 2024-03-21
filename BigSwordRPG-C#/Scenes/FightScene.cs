using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using System.Linq;

namespace BigSwordRPG.Assets
{
    public class FightScene : Scene
    {
        private Dictionary<string, Hero> heroesInCombat;
        List<Ennemy> ennemiesInCombat;
        public FightScene(Dictionary<string, Hero> heroes, List<Ennemy> ennemies) 
        {
            heroesInCombat = heroes;
            ennemiesInCombat = ennemies;
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

            do // Bug d'affichage ???
            {
                foreach (BigSwordRPG_C_.Abilities ability in actHero.Abilities)
                {
                    bool isSelected = ability == actHero.Abilities[indexAbility];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{ability}");
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && indexAbility + 1 < actHero.Abilities.Count)
                {
                    indexAbility++;
                }
                else if (pressedKey == ConsoleKey.UpArrow && indexAbility - 1 >= 0)
                {
                    indexAbility--;
                }
            } while (pressedKey != ConsoleKey.Enter);

            actHero.UseAbilities(indexAbility);

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
                    RandomAction(actEnnemy);
                    break;
                case Difficulties.HARD:
                    Action(actEnnemy);
                    break;
                default:
                    break;
            }
        }

        private void RandomAction(Ennemy actEnnemy)
        {
            var rand = new Random();

            actEnnemy.UseRandomAbilities(); // Savoir si c'est une att ou du soins

            List<string> _heroesNames = heroesInCombat.Values.Select(heroes => heroes.Name).ToList();
            int nameIndex = 0;

            if (heroesInCombat.Count != 1) { nameIndex = rand.Next(heroesInCombat.Count); }

            heroesInCombat[_heroesNames[nameIndex]].TakeDammage(actEnnemy.Damage);
        }

        private void Action(Ennemy actEnnemy)
        {
            List<string> _heroesNames = heroesInCombat.Values.Select(heroes => heroes.Name).ToList();
            List<int> _damageCompare = heroesInCombat.Values.Select(heroes => heroes.Damage).ToList();
            List<int> _healthCompare = heroesInCombat.Values.Select(heroes => heroes.Health).ToList();
            
            for (int i = 0; i < heroesInCombat.Count; i++)
            {
                //heroesInCombat[_heroesNames[i]];
            }

        }
    }
}