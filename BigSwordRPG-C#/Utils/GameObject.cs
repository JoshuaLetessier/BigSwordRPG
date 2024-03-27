using BigSwordRPG.Assets;
using BigSwordRPG.Utils.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Utils
{
    public class GameObject
    {
        private int[] _position = { 0, 0 };
        private Texture _texture;
        private int _spriteIndex = 0;

        public int[] Position { get => _position; set => _position = value; }
        public Texture Texture { get => _texture; set => _texture = value; }
        public int SpriteIndex { get => _spriteIndex; set => _spriteIndex = value; }

        public GameObject() { }
        public int Initialize() { return 0; }

        /*public int Move(int[] newPosition) { 
            Renderer->Move()
        }*/


    }
}