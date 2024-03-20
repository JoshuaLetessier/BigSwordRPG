using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        public Hero(string name, int health, int level, int damage, string type, int speed, List<Abilities> abilities) : base(name, health, level, damage, type, speed, abilities)
        {
        }

        public int UseAbilities(string nameAbilities)
        {
            for(int i=0; i<Abilities.Count(); i++)
            {
                if (Abilities[i].Name == nameAbilities)
                {
                    if (Abilities[i].Type == "attackPhysics")
                    {
                        return Abilities[i].Damage;//ad
                    }
                    else
                    {
                        return Abilities[i].Damage;//ap
                    }
                }
            }
            return 0;
           
        }

    }
}