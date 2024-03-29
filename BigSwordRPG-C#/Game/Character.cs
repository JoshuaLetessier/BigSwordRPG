﻿using BigSwordRPG.Utils;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Game;
using System.Linq.Expressions;

namespace BigSwordRPG.Game
{
    public class Character : GameObject
    {
        //champ
        private string _name;
        private int _health;
        private int _maxHealth;
        private int _level;
        private float _healthMultiplier;
        private float _attMultiplier;
        private float _healMultiplier;
        private int _speed;
        private int _PM;
        private int _PMMax;
        private bool _isDead;

        private Dictionary<string, Abilities> _abilities;
        private Dictionary<string, Equipement> _equipements;

        //Property
        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set
            {
                if(_health - value < 0 )
                    _health = 0;
                if(_health + value > MaxHealth)
                        _health = MaxHealth;
            }
        }
        public int Level { get => _level; set => _level = value; }

        public int Speed { get => _speed; set => _speed = value; }
        public bool IsDead { get => _isDead; set => _isDead = value; }
        public Dictionary<string,Abilities> CAbilities { get => _abilities; set => _abilities = value; }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public float HealthMultiplier { get => _healthMultiplier; set => _healthMultiplier = value; }
        public float AttMultiplier { get => _attMultiplier; set => _attMultiplier = value; }
        public float HealMultiplier { get => _healMultiplier; set => _healMultiplier = value; }
        public int PM { get => _PM; set
            {
                if(_PM - value < 0)
                    _PM = 0;
                if (_PM + value > _PMMax)
                    _PM = _PMMax;
            }
        }
        public int PMMax { get => _PMMax; set => _PMMax = value; }
        public Dictionary<string, Equipement> Equipements { get => _equipements; set => _equipements = value; }
       



        //Méthodes
        public Character(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, int speed, Dictionary<string, Abilities> abilities, bool isDead, int pM, int PMMax, Dictionary<string, Equipement> equipements)
        {
            _name = name;
            _health = health;
            _maxHealth = maxHealth;
            _level = level;
            _healthMultiplier = healthMultiplier;
            _attMultiplier = attMultiplier;
            _healMultiplier = healMultiplier;
            _speed = speed;
            _abilities = abilities;
            _isDead = isDead;
            CAbilities = new Dictionary<string, Abilities>();
            _PM = pM;
            _PMMax = pM;
            _equipements = equipements;
        }

        public Character()
        {
        }

        ~Character() { }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Updtate()
        {
            throw new NotImplementedException();
        }


        public void TakeDammage(int attackPoint)
        {
            Health -= attackPoint;
        }

       /* public int MakeDammage()
        {
            return Damage;
        }*/

        public void Heal(int healValue)
        {
            Health += healValue;
        }

        public void ManaHeal(int manaValue)
        {
            PMMax += manaValue;
        }

        public void takeDammage(int healValue)
        {
            Health -= healValue;
        }

        public void UseMana(int manaValue)
        {
            PMMax -= manaValue;
        }

        public  void Talk()
        {
            throw new NotImplementedException();
        }
    }
}