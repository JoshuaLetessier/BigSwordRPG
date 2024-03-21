using BigSwordRPG.Utils;
using BigSwordRPG_C_;

namespace BigSwordRPG.Game
{
    public class Character : GameObject
    {
        //champ
        private string _name;
        private int _health;
        private int _level;
        private int _damage;
        private string _type;
        private int _speed;
        private List<Abilities> _abilities;

        //Property
        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = value; }
        public int Level { get => _level; set => _level = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public string Type { get => _type; set => _type = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public List<Abilities> Abilities { get => _abilities; set => _abilities = value; }


        //Méthodes
        public Character(string name, int health, int level, int damage, string type, int speed, List<Abilities> abilities)
        {
            _name = name;
            _health = health;
            _level = level;
            _damage = damage;
            _type = type;
            _speed = speed;
            _abilities = abilities;
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

        public  int MakeDammage()
        {
            return Damage;
        }
        public  void Talk()
        {
            throw new NotImplementedException();
        }


    }
}