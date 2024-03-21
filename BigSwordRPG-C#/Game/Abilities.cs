using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG_C_
{
    public enum actionType
    {
        ATT = 1,
        HEAL,
        MAG
    }

    public class Abilities
    {
        public Abilities() { }
        ~Abilities() { }

        //Champ
        private string _name;
        private int _type;
        private int _damage;
        private int _cooldown;
        private int _cost;


        //Property
        public string Name { get => _name; set => _name = value; }
        public int Type { get => _type; set => _type = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public int Cooldown { get => _cooldown; set => _cooldown = value; }
        public int Cost { get => _cost; set => _cost = value; }

        //Méthodes
    }
}