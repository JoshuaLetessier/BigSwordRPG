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
        BUFF,
        ESCAPED
    }

    enum ZoneAction
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
        private Enum _type; 
        private float _damage;
        private float _heal;
        private float _speedUp;
        private int _cooldown;
        private float _cost;
        private Enum _zone;


        //Property
        public string Name { get => _name; set => _name = value; }
        public Enum Type { get => _type; set => _type = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public int Cooldown { get => _cooldown; set => _cooldown = value; }
        public float Cost { get => _cost; set => _cost = value; }
        public float Heal { get => _heal; set => _heal = value; }
        public Enum Zone { get => _zone; set => _zone = value; }
        public float SpeedUp { get => _speedUp; set => _speedUp = value; }
    }
}