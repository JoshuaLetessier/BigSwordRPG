using BigSwordRPG.Utils;

namespace BigSwordRPG.Game
{
    public class Character : GameObject
    {
        private string _name;
        private int _health;
        private int _level;

        private struct Abilities
        {

        }

        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = value; }
        public int Level { get => _level; set => _level = value; }


        public void Heal(int healPoint) { _health += healPoint; }
    }
}