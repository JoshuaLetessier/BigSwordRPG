using BigSwordRPG_C_;
using BigSwordRPG_C_.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace BigSwordRPG.Game
{

    enum ennemyType
    {
        Bourrin = 1,
        Peureux,
        Stratege,
        Lache
    }
    public class Ennemy : Character
    {
        private int _type;

        public Ennemy(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, float speed, Dictionary<string, Abilities> abilities, bool isDead, int PM, int PMMAX,Dictionary<string, Equipement> equipements) : base(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, PMMAX, equipements)
        {
        }

        public int Type { get => _type; set => _type = value; }




        public Abilities UseAbilities(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    return RandomAbilitiesEasyMod();
                case 2:
                    switch (Type)
                    {
                        case 1:
                            return bourinCharactherAbilities();
                        case 2:
                            return peureuxCharacterAbilities();
                        case 3:
                            return strategeCharacterAbilities();
                        case 4:
                            return lacheCharacterAbilities();
                    }
                    return null;
                case 3:
                    return null;
            }
            return null;
        }

        public Abilities RandomAbilitiesEasyMod()
        {
            return CAbilities.ElementAt(RandomAbilities(CAbilities)).Value;
        }

        //charactère des ennemis
        private Abilities bourinCharactherAbilities()//attaque ou rate
        {
            return CAbilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(actionType.ATT))).Value;
        }

        private Abilities peureuxCharacterAbilities()
        {
            // toujours avoir un pourcentage de chance de heal supérieur à l'attaque
            Random random = new Random();

            if (random.Next(0, 1) < 0.75f)
            {
                return CAbilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(actionType.HEAL))).Value;
            }
            else
            {
                return CAbilities.ElementAt(RandomAbilities((Dictionary<string, Abilities>)GetAbilitiesByTypes(actionType.ATT).Concat(GetAbilitiesByTypes(actionType.BUFF)))).Value;
            }
        }

        private Abilities strategeCharacterAbilities() // chanher ça 
        {

            /*List<Abilities> abilitiesTempt = GetAbilitiesByTypes(actionType.HEAL).Value;
            int newValueHeal;
            int oldValueHeal = 0;
            int value;

            if ((float)Health / MaxHealth * 100 < 0.75f)
            {

            }
            else
            {

            }*/

            return null;
        }
        private Abilities lacheCharacterAbilities()
        {
            Random random = new();

            if (random.Next(0, 1) < 0.75f)
                return CAbilities.ElementAt(RandomAbilities((Dictionary<string, Abilities>)GetAbilitiesByTypes(actionType.ATT).Concat(GetAbilitiesByTypes(actionType.HEAL)))).Value;
            else
                return CAbilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(actionType.ESCAPED))).Value;
        }

        private Dictionary<string, Abilities> GetAbilitiesByTypes(Enum type)
        {
            Dictionary<string, Abilities> tempAbilities = new Dictionary<string, Abilities>();

            foreach (var abilities in CAbilities.Values)
            {
                if (abilities.Type == type)
                {
                    tempAbilities.TryAdd(abilities.Name, abilities);
                }
            }
            return tempAbilities;
        }
        private int RandomAbilities(Dictionary<string, Abilities> abilities)
        {
            Random random = new Random();

            return random.Next(0, abilities.Count);
        }
    }

    public class CreateEnnemy
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
        private int PMMAX;
        private Dictionary<string , Equipement> equipements;

        public Dictionary<string, Ennemy> CreateDictionaryEnnemies()
        {
            Dictionary<string, Ennemy> ennemies = new Dictionary<string, Ennemy>();

            CreateListAbilities createListAbilities = new CreateListAbilities();

            string filePath = "../../../Game/Stat/EnnemiesStat.csv";

            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    while(!streamReader.EndOfStream)
                    {
                        string[] ennemiesData = streamReader.ReadLine().Split(',');

                        string stringHealthMultiplier = ennemiesData[4].Replace("\"", "");
                        string stringAttMultiplier = ennemiesData[5].Replace("\"", "");
                        string stringHealMultiplier = ennemiesData[6].Replace("\"", "");
                        string stringSpeed = ennemiesData[6].Replace("\"", "");

                        Ennemy ennemy = new Ennemy(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead, PM, PMMAX, equipements)
                        {
                            Name = ennemiesData[0],
                            MaxHealth = int.Parse(ennemiesData[2]),
                            Health = int.Parse(ennemiesData[1]),
                            Level = int.Parse(ennemiesData[3]),
                            HealthMultiplier = float.Parse(stringHealthMultiplier.Replace(".", ",")),
                            AttMultiplier = float.Parse(stringAttMultiplier.Replace(".", ",")),
                            HealMultiplier = float.Parse(stringHealMultiplier.Replace(".", ",")),
                            Speed = float.Parse(stringSpeed.Replace(".", ",")),
                            IsDead = false,
                            PM = int.Parse(ennemiesData[8]),
                            PMMax = int.Parse(ennemiesData[8]),
                        };

                        for (int i = 9; i < ennemiesData.Length-1; i++)
                        {
                            ennemy.CAbilities.Add(ennemiesData[i], createListAbilities.AbilitiesList[ennemiesData[i]]);
                        }
                        ennemies.Add(ennemy.Name, ennemy);
                    }
                    return ennemies;
                }
            }
            else
            {
                throw new FileNotFoundException("Fichier " + filePath + " entrouvable");
            }
                
        }
    }
}