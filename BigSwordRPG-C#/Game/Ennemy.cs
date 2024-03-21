using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Ennemy : Character
    {
        public Ennemy(string name, int health, int level, int damage, string type, int speed, List<Abilities> abilities, bool isDead) : base(name, health, level, damage, type, speed, abilities, isDead)
        {
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
                        case "bourrin":
                            return bourinCharactherAbilities();
                        case "peureux":
                            return peureuxCharacterAbilities();
                        case "stratege":
                            return strategeCharacterAbilities();
                        case "lache":
                            return lacheCharacterAbilities();
                    }
                    return 2;
                case 3:
                    return 3;
            }
            return 0;
        }

        private Abilities RandomAbilitiesEasyMod()
        {
            return Abilities.ElementAt(RandomAbilities(Abilities));
        }

        //charactère des ennemis
        private Abilities bourinCharactherAbilities()//attaque ou rate
        {
            return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes("attaquePhysique")));
        }

        private Abilities peureuxCharacterAbilities()
        {
            // toujours avoir un pourcentage de chance de heal supérieur à l'attaque
            Random random = new Random();

            if(random.Next(0, 1) < 0.75f)
            {   
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes("heal")));
            }
            else
            {
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes("attaquePhysique").Concat(GetAbilitiesByTypes("magicAttack")).ToList()));
            }
        }

/*        private Abilities strategeCharacterAbilities()
        {
            //return Abilities;
        }*/
        private Abilities lacheCharacterAbilities()
        {
            Random random = new();
            if(random.Next(0, 1) < 0.75f)
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes("attaquePhysique").Concat(GetAbilitiesByTypes("magicAttack")).ToList()));
            else
                return Abilities.ElementAt(RandomAbilities(GetAbilitiesByTypes("attques")));
        }

        private List<Abilities> GetAbilitiesByTypes(string type)
        {
            List<Abilities> tempAbilities = new List<Abilities>();

            for (int i = 0; i < Abilities.Count; i++)
            {
                if (Abilities[i].Type == "heal")
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
        }
    }
}