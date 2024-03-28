using BigSwordRPG.Game;
using BigSwordRPG_C_;

namespace BigSwordRPG.Assets
{
    public class FightScene : Scene
    {
        private Dictionary<string, Game.Hero> heroesInCombat;
        private Dictionary<string, Game.Ennemy> _ennemiesList;
        private int indexAbility = 0;

        private bool startFight = false;
        private string firstTeamPlay;
        private int countHeros;
        private int countEnnemy;
        private int allEnnemyDeath;

        Player _player;

        

        public Dictionary<string, Ennemy> EnnemiesList { get => _ennemiesList; set => _ennemiesList = value; }
        public Player player { get => _player; set => _player = value; }

        public FightScene(Dictionary<string, Game.Hero> heroes, Dictionary<string,Game.Ennemy> ennemies, Player player) 
        {
            int count = 0;
            foreach (var key in heroes.Keys)
            {
                if (heroes[key].Health == 0) { ++count; }
            }
            if (count == heroes.Count) { /*return false;*/ }

            heroesInCombat = heroes;
            _ennemiesList = ennemies;
            firstTeamPlay = orderStartFight();
            _player = player;
            countHeros = 0;
            countEnnemy = 0;

        } //exption pour remplacer le bool

        public override void Update()
        {
            Console.WriteLine("FIGHT !!!");
            // boucle de combas 
            //version 1 simplifié
            while(player._allHeroDead == false || allEnnemyDeath == EnnemiesList.Count)
            {
                if (firstTeamPlay == "h")
                {
                    //random
                    Round(heroesInCombat.ElementAt(RandomFonction(heroesInCombat.Count)).Value);
                    firstTeamPlay = "e";  
                }
                else
                {
                    Round(EnnemiesList.ElementAt(RandomFonction(heroesInCombat.Count)).Value);
                    firstTeamPlay = "h";
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
                Random random = new();
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

        private int RandomFonction(int value)
        {
            Random random = new Random();

            return random.Next(0, value);
        }

        private void Round(Hero actHero)
        {
            actHero.ActAbilities = actHero.CAbilities.Values.ToList();
            // S'il n'a pas d'abilité selectionné, prends la première
            int previousLineIndex = 0, selectedLineIndex = 1;
            bool isSepcialReady = false;
            // Vérifie que la capacité peut être activée
            //if (actHero.MagicPoint == 4) { previousLineIndex = -1, selectedLineIndex = 0, isSpecialReady = true; }
            ConsoleKey pressedKey;
            
            do
            {
                if(previousLineIndex != selectedLineIndex)
                {
                    UpdateMenu(actHero.ActAbilities, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < actHero.ActAbilities.Count)
                    selectedLineIndex++;

                else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                {
                    if (actHero.ActAbilities[selectedLineIndex-1].Type == actionType.CAPA && isSepcialReady == true)
                        selectedLineIndex--;
                }

            } while (pressedKey != ConsoleKey.Enter && actHero.ActAbilities[selectedLineIndex].Cost > actHero.PM);

            // Vérifie le type de l'action et l'effectue
            if (actHero.ActAbilities[indexAbility].Type == actionType.ATT)
            {
                actHero.UseAbilities(indexAbility, EnnemiesList.Values.ToList());
            }
            else if (actHero.ActAbilities[indexAbility].Type == actionType.BUFF)
            {
                string buffType;
                if (actHero.ActAbilities[indexAbility].Damage != 0)
                    buffType = "dammage";
                else if (actHero.ActAbilities[indexAbility].Heal != 0)
                    buffType = "heal";
                else
                    buffType = "speed";
                actHero.UseAbilities(indexAbility, heroesInCombat, buffType);
            }
            else if ((actionType)actHero.ActAbilities[indexAbility].Type == actionType.CAPA /*&& actHero.MagicPoints == 4*/)
            {
                //actHero.UseSpecialAbility();
            }
            else if ((actionType)actHero.ActAbilities[indexAbility].Type == actionType.HEAL && actHero.Health != actHero.MaxHealth)
            {
                actHero.UseAbilities(indexAbility);
            }
            else if ((actionType)actHero.ActAbilities[indexAbility].Type == actionType.ESCAPED)
            {
                //Escape();
            }
        }
        static void UpdateMenu(List<Abilities> actAbilities, int index)
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");
            foreach (var ability in actAbilities)
            {
                bool isSelected = ability == actAbilities[index];
                if (isSelected)
                {
                    DrawSelectedMenu(ability);
                }
                else
                    Console.WriteLine($"  {ability.Name} | Type: {ability.Type} | Zone: {ability.Zone} \n");
            }
        }

        static void DrawSelectedMenu(Abilities ability)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {ability.Name} | Type: {ability.Type} | Zone: {ability.Zone}");
            Console.WriteLine($"    ATT: {ability.Damage} | HEAL: {ability.Heal} | SP-UP: {ability.SpeedUp} \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Round(Ennemy actEnnemy)
        {
            Console.Clear(); Console.WriteLine("\x1b[3J");
            RandomAction(actEnnemy);
            /*switch (GameManager.Instance.Difficulty)
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
            }*/
        }

        private void RandomAction(Ennemy actEnnemy)
        {
            Console.WriteLine($"Au tour de {actEnnemy.Name} !");
            Console.WriteLine($"HP: {actEnnemy.Health} \n");
            Thread.Sleep(1000);
            Random rand = new();
            Abilities useAbility = actEnnemy.RandomAbilitiesEasyMod(); // Savoir si c'est une att ou du soins

            switch (useAbility.Type)
            {
                case actionType.ATT:
                    int nameIndex = 0;
                    if (heroesInCombat.Count != 1) { nameIndex = rand.Next(heroesInCombat.Count); }
                    Console.WriteLine($"{actEnnemy.Name} utilise {useAbility.Name} sur {heroesInCombat.ElementAt(nameIndex).Value.Name} !");
                    Thread.Sleep(3000);
                    heroesInCombat.ElementAt(nameIndex).Value.TakeDammage((int)useAbility.Damage);
                    break;
                case actionType.HEAL:
                    actEnnemy.Heal((int)useAbility.Heal);
                    Console.WriteLine($"{actEnnemy.Name} se soigne de {(int)useAbility.Heal}");
                    Thread.Sleep(3000);
                    break;
                case actionType.BUFF:
                    Console.WriteLine($"{actEnnemy.Name} buff quelqu'un");
                    Thread.Sleep(3000);
                    break;
                case actionType.ESCAPED:
                    Console.WriteLine($"{actEnnemy.Name} n'arrive pas à s'échappper ಥ﹏ಥ");
                    Thread.Sleep(3000);
                    break;
            }

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