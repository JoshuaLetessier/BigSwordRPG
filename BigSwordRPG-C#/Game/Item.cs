using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigSwordRPG.Utils;

namespace BigSwordRPG.Game
{
    public class Item : GameObject
    {
        // Champs
        private string _name;
        private int _value;

        public Item(string name, int value)
        {
            _name = name;
            _value = value;
        }

        ~Item() { }



        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Updtate()
        {
            throw new NotImplementedException();
        }

        public void Use()
        {
            throw new NotImplementedException();
        }

        // Getter
        public string Name { get => _name; set => _name = value; }
        public int Value { get => _value; set => _value = value; }

    }
}