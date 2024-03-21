using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        List<Abilities> actAbilities;
        public Hero(string name, int health, int level, int damage, string type, int speed, List<Abilities> abilities) : base(name, health, level, damage, type, speed, abilities)
        {
            actAbilities = abilities;
        }

        public int UseAbilities(int indexAbilities)
        {
            switch (actAbilities[indexAbilities].Type)
            {
                case (int)actionType.ATT:
                    return actAbilities[indexAbilities].Damage;//ad
                case (int)actionType.HEAL:
                    break;
                case (int)actionType.MAG:
                    break;
                default:
                    break;
            }
            return 0;

        }

    }
}