using BigSwordRPG.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    public abstract class GameObject
    {
        public GameObject() { }
        ~GameObject() { }

        public abstract void Draw();
        public abstract void Updtate();
        public abstract void Destroy();
    }

    public class Transform
    {
        private int[] _position = { 0, 0 };
        private int[] _size = { 0, 0 };
        public int[] Position { get => _position; set => _position = value; }
        public int[] Size { get => _size; set => _size = value; }
    }

}