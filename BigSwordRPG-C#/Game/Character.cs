using BigSwordRPG.Utils;
using BigSwordRPG_C_;

namespace BigSwordRPG.Game
{
    public abstract class Character : GameObject
    {
        //champ
        private string _name;
        private int _health;
        private int _level;
        private int _damage;
        private string _type;
        private int _speed;

        public Dictionary<string, Abilities> _abilities;

        //Property
        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = value; }
        public int Level { get => _level; set => _level = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public string Type { get => _type; set => _type = value; }
        public int Speed { get => _speed; set => _speed = value; }


        //Méthodes
        public Character() { } 
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

        public abstract void TakeDammage(int attackPoint);
        public abstract int MakeDammage();
        public abstract void Talk();


    }
}