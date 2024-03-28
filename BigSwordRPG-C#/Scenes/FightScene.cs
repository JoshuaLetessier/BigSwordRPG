using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG_C_;
using System.Reflection.Metadata.Ecma335;
using BigSwordRPG_C_.Game;

namespace BigSwordRPG.Assets
{
    public class FightScene : Scene
    {
        private Dictionary<string, Game.Hero> heroesInCombat;
        private Dictionary<string, Game.Ennemy> _ennemiesList;
        private Dictionary<string, Equipement> _equipementList;
        private int indexAbility = 0;

        private bool startFight = false;
        private string firstTeamPlay;
        private int countHeros;
        private int countEnnemy;
        private int allEnnemyDeath;

        Player _player;

        CreateEquipement createEquipement = new CreateEquipement();

        

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
            int levelTotal = 0;
            for(int i = 0; i <heroesInCombat.Count; i++)
            {
                levelTotal += heroesInCombat.ElementAt(i).Value.Level;
            }
            int moyLevelHeros = levelTotal / heroesInCombat.Count;

            _ennemiesList = ennemies;

            _equipementList = createEquipement.CreateDictionaryEquipement();
            for(int i = 0; i < _ennemiesList.Count; ++i)
            {
                if(moyLevelHeros < 10)
                {
                    if (_ennemiesList.ContainsKey("Generatron"))
                    {
                        _ennemiesList.ElementAt(i).Value.Equipements.Clear();
                        _ennemiesList.ElementAt(i).Value.Equipements.Add(_equipementList["Defibrilateur Nanite"].Name, _equipementList["Defibrilateur Nanite"]);
                    }
                    else
                    {
                        Random random = new Random();
                        int value = random.Next(0, 2);
                        _ennemiesList.ElementAt(i).Value.Equipements.Add(_equipementList.ElementAt(value).Value.Name, _equipementList.ElementAt(value).Value);
                    }
                        
                }
                else
                {
                    if (_ennemiesList.ContainsKey("Generatron"))
                        _ennemiesList.ElementAt(i).Value.Equipements.Add(_equipementList["Stimulateur Neuro-Electrique"].Name, _equipementList["Stimulateur Neuro-Electrique"]);
                    else
                    {
                        Random random = new Random();
                        int value = random.Next(5, 7);
                        _ennemiesList.ElementAt(i).Value.Equipements.Add(_equipementList.ElementAt(value).Value.Name, _equipementList.ElementAt(value).Value);
                    }
                        
                }
                
                
               

            }

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
            bool isSpecialReady = false, canBeUsed = false;
            // Vérifie que la capacité peut être activée
            //if (actHero.MagicPoint == 4) { previousLineIndex = -1, selectedLineIndex = 0, isSpecialReady = true; }
            ConsoleKey pressedKey;
            
            do
            {
                if(previousLineIndex != selectedLineIndex)
                {
                    UpdateMenu(actHero, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.Enter && actHero.ActAbilities[selectedLineIndex].Cost < actHero.PM)
                    canBeUsed = true;

                if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < actHero.ActAbilities.Count)
                    selectedLineIndex++;

                else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 1 || actHero.ActAbilities[selectedLineIndex - 1].Type == actionType.CAPA && isSpecialReady == true)
                    selectedLineIndex--;

            } while (pressedKey != ConsoleKey.Enter && canBeUsed != true);

