using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using System.IO;
using BigSwordRPG_C_.Game;
using System.Reflection.Metadata;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        List<Abilities> actAbilities;

        public Hero(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, float speed, Dictionary<string, Abilities> abilities, bool isDead, int PM,int PMMax, Dictionary<string, Equipement> equipements) : base(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, PMMax, equipements)
        {
            Name = name;
            Health = health;
            MaxHealth = maxHealth;
            Level = level;
            HealthMultiplier = healthMultiplier;
            AttMultiplier = attMultiplier;
            HealMultiplier = healMultiplier;
            Speed = speed;
            CAbilities = abilities;
            IsDead = isDead;
        }

        public List<Abilities> ActAbilities { get => actAbilities; set => actAbilities = value; }

        public void UseAbilities(int index)
        {
            int healthDiff = MaxHealth - Health;
            if (healthDiff < (int)actAbilities[index].Heal)
                Health += healthDiff;
            else
                Health += (int)actAbilities[index].Heal;
        }

        public void UseAbilities(int index, List<Ennemy> ennemies)
        {
            int ennemyIndex;
            switch ((ZoneAction)actAbilities[index].Zone)
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
        }

        private int SelectEnnemy(List<Ennemy> ennemies)
        {
            ConsoleKey pressedKey;
            int index = 0;

            // Selection de la clé du héro
            do
            {
                foreach (Ennemy ennemy in ennemies)
                {
                    bool isSelected = ennemy == ennemies[index];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{ennemy.Name}");
                }
                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && index + 1 < ennemies.Count)
                {
                    index++;
                }
                else if (pressedKey == ConsoleKey.UpArrow && index - 1 >= 0)
                {
                    index--;
                }
            } while(index < ennemies.Count);

            // Retoune la clé séléctionnée
            return index;
        }

        public void UseAbilities(int indexAbilities, Dictionary<string, Game.Hero> heroes, string buffType)
        {
            List<string> heroesName = heroes.Values.Select(h => h.Name).ToList();
            string heroIndex = SelectHero(heroesName);
            if (buffType == "dammage")
                heroes[heroIndex].AttMultiplier *= actAbilities[indexAbilities].Damage;
            else if (buffType == "heal")
                heroes[heroIndex].HealMultiplier *= actAbilities[indexAbilities].Heal;
            else
                heroes[heroIndex].Speed *= actAbilities[indexAbilities].SpeedUp;

        }

        private string SelectHero(List<string> heroesName)
        {
            ConsoleKey pressedKey;
            int index = 0;

            // Selection de la clé du héro
            do
            {
                foreach (string name in heroesName)
                {
                    bool isSelected = name == heroesName[index];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{name}");
                }
                pressedKey = Console.ReadKey().Key;

                if (pressedKey == ConsoleKey.DownArrow && index + 1 < heroesName.Count)
                {
                    index++;
                }
                else if (pressedKey == ConsoleKey.UpArrow && index - 1 >= 0)
                {
                    index--;
                }
            } while(index < heroesName.Count);

            // Retoune la clé séléctionnée
            return heroesName[index];
        }
        private static void ChangeLineColor(bool shouldHighlight)
        {
            Console.BackgroundColor = shouldHighlight ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = shouldHighlight ? ConsoleColor.Black : ConsoleColor.White;
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

                        Hero hero = new Hero(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, PMmax, equipements)
                        {
                            Name = heroData[0],
                            Health = int.Parse(heroData[1]),
                            MaxHealth = int.Parse(heroData[2]),
                            Level = int.Parse(heroData[3]),
                            HealthMultiplier = float.Parse(stringHealthMultiplier.Replace(".", ",")),

                            AttMultiplier = float.Parse(stringAttMultiplier.Replace(".", ",")),
                            HealMultiplier = float.Parse(stringHealMultiplier.Replace(".", ",")),
                            Speed = float.Parse(stringSpeed.Replace(".", ",")),
                            IsDead = false,
                            PM = int.Parse(heroData[8]),
                            PMMax = int.Parse(heroData[8]),
                        };

                        hero.CAbilities = new Dictionary<string, Abilities>();
                        for (int i = 9; i < heroData.Length-1; i++)
                        {
                            hero.CAbilities.Add(heroData[i], createListAbilities.AbilitiesList[heroData[i]]);
                        }
                        heroes.Add(hero.Name, hero);
                    }
                    return heroes;

                }
            }
            else
            {
                throw new FileNotFoundException("Fichier " + filePath + " entrouvable");
            }
        }
    }
}