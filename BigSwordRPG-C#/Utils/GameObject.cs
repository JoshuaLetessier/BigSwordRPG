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
}