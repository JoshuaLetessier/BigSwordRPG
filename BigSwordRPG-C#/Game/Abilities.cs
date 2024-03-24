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
        CAPA,
        ESCAPED
    }

    enum Zone
    {
        Unique = 1,
        Near,
        All
    }

    public class Abilities
    {
        public Abilities() { }
        ~Abilities() { }

        //Champ
        private string _name;
        private int _type; 
        private int _damage;
        private int _heal;
        private float _speedUp;
        private int _cooldown;
        private int _cost;
        private int _zone;


        //Property
        public string Name { get => _name; set => _name = value; }
        public int Type { get => _type; set => _type = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public int Cooldown { get => _cooldown; set => _cooldown = value; }
        public int Cost { get => _cost; set => _cost = value; }
        public int Heal { get => _heal; set => _heal = value; }
        public int Zone { get => _zone; set => _zone = value; }
        public float SpeedUp { get => _speedUp; set => _speedUp = value; }
    }
}