            // Vérifie le type de l'action et l'effectue
            if (actHero.ActAbilities[selectedLineIndex].Type == actionType.ATT)
            {
                actHero.UseAbilities(selectedLineIndex, EnnemiesList.Values.ToList());
            }
            else if (actHero.ActAbilities[selectedLineIndex].Type == actionType.BUFF)
            {
                string buffType;
                if (actHero.ActAbilities[selectedLineIndex].Damage != 0)
                    buffType = "dammage";
                else if (actHero.ActAbilities[selectedLineIndex].Heal != 0)
                    buffType = "heal";
                else
                    buffType = "speed";
                actHero.UseAbilities(selectedLineIndex, heroesInCombat, buffType);
            }
            else if (actHero.ActAbilities[selectedLineIndex].Type == actionType.CAPA)
            {
                if (actHero.ActAbilities[selectedLineIndex].Damage != 0)
                    actHero.UseSpecialAbility(EnnemiesList.Values.ToList());
                else
                    actHero.UseSpecialAbility(heroesInCombat.Values.ToList());
            }
            else if (actHero.ActAbilities[selectedLineIndex].Type == actionType.HEAL && actHero.Health != actHero.MaxHealth)
            {
                actHero.UseAbilities(selectedLineIndex);
            }
            else if (actHero.ActAbilities[selectedLineIndex].Type == actionType.ESCAPED)
            {
                //Escape();
            }
        }
        static void UpdateMenu(Hero actHero, int index)
        {
            Console.Clear();
            Console.WriteLine($"A toi de jouer {actHero.Name} !");
            Console.WriteLine($"  Stats ->\u001b[38;5;40m HP: {actHero.Health}/{actHero.MaxHealth} " +
                $"\u001b[38;5;12m PM: {actHero.PM}/{actHero.PMMax} " +
                $"\u001b[38;5;11m Vitesse: {actHero.Speed} " +
                $"\u001b[38;5;13m MP: WIP\u001b[38;5;15m \n");
            foreach (var ability in actHero.ActAbilities)
            {
                bool isSelected = ability == actHero.ActAbilities[index];
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
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {ability.Name} | Type: {ability.Type} | Zone: {ability.Zone}");
            Console.WriteLine($"    ATT: {ability.Damage} | HEAL: {ability.Heal} | SP-UP: {ability.SpeedUp} \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Round(Ennemy actEnnemy)
        {
            Console.Clear();
            switch (GameManager.Instance.Difficulty)
            {
                case Difficulties.EASY:
                    RandomAction(actEnnemy);
                    break;
                case Difficulties.MEDIUM:
                    Action(actEnnemy);
                    break;
                case Difficulties.HARD:
                    Action(actEnnemy);
                    break;
                default:
                    RandomAction(actEnnemy);
                    break;
            }
        }

        private void RandomAction(Ennemy actEnnemy)
        {
            Console.WriteLine($"Au tour de {actEnnemy.Name} !");
            Console.WriteLine($"  Stats ->\u001b[38;5;40m HP: {actEnnemy.Health}/{actEnnemy.MaxHealth}\u001b[38;5;15m \n");
            Thread.Sleep(2000);
            Random rand = new();
            Abilities useAbility = actEnnemy.RandomAbilitiesEasyMod();
            // Savoir si c'est une att ou du soins
            switch (useAbility.Type)
            {
                case actionType.ATT:
                    int nameIndex = 0;
                    // Si il n'y a pas qu'un héro au combat, randomize
                    if (heroesInCombat.Count != 1) { nameIndex = rand.Next(heroesInCombat.Count); }
                    Console.WriteLine($"{actEnnemy.Name} utilise {useAbility.Name} sur {heroesInCombat.ElementAt(nameIndex).Value.Name} !");
                    heroesInCombat.ElementAt(nameIndex).Value.TakeDammage((int)useAbility.Damage);
                    Console.WriteLine($"  {heroesInCombat.ElementAt(nameIndex).Value.Name} ->\u001b[38;5;40m HP: {heroesInCombat.ElementAt(nameIndex).Value.Health}/{heroesInCombat.ElementAt(nameIndex).Value.MaxHealth}\u001b[38;5;15m");
                    Thread.Sleep(2000);
                    // Si le héro visé est déjà à terre
                    if (heroesInCombat.ElementAt(nameIndex).Value.Health == 0)
                        Console.WriteLine("mais il est déjà à terre, il ne subit donc aucun dégat !");
                    Thread.Sleep(3000);
                    break;
                case actionType.HEAL:
                    actEnnemy.Heal((int)useAbility.Heal);
                    Console.WriteLine($"{actEnnemy.Name} se soigne de {(int)useAbility.Heal} HP");
                    Thread.Sleep(2000);
                    // Si l'ennemi soigné est déjà full vie
                    if (actEnnemy.Health == actEnnemy.MaxHealth)
                        Console.WriteLine("malheur, il est déjà sur pied ! Pour la peine tu auras un bonbon.");
                    Thread.Sleep(3000);
                    break;
                case actionType.BUFF:
                    // Définis le type de buff
                    string buffType;
                    if (useAbility.Damage != 0)
                        buffType = "dammage";
                    else if (useAbility.Heal != 0)
                        buffType = "heal";
                    else
                        buffType = "speed";
                    // Choisi un allié au pif
                    int ennemyIndex = rand.Next(0, EnnemiesList.Count);
                    if (buffType == "dammage")
                        EnnemiesList.ElementAt(ennemyIndex).Value.AttMultiplier *= useAbility.Damage;
                    else if (buffType == "heal")
                        EnnemiesList.ElementAt(ennemyIndex).Value.HealMultiplier *= useAbility.Heal;
                    else
                        EnnemiesList.ElementAt(ennemyIndex).Value.Speed += useAbility.SpeedUp;
                    Console.WriteLine($"{actEnnemy.Name} soutient {EnnemiesList.ElementAt(ennemyIndex).Value.Name}");
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