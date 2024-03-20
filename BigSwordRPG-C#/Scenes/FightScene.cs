using BigSwordRPG.Game;
using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BigSwordRPG.Assets
{
    public class FightScene : Scene
    {
        private Dictionary<string, Game.Hero> heroesInCombat;
        private int indexAbility = 0;
        public FightScene() { }

        public bool Initialize(Dictionary<string, Game.Hero> heroes, List<Game.Ennemy> ennemies)
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

        private void Round(Game.Hero actHero)
        {
            Console.Clear();
            Console.WriteLine($"Au tour de {actHero.Name} ! \n");
            // S'il n'a pas d'abilité selectionné, prends la première
            ConsoleKey pressedKey;

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

        }

        private static void ChangeLineColor(bool shouldHighlight)
        {
            Console.BackgroundColor = shouldHighlight ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = shouldHighlight ? ConsoleColor.Black : ConsoleColor.White;
        }

        private void Round(Game.Ennemy actEnnemy)
        {
            Console.Write(actEnnemy.Name);
            Console.WriteLine(" ");
        }
    }
}