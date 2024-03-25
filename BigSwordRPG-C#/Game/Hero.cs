using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using System.IO;
using BigSwordRPG_C_.Game;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        List<Abilities> actAbilities;

        public Hero(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, int speed, Dictionary<string, Abilities> abilities, bool isDead, int PM, Dictionary<string, Equipement> equipements) : base(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, equipements)
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

        public int UseAbilities(int indexAbilities)
        {
            switch (actAbilities[indexAbilities].Type)
            {
               /* case (int)actionType.ATT:
                    return actAbilities[indexAbilities].Damage;//ad
                case (int)actionType.HEAL:
                    break;
                case (int)actionType.CAPA:
                    break;*/
                default:
                    break;
            }
            return 0;

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
        private int PM;
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

                        Hero hero = new Hero(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, equipements)
                        {
                            Name = heroData[0],
                            Health = int.Parse(heroData[1]),
                            MaxHealth = int.Parse(heroData[2]),
                            Level = int.Parse(heroData[3]),
                            HealthMultiplier = float.Parse(stringHealthMultiplier.Replace(".", ",")),

                            AttMultiplier = float.Parse(stringAttMultiplier.Replace(".", ",")),
                            HealMultiplier = float.Parse(stringHealMultiplier.Replace(".", ",")),
                            Speed = int.Parse(heroData[7]),
                            IsDead = false,
                            PM = int.Parse(heroData[8])
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