using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Assets
{
    public abstract class Scene
    {
        public Scene() { }
        ~Scene() { }

        public abstract void Draw();
        public abstract void Update();
    }
}   