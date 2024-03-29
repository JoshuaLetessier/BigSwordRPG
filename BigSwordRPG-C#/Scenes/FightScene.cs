using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using System.Linq;
using BigSwordRPG_C_;

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

        Player _player;

        public List<Ennemy> EnnemiesList { get => _ennemiesList; set => _ennemiesList = value; }
        public Player player { get => _player; set => _player = value; }

        public FightScene() { }

        public FightScene(Dictionary<string, Game.Hero> heroes, List<Game.Ennemy> ennemies, Player player) 
        {
            int count = 0;
            foreach (var key in heroes.Keys)
            {
                if (heroes[key].Health == 0) { ++count; }
            }
            if (count == heroes.Count) { /*return false;*/ }

            heroesInCombat = heroes;
            firstTeamPlay = orderStartFight();
            _player = player;

        } //exption pour remplacer le bool

        public override void Run()
        {
            Console.WriteLine("FIGHT !!!");
            // boucle de combas
            while(player._allHeroDead == false || allEnnemyDeath == EnnemiesList.Count)
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
                        else
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
                        else
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
                        else
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
                        else
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

        private void Round(Hero actHero)
        {
            Console.Clear();
            Console.WriteLine($"Au tour de {actHero.Name} ! \n");
            // S'il n'a pas d'abilité selectionné, prends la première
            int indexAbility = 0;
            ConsoleKey pressedKey;

            do // Bug d'affichage ???
            {
              /*  foreach (BigSwordRPG_C_.Abilities ability in actHero.CAbilities)
                {
                    bool isSelected = ability == actHero.CAbilities[indexAbility];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{ability}");
                }*/

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && indexAbility + 1 < actHero.CAbilities.Count)
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
                case Difficulties.MEDIUM:
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

           // actEnnemy.RandomAbilitiesEasyMod();

            List<string> _heroesNames = new List<string>();
            foreach (var heroes in heroesInCombat.Values) { _heroesNames.Add(heroes.Name); }
            actEnnemy.RandomAbilitiesEasyMod(); // Savoir si c'est une att ou du soins

            _heroesNames = heroesInCombat.Values.Select(heroes => heroes.Name).ToList();
            int nameIndex = 0;

            if (heroesInCombat.Count != 1) { nameIndex = rand.Next(heroesInCombat.Count); }

            //heroesInCombat[_heroesNames[nameIndex]].TakeDammage(actEnnemy.Damage);
        }

        private void Action(Ennemy actEnnemy)
        {
            List<string> _heroesNames = heroesInCombat.Values.Select(heroes => heroes.Name).ToList();
            //List<int> _damageCompare = heroesInCombat.Values.Select(heroes => heroes.Damage).ToList();
            List<int> _healthCompare = heroesInCombat.Values.Select(heroes => heroes.Health).ToList();
            
            for (int i = 0; i < heroesInCombat.Count; i++)
            {
                //heroesInCombat[_heroesNames[i]];
            }

        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}