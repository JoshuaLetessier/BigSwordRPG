using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Assets
{
    public abstract class Scene
    {
        private Scene _previousScene;
        public Scene PreviousScene { get => _previousScene; set => _previousScene = value; }

        public Scene() { }
        ~Scene() { }


        public int Initialize(Scene previousScene)
        {
            PreviousScene = previousScene;
            return 0;
        }

        public abstract void Draw();
        public abstract void Run();
    }
}   