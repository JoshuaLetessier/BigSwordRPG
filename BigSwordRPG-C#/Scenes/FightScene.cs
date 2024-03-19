using BigSwordRPG.Game;
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
            int index = 0;
            ConsoleKeyInfo keyinfo;

            Console.Clear();
            Console.Write("Au tour de ");
            Console.WriteLine($"{actHero.Name}\n");
            foreach (var abilityName in actHero.Abilities.Keys)
            {
                string selectOptionSymbol = "  ";
                if (abilityName == actHero.Abilities[abilityName])
                {
                    selectOptionSymbol = "> ";
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"{selectOptionSymbol}{actHero.Abilities[abilityName]}");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{abilityName}");
            }

            if (keyinfo.Key == ConsoleKey.DownArrow)
            {
                if (index + 1 < actHero.Abilities.Count)
                {
                    index++;
                }
            }

            else if (keyinfo.Key == ConsoleKey.UpArrow)
            {
                if (index - 1 >= 0)
                {
                    index--;
                }
            }

            else if (keyinfo.Key == ConsoleKey.Enter)
            {
                // there will be another logic in the future here. For now it is irrelevant.
                Console.WriteLine($"{actHero.Abilities[index]} was chosen as an option");
                break;
            }

        }

        private void Round(Game.Ennemy actEnnemy)
        {
            Console.Write(actEnnemy.Name);
            Console.WriteLine(" ");
        }
    }
}