using BigSwordRPG.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    public class Transform
    {
        private int[] _position = { 0, 0 };
        private int[] _size = { 0, 0 };
        public int[] Position { get => _position; set => _position = value; }
        public int[] Size { get => _size; set => _size = value; }
    }
    public class GameObject
    {
        private Transform _transform;
        private int _spriteIndex = 0;
        public int SpriteIndex { get => _spriteIndex; set => _spriteIndex = value; }
        public Transform Transform { get => _transform; set => _transform = value; }

        public GameObject() { }
        public int Initialize() { return 0; }

        /*public int Move(int[] newPosition) { 
            Renderer->Move()
        }*/


    }
}