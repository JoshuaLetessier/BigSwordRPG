﻿using System.Text;

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
        Potion potion = new();
        CreateEquipement createEquipement = new CreateEquipement();

        public Ennemy()
            : base()
        {
        }

        public int Type { get => _type; set => _type = value; }

        public void IsDeadLoot(int herosLevel, ref Inventory inventory)
        {

            List<Item> tempList = new List<Item>();
            Dictionary<string, Equipement> tempDico = new Dictionary<string, Equipement>();
            Dictionary<string, Equipement> equipements = createEquipement.CreateDictionaryEquipement();
            if (herosLevel < 10)
            {

                for (int i = 0; i < potion.ListItem().Count; i++)
                {
                    if (potion.ListItem()[i].Rarety == 0)
                    {
                        tempList.Add(potion.ListItem()[i]);
                    }
                }

                Random rand = new Random();
                if (rand.Next(0, 10) < 7)
                    inventory.Store(tempList[rand.Next(tempList.Count)]);
                else
                {
                    for (int i = 0; i < equipements.Count; i++)
                    {
                        if (equipements.ElementAt(i).Value.Rarety == 0)
                            tempDico.Add(equipements.ElementAt(i).Value.Name, equipements.ElementAt(i).Value);
                    }

                    inventory.Store(tempDico.ElementAt(rand.Next(tempList.Count)).Value);
                }
            }
            else
            {
                for (int i = 0; i < potion.ListItem().Count; i++)
                {
                    if (potion.ListItem()[i].Rarety == 1)
                    {
                        tempList.Add(potion.ListItem()[i]);
                    }
                }

                Random rand = new Random();
                if (rand.Next(0, 10) < 7)
                    inventory.Store(tempList[rand.Next(tempList.Count)]);
                else
                {
                    for (int i = 0; i < equipements.Count; i++)
                    {
                        if (equipements.ElementAt(i).Value.Rarety == 0)
                            tempDico.Add(equipements.ElementAt(i).Value.Name, equipements.ElementAt(i).Value);
                    }

                    inventory.Store(tempDico.ElementAt(rand.Next(tempList.Count)).Value);
                }
            }

        }

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
            Abilities randomAbility;
            do
            {
                randomAbility = CAbilities.ElementAt(RandomAbilities(CAbilities)).Value;
            } while (randomAbility.Cost > PM);
            return randomAbility;
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

        private Dictionary<string, Abilities> GetAbilitiesByTypes(actionType type)
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
        Dictionary<string, Equipement> equipements;
        private CreateEquipement createEquipement = new CreateEquipement();

        public CreateEquipement CreateEquipement { get => createEquipement; set => createEquipement = value; }

        public Dictionary<string, Ennemy> CreateDictionaryEnnemies()
        {
            Dictionary<string, Ennemy> ennemies = new Dictionary<string, Ennemy>();

            CreateListAbilities createListAbilities = new CreateListAbilities();

#if DEBUG
            const string filePath = "../../../Game/Stat/EnnemiesStat.csv";
#else
                const string filePath = "./Data/Stat/EnnemiesStat.csv";
#endif

            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new StreamReader(filePath, Encoding.UTF8))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] ennemiesData = streamReader.ReadLine().Split(',');

                        string stringHealthMultiplier = ennemiesData[4].Replace("\"", "");
                        string stringAttMultiplier = ennemiesData[5].Replace("\"", "");
                        string stringHealMultiplier = ennemiesData[6].Replace("\"", "");
                        string stringSpeed = ennemiesData[6].Replace("\"", "");

                        Ennemy ennemy = new Ennemy()
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
                            PMMax = int.Parse(ennemiesData[8]),
                            PM = int.Parse(ennemiesData[8]),
                            Equipements = new Dictionary<string, Equipement> { }
                        };

                        for (int i = 9; i < ennemiesData.Length - 1; i++)
                        {
                            ennemy.CAbilities.Add(ennemiesData[i], createListAbilities.AbilitiesList[ennemiesData[i]]);
                        }
                        for (int i = 0; i < ennemy.CAbilities.Count; i++)
                        {
                            ennemy.CAbilities.ElementAt(i).Value.Damage = ennemy.CAbilities.ElementAt(i).Value.Damage * ennemy.AttMultiplier * ennemy.Level;
                            ennemy.CAbilities.ElementAt(i).Value.Heal = ennemy.CAbilities.ElementAt(i).Value.Heal * ennemy.HealMultiplier * ennemy.Level;
                        }
                        equipements = new Dictionary<string, Equipement>();
                        equipements = CreateEquipement.CreateDictionaryEquipement();
                        ennemy.Equipements.Add(equipements["Defibrilateur Nanite"].Name, equipements["Defibrilateur Nanite"]);

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