using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Ennemy(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, int speed, List<Abilities> abilities, bool isDead) : base(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead)
        {
        }

        public int Type { get => _type; set => _type = value; }




      /*  public Abilities UseAbilities(int difficulty)
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
            return Abilities.ElementAt(RandomAbilities(Abilities));
        }

        //charactère des ennemis
        private Abilities bourinCharactherAbilities()//attaque ou rate
        {
            return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(1)));
        }

        private Abilities peureuxCharacterAbilities()
        {
            // toujours avoir un pourcentage de chance de heal supérieur à l'attaque
            Random random = new Random();

            if(random.Next(0, 1) < 0.75f)
            {   
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(2)));
            }
            else
            {
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(1).Concat(GetAbilitiesByTypes(3)).ToList()));
            }
        }

        private Abilities strategeCharacterAbilities() // chanher ça 
        {

            List<Abilities> abilitiesTempt = GetAbilitiesByTypes(2);
            int newValueHeal;
            int oldValueHeal = 0;
            int value;

            if ((float)Health / MaxHealth * 100 < 0.75f)
            {
                int healtLost = MaxHealth - Health;
                oldValueHeal = healtLost;
                for (int i = 0; i < abilitiesTempt.Count; i++)
                {
                   
                    if (Math.Abs(healtLost - abilitiesTempt[i].Damage) < oldValueHeal)
                    {
                        oldValueHeal = Math.Abs(healtLost - (abilitiesTempt[i].Damage));
                        value = i;
                    }
                }
            }
            else
            {
                
            }

            return null;
        }
        private Abilities lacheCharacterAbilities()
        {
            Random random = new();
            if(random.Next(0, 1) < 0.75f)
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(1).Concat(GetAbilitiesByTypes(3)).ToList()));
            else
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes(4)));
        }

        private List<Abilities> GetAbilitiesByTypes(int type)
        {
            List<Abilities> tempAbilities = new List<Abilities>();

            for (int i = 0; i < Abilities.Count; i++)
            {
                if (Abilities[i].Type == type)
                {
                    tempAbilities.Add(Abilities[i]);
                }
            }
            return tempAbilities;
        }
        private int RandomAbilities(List<Abilities> abilities)
        {
            Random random = new Random();

            return random.Next(0, abilities.Count);
        }*/
    }
}