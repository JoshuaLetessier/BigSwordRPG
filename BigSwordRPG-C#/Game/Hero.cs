using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Hero : Character
    {
        public override int MakeDammage()
        {
            return Damage;
        }

        public override void TakeDammage(int attackPoint)
        {
            Health -= attackPoint;
        }

        public override void Talk()
        {
            throw new NotImplementedException();
        }

        public override int UseAbilities(string nameAbilities)
        {
            _abilities = new();
            
            if(_abilities.ContainsKey(nameAbilities))
            {
                if (_abilities[nameAbilities].Type == true)
                {
                    return _abilities[nameAbilities].Damage;//ad
                }
                else
                {
                    return _abilities[nameAbilities].Damage;//ap
                }
            } 
            return 0;
           
        }
    }
}