using BigSwordRPG_C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Ennemy : Character
    {
        private int _difficulty;


        public int Difficulty { get => _difficulty; set => _difficulty = value; }
        public Ennemy(int difficulty, string name, int health, int level, int damage, string type, int speed, List<Abilities> abilities) : base(name, health, level, damage, type, speed, abilities)
        {
            _difficulty = difficulty;
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

        public int UseRandomAbilities()//attaque ou rate
        {
            /*Random random = new Random();
            Abilities abilities = new Abilities();

            int randomAbilities = random.Next(0, _abilities.Count);

            abilities = _abilities.ElementAt(randomAbilities).Value;

            return abilities.Damage; ;*/
            return 0;
        }
    }
}