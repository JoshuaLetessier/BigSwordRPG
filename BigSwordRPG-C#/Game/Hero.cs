using BigSwordRPG_C_;
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
            Abilities = new();
            
            if(Abilities.ContainsKey(nameAbilities))
            {
                if (Abilities[nameAbilities].Type == true)
                {
                    return Abilities[nameAbilities].Damage;//ad
                }
                else
                {
                    return Abilities[nameAbilities].Damage;//ap
                }
            } 
            return 0;
           
        }
    }
}