using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Reflection.Metadata;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        List<Abilities> actAbilities;

        public Hero(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, int speed, Dictionary<string, Abilities> abilities, bool isDead) :base(name, health,maxHealth,level,healthMultiplier,attMultiplier,healMultiplier,speed, abilities,isDead)
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
            throw new NotImplementedException();
        }

        public void UseAbilities(int indexAbilities, Dictionary<string, Game.Hero> heroes)
        {

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
        private int speed;
        private Dictionary<string, Abilities> abilities;
        private bool isDead;

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

                        Hero hero = new Hero(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead)
                        {
                            Name = heroData[0],
                            Health = int.Parse(heroData[1]),
                            MaxHealth = int.Parse(heroData[2]),
                            Level = int.Parse(heroData[3]),
                            HealthMultiplier = float.Parse(stringHealthMultiplier.Replace(".", ",")),

                            AttMultiplier = float.Parse(stringAttMultiplier.Replace(".", ",")),
                            HealMultiplier = float.Parse(stringHealMultiplier.Replace(".", ",")),
                            Speed = int.Parse(heroData[7]),
                            IsDead = false
                        };

                        hero.CAbilities = new Dictionary<string, Abilities>();
                        for (int i = 8; i < heroData.Length-1; i++)
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