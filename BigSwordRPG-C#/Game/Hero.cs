using BigSwordRPG_C_;
using BigSwordRPG_C_.Game;
using System.Diagnostics;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        List<Abilities> actAbilities;
        int coolDownCount;
        int magicPoint;

        public Hero() : base()
        {
        }

        public List<Abilities> ActAbilities { get => actAbilities; set => actAbilities = value; }
        public int CoolDownCount { get => coolDownCount; set => coolDownCount = value; }
        public int MagicPoint { get => magicPoint; set => magicPoint = value; }

        public void UseAbilities(int index)
        {
            int healthDiff = MaxHealth - Health;
            if (healthDiff < (int)ActAbilities[index].Heal)
                Health += healthDiff;
            else
                Health += (int)actAbilities[index].Heal;
            MagicPoint++;
        }

        public void UseAbilities(int index, List<Ennemy> ennemies)
        {
            int ennemyIndex;
            switch (ActAbilities[index].Zone)
            {
                case ZoneAction.All:
                    foreach (var enemy in ennemies)
                    { enemy.TakeDammage((int)ActAbilities[index].Damage); }
                    break;
                case ZoneAction.Near:
                    ennemyIndex = SelectEnnemy(ennemies);
                    int finalIndex = ennemies.Count - 1;
                    if (ennemyIndex == 0)
                    {
                        ennemies[ennemyIndex].TakeDammage((int)ActAbilities[index].Damage);
                        ennemies[ennemyIndex + 1].TakeDammage((int)ActAbilities[index].Damage);
                    }
                    else if (ennemyIndex == finalIndex)
                    {
                        ennemies[ennemyIndex - 1].TakeDammage((int)ActAbilities[index].Damage);
                        ennemies[ennemyIndex].TakeDammage((int)ActAbilities[index].Damage);
                    }
                    else
                    {
                        ennemies[ennemyIndex - 1].TakeDammage((int)ActAbilities[index].Damage);
                        ennemies[ennemyIndex].TakeDammage((int)ActAbilities[index].Damage);
                        ennemies[ennemyIndex + 1].TakeDammage((int)ActAbilities[index].Damage);
                    }
                    break;
                default:
                    ennemyIndex = SelectEnnemy(ennemies);
                    ennemies[ennemyIndex].TakeDammage((int)ActAbilities[index].Damage);
                    break;
            }
            MagicPoint++;
        }

        private int SelectEnnemy(List<Ennemy> ennemies)
        {
            bool canBeSelected = false;
            int previousLineIndex = -1, selectedLineIndex = 0;

            ConsoleKey pressedKey;
            do
            {
                if (previousLineIndex != selectedLineIndex)
                {
                    UpdateMenu(ennemies, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.Enter && ennemies[selectedLineIndex].Health > 0)
                    canBeSelected = true;
                else if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < ennemies.Count)
                    selectedLineIndex++;
                else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                    selectedLineIndex--;

            } while (canBeSelected != true);

            // Retoune la clé séléctionnée
            return selectedLineIndex;
        }
        static void UpdateMenu(List<Ennemy> ennemies, int index)
        {
            Console.Clear();
            Console.WriteLine("Qui doit subir la sentence ? \n");
            foreach (var ennemy in ennemies)
            {
                bool isSelected = ennemy == ennemies[index];
                if (isSelected)
                    DrawSelectedMenu(ennemy);
                else
                    Console.WriteLine($"  {ennemy.Name} \u001b[38;5;40mHP: {ennemy.Health}/{ennemy.MaxHealth}\u001b[38;5;15m \n");
            }
        }

        static void DrawSelectedMenu(Ennemy ennemy)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {ennemy.Name} | HP: {ennemy.Health}/{ennemy.MaxHealth} \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseAbilities(int indexAbilities, Dictionary<string, Game.Hero> heroes, string buffType)
        {
            string heroIndex = SelectHero(heroes.Values.ToList());
            if (buffType == "dammage")
                heroes[heroIndex].AttMultiplier *= ActAbilities[indexAbilities].Damage;
            else if (buffType == "heal")
                heroes[heroIndex].HealMultiplier *= ActAbilities[indexAbilities].Heal;
            else
                heroes[heroIndex].Speed += actAbilities[indexAbilities].SpeedUp;
            CoolDownCount = actAbilities[indexAbilities].Cooldown + 1;
            MagicPoint++;
        }

        public string SelectHero(List<Hero> heroes)
        {
            bool canBeSelected = false;
            int previousLineIndex = -1, selectedLineIndex = 0;

            ConsoleKey pressedKey;
            // Selection de la clé du héro
            do
            {
                if (previousLineIndex != selectedLineIndex)
                {
                    UpdateMenu(heroes, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.Enter && heroes[selectedLineIndex].Health > 0)
                    canBeSelected = true;
                else if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < heroes.Count)
                    selectedLineIndex++;
                else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                    selectedLineIndex--;

            } while (canBeSelected != true);

            // Retoune la clé séléctionnée
            return heroes[selectedLineIndex].Name;
        }
        static void UpdateMenu(List<Hero> heroes, int index)
        {
            Console.Clear();
            Console.WriteLine("Qui doit-être soutenu ? \n");
            foreach (var hero in heroes)
            {
                bool isSelected = hero == heroes[index];
                if (isSelected)
                {
                    DrawSelectedMenu(hero);
                }
                else
                    Console.WriteLine($"  {hero.Name} \u001b[38;5;11mSpeed: {hero.Speed} \u001b[38;5;40mHealMult: {hero.HealMultiplier} \u001b[38;5;9mAttMult: {hero.AttMultiplier}\u001b[38;5;15m \n");
            }
        }

        static void DrawSelectedMenu(Hero hero)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {hero.Name} | Speed: {hero.Speed} | HealMult: {hero.HealMultiplier} | AttMult: {hero.AttMultiplier} \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseSpecialAbility(List<Ennemy> ennemies)
        {
            Abilities specAbility = CAbilities.ElementAt(0).Value;
            if (specAbility.Zone == ZoneAction.All)
            {
                for (int i = 0; i < ennemies.Count - 1; i++)
                    ennemies[i].TakeDammage((int)specAbility.Damage);
            }
            else
            {
                int ennemyIndex = SelectEnnemy(ennemies);
                ennemies[ennemyIndex].TakeDammage((int)specAbility.Damage);
            }
            MagicPoint = 0;
        }

        public void UseSpecialAbility(List<Hero> heroes)
        {
            Abilities specAbility = CAbilities.ElementAt(0).Value;
            for (int i = 0; i < heroes.Count - 1; i++)
                heroes[i].Heal((int)specAbility.Heal);
            MagicPoint = 0;
        }
    }


    public class CreateHero
    {
        private string name;
        private int health;
        private int maxHealth;
        private int level;
        private float healthMultiplier;
        private float attMultiplier;
        private float healMultiplier;
        private float speed;
        private Dictionary<string, Abilities> abilities;
        private bool isDead;
        private int PM;
        private int PMmax;
        private Dictionary<string, Equipement> equipements;
        private CreateEquipement createEquipement = new CreateEquipement();

        private int coolDownCount;
        private int magicPoint;

        public CreateEquipement CreateEquipement { get => createEquipement; set => createEquipement = value; }

        public Dictionary<string, Hero> CreateDictionaryHero()//Génerer le disctionnaire des Héros
        {

            Dictionary<string, Hero> heroes = new Dictionary<string, Hero>();

            CreateListAbilities createListAbilities = new CreateListAbilities();

#if DEBUG
            const string filePath = "../../../Game/Stat/HerosStat.csv";
#else
                const string filePath = "./Data/Stat/HerosStat.csv";
#endif

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] heroData = sr.ReadLine().Split(',');

                        string stringHealthMultiplier = heroData[4].Replace("\"", "");
                        string stringAttMultiplier = heroData[5].Replace("\"", "");
                        string stringHealMultiplier = heroData[6].Replace("\"", "");
                        string stringSpeed = heroData[6].Replace("\"", "");

                        Hero hero = new Hero()
                        {
                            Name = heroData[0],
                            MaxHealth = int.Parse(heroData[2]),
                            Health = int.Parse(heroData[2]),
                            Level = int.Parse(heroData[3]),
                            HealthMultiplier = float.Parse(stringHealthMultiplier.Replace(".", ",")),

                            AttMultiplier = float.Parse(stringAttMultiplier.Replace(".", ",")),
                            HealMultiplier = float.Parse(stringHealMultiplier.Replace(".", ",")),
                            Speed = float.Parse(stringSpeed.Replace(".", ",")),
                            IsDead = false,
                            PMMax = int.Parse(heroData[8]),
                            PM = int.Parse(heroData[8]),
                            Equipements = new Dictionary<string, Equipement> { },
                        };

                        hero.CAbilities = new Dictionary<string, Abilities>();
                        for (int i = 9; i < heroData.Length - 1; i++)
                        {
                            hero.CAbilities.Add(heroData[i], createListAbilities.AbilitiesList[heroData[i]]);
                        }
                        for (int i = 0; i < hero.CAbilities.Count; i++)
                        {
                            hero.CAbilities.ElementAt(i).Value.Damage = hero.CAbilities.ElementAt(i).Value.Damage * hero.AttMultiplier * hero.Level;
                            hero.CAbilities.ElementAt(i).Value.Heal = hero.CAbilities.ElementAt(i).Value.Heal * hero.HealMultiplier * hero.Level;
                        }
                        equipements = CreateEquipement.CreateDictionaryEquipement();


                        hero.Equipements.Add(equipements["Defibrilateur Nanite"].Name, equipements["Defibrilateur Nanite"]);

                        heroes.Add(hero.Name, hero);
                    }

                    heroes["Nova"].Equipements.Clear();
                    heroes["Lexus"].Equipements.Clear();
                    heroes["Nova"].Equipements.Add(equipements["Gantelets Electro-Plasma"].Name, equipements["Gantelets Electro-Plasma"]);
                    heroes["Lexus"].Equipements.Add(equipements["Dispositif de fission"].Name, equipements["Dispositif de fission"]);

                    return heroes;

                }
            }
            else
            {
                throw new FileNotFoundException("Fichier " + filePath + " introuvable");
            }
        }

        public void AffichageHeros(Dictionary<string, Hero> heroes)
        {
            List<Hero> h = heroes.Values.ToList();

            int previousLineIndex = 0, selectedLineIndex = 1;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                if (previousLineIndex != selectedLineIndex)
                {
                    UpadateAffichageHeros(h, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.DownArrow && selectedLineIndex + 1 < h.Count)
                {
                    selectedLineIndex++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                {
                    selectedLineIndex--;
                }

                if (keyPressed == ConsoleKey.Enter)
                {
                    Console.Clear();
                    AfficherStatHeros(h[selectedLineIndex], heroes);
                    AffichageHeros(heroes);
                    return;
                }

                if (keyPressed == ConsoleKey.Tab)
                {
                    Console.Clear();
                    return;
                }

            } while (true);


        }

        public void AfficherStatHeros(Hero hero, Dictionary<string, Hero> heroes)
        {
            Console.Clear();
            List<Abilities> ability = hero.CAbilities.Values.ToList();


            int previousLineIndex = -1, selectedLineIndex = 0;
            ConsoleKey keyPressed;
            do
            {
                if (previousLineIndex != selectedLineIndex)
                {
                    UpdateAfficherStatHeros(selectedLineIndex, hero);
                    previousLineIndex = selectedLineIndex;
                }

                keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.DownArrow && selectedLineIndex + 1 < ability.Count)
                {
                    selectedLineIndex++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                {
                    selectedLineIndex--;
                }

                if (keyPressed == ConsoleKey.Tab)
                {
                    Console.Clear();
                    AffichageHeros(heroes);
                }
            } while (keyPressed != ConsoleKey.Enter);

            Console.WriteLine($"\u001b[38;5;7m Abilities:");

            Console.WriteLine();
        }

        static void UpadateAffichageHeros(List<Hero> h, int index)
        {
            foreach (Hero hero in h)
            {
                bool isSelected = hero == h[index];
                if (isSelected)
                    DrawHero(h[index]);
                else
                    Console.WriteLine($"{(isSelected ? ">" : " ")}{hero.Name}");
            }
        }
        static void UpdateAfficherStatHeros(int index, Hero hero)
        {
            Console.Clear();
            Console.WriteLine($"Hero: {hero.Name} \n" + $"\u001b[38;5;40m Health: {hero.Health} \n Max health: {hero.MaxHealth} \n Level: {hero.Level} \n" + $"\u001b[38;5;11m Speed: {hero.Speed} \n" + $"\u001b[38;5;12m PM: {hero.PM} \n PM Max \n");

            Console.WriteLine($"\u001b[38;5;7m Equipement :");

            foreach (KeyValuePair<string, Equipement> abilityKvp in hero.Equipements)
            {
                string abilityName = abilityKvp.Key;
                //Equipement ability = abilityKvp.Value;

                Console.WriteLine($"- {abilityName} + \n");
            }

            hero.ActAbilities = hero.CAbilities.Values.ToList();
            foreach (Abilities abi in hero.ActAbilities)
            {
                bool isSelected = abi == hero.ActAbilities[index];
                if (isSelected)
                {
                    DrawStatHero(abi);
                }
                else
                    Console.WriteLine($"{(isSelected ? ">" : " ")}{hero.ActAbilities.ElementAt(index).Name}");
            }
        }
        static void DrawHero(Hero hero)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {hero.Name}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DrawStatHero(Abilities ability)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {ability.Name} | Type: {ability.Type} | Zone: {ability.Zone}");
            Console.WriteLine($"    ATT: {ability.Damage} | HEAL: {ability.Heal} | SP-UP: {ability.SpeedUp} | Cost: {ability.Cost} | Cooldown: {ability.Cooldown}\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}