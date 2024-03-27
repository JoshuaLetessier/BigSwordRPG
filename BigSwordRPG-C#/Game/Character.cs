using BigSwordRPG.Utils;
using BigSwordRPG_C_;

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
        private float _speed;
        private bool _isDead;

        private Dictionary<string, Abilities> _abilities;



        //private List<Equipement> _equipement;

        //Property
        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set
            {
                if(_health - value < 0 )
                    _health = 0;
            }
        }
        public int Level { get => _level; set => _level = value; }

        public float Speed { get => _speed; set => _speed = value; }
        public bool IsDead { get => _isDead; set => _isDead = value; }
        public Dictionary<string,Abilities> CAbilities { get => _abilities; set => _abilities = value; }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public float HealthMultiplier { get => _healthMultiplier; set => _healthMultiplier = value; }
        public float AttMultiplier { get => _attMultiplier; set => _attMultiplier = value; }
        public float HealMultiplier { get => _healMultiplier; set => _healMultiplier = value; }



        //Méthodes
        public Character(string name, int health, int maxHealth, int level, float healthMultiplier, float attMultiplier, float healMultiplier, float speed, Dictionary<string,Abilities> abilities,bool isDead)
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
            CAbilities = new Dictionary<string,Abilities>();
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

        public override void Destroy()
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
        public  void Talk()
        {
            throw new NotImplementedException();
        }


    }
}