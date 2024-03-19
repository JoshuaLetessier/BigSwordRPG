using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG_C_
{
    public class Abilities
    {
        public Abilities() { }
        ~Abilities() { }

        //Champ
        private string _name;
        private bool type; // true attack false magic
        private int _damage;
        private int _cooldown;
        private int _cost;


        //Property
        public string Name { get => _name; set => _name = value; }
        public bool Type { get => type; set => type = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public int Cooldown { get => _cooldown; set => _cooldown = value; }
        public int Cost { get => _cost; set => _cost = value; }

        //Méthodes
    }
}