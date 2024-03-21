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
        List<Game.Ennemy> _ennemiesList;
        private int indexAbility = 0;

        private bool startFight = false;
        private string firstTeamPlay;
        private int heroPlayable;
        private int ennemyPlaybale;
        private int allEnnemyDeath;

        Player player = new Player(); //!!!!!!!!!!A changer !!!!!!!!!

        public List<Ennemy> EnnemiesList { get => _ennemiesList; set => _ennemiesList = value; }

        public FightScene(Dictionary<string, Game.Hero> heroes, List<Game.Ennemy> ennemies) 
        {
            int count = 0;
            foreach (var key in heroes.Keys)
            {
                if (heroes[key].Health == 0) { ++count; }
            }
            if (count == heroes.Count) { /*return false;*/ }

            heroesInCombat = heroes;
            firstTeamPlay = orderStartFight();
        } //exption pour remplacer le bool

        public override void Update()
        {
            Console.WriteLine("FIGHT !!!");
            // boucle de combas
            while(player.allHeroDead == false || allEnnemyDeath == EnnemiesList.Count)
            {
                if (startFight == true)
                {
                    startFight = false;

                    if (firstTeamPlay == "h")
                    {
                        Round(heroesInCombat.First().Value);
                        firstTeamPlay = "e";
                        if (heroesInCombat.Count != 1)
                        {
                            heroPlayable = 0;
                        }
                        heroPlayable += 1;

                    }
                    else
                    {
                        Round(EnnemiesList[0]);
                        firstTeamPlay= "h";
                        if (EnnemiesList.Count == 1)
                        {
                            ennemyPlaybale = 0;
                        }
                        ennemyPlaybale += 1;
                    }
                }
                else
                {
                    if(firstTeamPlay == "h")
                    {
                        Round(heroesInCombat.First().Value);
                        firstTeamPlay = "e";
                        if (heroesInCombat.Count != 1)
                        {
                            heroPlayable = 0;
                        }
                        heroPlayable += 1;

                    }
                    else
                    {
                        Round(EnnemiesList[0]);
                        firstTeamPlay = "h";
                        if (EnnemiesList.Count == 1)
                        {
                            ennemyPlaybale = 0;
                        }
                        ennemyPlaybale += 1;
                    }
                    //dico toList
                }
            }
        }

        private string orderStartFight()
        {
            if (heroesInCombat.Count < EnnemiesList.Count)
            {
                return "h";
            }
            else if (heroesInCombat.Count > EnnemiesList.Count)
            {
                return "e";
            }
            else
            {
                Random random = new Random();
                float randomFirstPlay = random.Next(0, 1);
                if (randomFirstPlay < 0.5f)
                {
                    return "h";
                }
                else
                {
                    return "e";
                }
            }
        }

        public void FightLoop(string firstPlay)
        {  
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