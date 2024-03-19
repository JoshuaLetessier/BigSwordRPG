using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Ennemy : Character
    {
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

        public override int UseAbilities(string nameAbilities)
        {
            throw new NotImplementedException();
        }
    }
}