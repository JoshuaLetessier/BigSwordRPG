using BigSwordRPG_C_;
using System.Text;
using BigSwordRPG_C_.Game;
using System;
using System.Reflection.PortableExecutable;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        List<Abilities> actAbilities;
        int magicPoint;

        public Hero(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, float speed, Dictionary<string, Abilities> abilities, bool isDead, int PM, int PMMax, Dictionary<string, Equipement> equipements, int magicPoint) : base(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, PMMax, equipements)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = health;
            Level = level;
            HealthMultiplier = healthMultiplier;
            AttMultiplier = attMultiplier;
            HealMultiplier = healMultiplier;
            Speed = speed;
            CAbilities = abilities;
            IsDead = isDead;
            this.magicPoint = magicPoint;
        }

        public List<Abilities> ActAbilities { get => actAbilities; set => actAbilities = value; }
        public int MagicPoint { get => magicPoint; set => magicPoint = value; }

        public void UseAbilities(int index)
        {
            int healthDiff = MaxHealth - Health;
            if (healthDiff < (int)actAbilities[index].Heal)
                Health += healthDiff;
            else
                Health += (int)actAbilities[index].Heal;
            //MagicPoint++
        }

        public void UseAbilities(int index, List<Ennemy> ennemies)
        {
            int ennemyIndex;
            switch (actAbilities[index].Zone)
            {
                case ZoneAction.All:
                    foreach (var enemy in ennemies)
                    { enemy.TakeDammage((int)actAbilities[index].Damage); }
                    break;
                case ZoneAction.Near:
                    ennemyIndex = SelectEnnemy(ennemies);
                    int finalIndex = ennemies.Count - 1;
                    if (ennemyIndex == 0)
                    {
                        ennemies[ennemyIndex].TakeDammage((int)actAbilities[index].Damage);
                        ennemies[ennemyIndex + 1].TakeDammage((int)actAbilities[index].Damage);
                    }
                    else if (ennemyIndex == finalIndex)
                    {
                        ennemies[ennemyIndex - 1].TakeDammage((int)actAbilities[index].Damage);
                        ennemies[ennemyIndex].TakeDammage((int)actAbilities[index].Damage);
                    }
                    else
                    {
                        ennemies[ennemyIndex - 1].TakeDammage((int)actAbilities[index].Damage);
                        ennemies[ennemyIndex].TakeDammage((int)actAbilities[index].Damage);
                        ennemies[ennemyIndex + 1].TakeDammage((int)actAbilities[index].Damage);
                    }
                    break;
                default:
                    ennemyIndex = SelectEnnemy(ennemies);
                    ennemies[ennemyIndex].TakeDammage((int)actAbilities[index].Damage);
                    break;
            }
            //MagicPoint++
        }

        private int SelectEnnemy(List<Ennemy> ennemies)
        {
            ConsoleKey pressedKey;
            int previousLineIndex = -1, selectedLineIndex = 0;

            // Selection de la clé du héro
            do
            {
                if (previousLineIndex != selectedLineIndex)
                {
                    UpdateMenu(ennemies, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < ennemies.Count)
                    selectedLineIndex++;
                else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                    selectedLineIndex--;

            } while(pressedKey != ConsoleKey.Enter);

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
                    Console.WriteLine($"  {ennemy.Name} | HP: {ennemy.Health} \n");
            }
        }

        static void DrawSelectedMenu(Ennemy ennemy)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {ennemy.Name} | HP: {ennemy.Health} \n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void UseAbilities(int indexAbilities, Dictionary<string, Game.Hero> heroes, string buffType)
        {
            string heroIndex = SelectHero(heroes.Values.ToList());
            if (buffType == "dammage")
                heroes[heroIndex].AttMultiplier *= actAbilities[indexAbilities].Damage;
            else if (buffType == "heal")
                heroes[heroIndex].HealMultiplier *= actAbilities[indexAbilities].Heal;
            else
                heroes[heroIndex].Speed += actAbilities[indexAbilities].SpeedUp;
            //MagicPoint++
        }

        public string SelectHero(List<Hero> heroes)
        {
            ConsoleKey pressedKey;
            int previousLineIndex = -1, selectedLineIndex = 0;

            // Selection de la clé du héro
            do
            {
                if (previousLineIndex != selectedLineIndex)
                {
                    UpdateMenu(heroes, selectedLineIndex);
                    previousLineIndex = selectedLineIndex;
                }

                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && selectedLineIndex + 1 < heroes.Count)
                    selectedLineIndex++;
                else if (pressedKey == ConsoleKey.UpArrow && selectedLineIndex - 1 >= 0)
                    selectedLineIndex--;

            } while(pressedKey != ConsoleKey.Enter);

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
                    Console.WriteLine($"  {hero.Name} | HP: {hero.Health} | Speed: {hero.Speed} \n");
            }
        }

        static void DrawSelectedMenu(Hero hero)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {hero.Name} | HP: {hero.Health} | Speed: {hero.Speed} \n");
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
            //MagicPoint = 0;
        }

        public void UseSpecialAbility(List<Hero> heroes)
        {
            Abilities specAbility = CAbilities.ElementAt(0).Value;
            for (int i = 0; i < heroes.Count - 1; i++)
                heroes[i].Heal((int)specAbility.Heal);
            //MagicPoint = 0;
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

        private int magicPoint;

        public CreateEquipement CreateEquipement { get => createEquipement; set => createEquipement = value; }

        public  Dictionary<string, Hero> CreateDictionaryHero()//Génerer le disctionnaire des Héros
        {

            Dictionary<string, Hero> heroes = new Dictionary<string, Hero>();

            CreateListAbilities createListAbilities = new CreateListAbilities();

            string filePath = "../../../Game/Stat/HerosStat.csv";

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

                        Hero hero = new Hero(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, PMmax, equipements, magicPoint)
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
                            Equipements = new Dictionary<string, Equipement> { }
                        };

                        hero.CAbilities = new Dictionary<string, Abilities>();
                        for (int i = 9; i < heroData.Length-1; i++)
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

                    heroes["Nova"].Equipements.Add(equipements["Gantelets Electro-Plasma"].Name, equipements["Gantelets Electro-Plasma"]);
                    heroes["Lexus"].Equipements.Add(equipements["Dispositif de fission"].Name, equipements["Dispositif de fission"]);

                    return heroes;

                }
            }
            else
            {
                throw new FileNotFoundException("Fichier " + filePath + " entrouvable");
            }
        }

        public void AffichageStat(Dictionary<string, Hero> heroes)
        {
            foreach (KeyValuePair<string, Hero> kvp in heroes)
            {
                string heroName = kvp.Key;
                Hero hero = kvp.Value;

                Console.WriteLine($"Hero: {heroName}, Health: {hero.Health}");
                Console.WriteLine("Abilities:");

                foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
                {
                    string abilityName = abilityKvp.Key;
                    Abilities ability = abilityKvp.Value;

                    Console.WriteLine($"- {abilityName}");
                }

                Console.WriteLine();
            }
        }
    }
}