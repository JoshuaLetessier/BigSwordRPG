using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Ennemy : Character
    {
        private int difficulty;

        public int Difficulty { get => difficulty; set => difficulty = value; }

        public override int MakeDammage()
        {
            return Damage;
            throw new NotImplementedException();
        }

        public override void TakeDammage(int attackPoint)
        {
            Health -= attackPoint;
            throw new NotImplementedException();
        }

        public override void Talk()
        {
            throw new NotImplementedException();
        }

        public int UseAbilities()
        {
            switch(Difficulty) { 
                case 0:
                    UseRandomAbilities();
                    return 0;
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 3:
                    return 3;
            }
            return 0;
        }

        public int UseRandomAbilities()
        {
            Random random = new Random();
            Abilities abilities = new Abilities();

            int randomAbilities = random.Next(0, _abilities.Count);

            abilities = _abilities.ElementAt(randomAbilities).Value;

            return abilities.Damage; ;
        }
    }
}