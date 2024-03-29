using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Game;

namespace BigSwordRPG.Assets
{
    public class FightScene : Scene
    {
        private Dictionary<string, Game.Hero> heroesInCombat;
        private Dictionary<string, Game.Ennemy> _ennemiesList;
        private Dictionary<string, Equipement> _equipementList;

        private string firstTeamPlay;
        private int allEnnemyDeath;

        Player _player;

        CreateEquipement createEquipement = new CreateEquipement();

        public Dictionary<string, Ennemy> EnnemiesList { get => _ennemiesList; set => _ennemiesList = value; }
        public Player player { get => _player; set => _player = value; }

        public FightScene()
        {

        }

        public FightScene(Dictionary<string, Game.Hero> heroes, Dictionary<string, Game.Ennemy> ennemies, Player player)
        {
            heroesInCombat = heroes;
            int levelTotal = 0;
            for (int i = 0; i < heroesInCombat.Count; i++)
            {
                levelTotal += heroesInCombat.ElementAt(i).Value.Level;
            }
            int moyLevelHeros = levelTotal / heroesInCombat.Count;

            _ennemiesList = ennemies;

            _equipementList = createEquipement.CreateDictionaryEquipement();
            for (int i = 0; i < _ennemiesList.Count; ++i)
            {
                if (moyLevelHeros < 10)
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

            firstTeamPlay = OrderStartFight();
            _player = player;

        } //exption pour remplacer le bool

        public void Update()
        {
            int countHeros = 0;
            int countEnnemy = 0;
            while (player._allHeroDead == false || allEnnemyDeath == EnnemiesList.Count) 
            { 
                switch (firstTeamPlay)
                {
                    case "h":
                        heroesInCombat.ElementAt(countHeros).Value.ManaHeal(10);
                        Round(heroesInCombat.ElementAt(countHeros).Value);
                        if (countHeros >= heroesInCombat.Count - 1) { countHeros = 0; }
                        else { countHeros++; }
                        firstTeamPlay = "e";
                        break;
                    case "e":
                        EnnemiesList.ElementAt(countEnnemy).Value.ManaHeal(10);
                        Round(EnnemiesList.ElementAt(countEnnemy).Value);
                        if (countEnnemy >= EnnemiesList.Count - 1) { countEnnemy = 0; }
                        else { countEnnemy++; }
                        firstTeamPlay = "h";
                        break;
                }
            }
            Console.Clear();
            if (player._allHeroDead)
                Console.WriteLine("Tu est nul");
            else Console.WriteLine("Tu as gagné");
        }

        private string OrderStartFight()
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

        private void Round(Hero actHero)
        {
            actHero.ActAbilities = actHero.CAbilities.Values.ToList();
            // S'il n'a pas d'abilité selectionné, prends la première
            int previousLineIndex = 0, selectedLineIndex = 1;
            bool isSpecialReady = false, canBeUsed = false;
            // Vérifie que la capacité spéciale peut être activée
            if (actHero.MagicPoint >= 4) { previousLineIndex = -1; selectedLineIndex = 0; isSpecialReady = true; }
            // Vérifie que le cooldown de buff est bien diminué
            if (actHero.CoolDownCount > 0) { actHero.CoolDownCount--; }
            
            ConsoleKey pressedKey;
            do
            {
                // Méthode d'affichage pour toutes les capacitées
                if (previousLineIndex != selectedLineIndex)
                {
                    UpdateMenu(actHero, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                pressedKey = Console.ReadKey().Key;

                // Vérifie si la touche est Enter et que le coût en PM est bon et le cooldown des buff
                if (pressedKey == ConsoleKey.Enter && actHero.ActAbilities[selectedLineIndex].Cost <= actHero.PM && (actHero.CoolDownCount == 0 || actHero.ActAbilities[selectedLineIndex].Cooldown == 0))
                    canBeUsed = true;
                // Vérifie si la touche est DownArrow
                else if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < actHero.ActAbilities.Count)
                    selectedLineIndex++;
                // Vérifie si la touche est UpArrow
                else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 1 || actHero.ActAbilities[selectedLineIndex - 1].Type == actionType.CAPA && isSpecialReady == true)
                    selectedLineIndex--;

            } while (canBeUsed != true);

            // Vérifie le type de l'action et l'effectue
            switch (actHero.ActAbilities[selectedLineIndex].Type)
            {
                case actionType.ATT:
                    actHero.UseAbilities(selectedLineIndex, EnnemiesList.Values.ToList());
                    actHero.UseMana(actHero.ActAbilities[selectedLineIndex].Cost);
                    break;
                case actionType.BUFF:
                    string buffType;
                    if (actHero.ActAbilities[selectedLineIndex].Damage != 0)
                        buffType = "dammage";
                    else if (actHero.ActAbilities[selectedLineIndex].Heal != 0)
                        buffType = "heal";
                    else
                        buffType = "speed";
                    actHero.UseAbilities(selectedLineIndex, heroesInCombat, buffType);
                    actHero.UseMana(actHero.ActAbilities[selectedLineIndex].Cost);
                    break;
                case actionType.HEAL:
                    actHero.UseAbilities(selectedLineIndex);
                    actHero.UseMana(actHero.ActAbilities[selectedLineIndex].Cost);
                    break;
                case actionType.CAPA:
                    if (actHero.ActAbilities[selectedLineIndex].Damage != 0)
                        actHero.UseSpecialAbility(EnnemiesList.Values.ToList());
                    else
                        actHero.UseSpecialAbility(heroesInCombat.Values.ToList());
                    actHero.UseMana(actHero.ActAbilities[selectedLineIndex].Cost);
                    break;
                case actionType.ESCAPED:
                    //Escape();
                    break;
                default:
                    break;
            }
        }
        static void UpdateMenu(Hero actHero, int index)
        {
            Console.Clear();
            Console.WriteLine($"A toi de jouer {actHero.Name} !");
            Console.WriteLine($"  Stats ->\u001b[38;5;40m HP: {actHero.Health}/{actHero.MaxHealth} \u001b[38;5;12m PM: {actHero.PM}/{actHero.PMMax} " +
                $"\u001b[38;5;11m Vitesse: {actHero.Speed} \u001b[38;5;13m MP: {actHero.MagicPoint}\u001b[38;5;15m \n");
            if (actHero.CoolDownCount > 0)
                Console.WriteLine($"Tours avant Buff disponible: {actHero.CoolDownCount}");
            foreach (var ability in actHero.ActAbilities)
            {
                bool isSelected = ability == actHero.ActAbilities[index];
                if (isSelected)
                {
                    DrawSelectedMenu(ability);
                }
                else
                    Console.WriteLine($"  {ability.Name} \u001b[38;5;210mType: {ability.Type}\u001b[38;5;15m \n");
            }
        }

        static void DrawSelectedMenu(Abilities ability)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {ability.Name} | Type: {ability.Type} | Zone: {ability.Zone} | Cout PM: {ability.Cost}");
            Console.WriteLine($"    ATT: {ability.Damage} | SOIN: {ability.Heal} | VIT-SUP: {ability.SpeedUp} \n");
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
                    Console.WriteLine($"  {heroesInCombat.ElementAt(nameIndex).Value.Name} -> \u001b[38;5;40mHP: {heroesInCombat.ElementAt(nameIndex).Value.Health}/{heroesInCombat.ElementAt(nameIndex).Value.MaxHealth}\u001b[38;5;15m");
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

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}