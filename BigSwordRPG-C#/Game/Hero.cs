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
        
        public Hero(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, int speed, List<Abilities> abilities, bool isDead) :base(name, health,maxHealth,level,healthMultiplier,attMultiplier,healMultiplier,speed, abilities,isDead)
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

}