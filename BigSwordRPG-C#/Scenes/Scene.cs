using BigSwordRPG.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Assets
{
    public abstract class Scene
    {
        private Scene _previousScene;
        private List<GameObject> _gameObjects;
        private Dictionary<ConsoleKey, List<Action>> _actionsMap;
        public Scene PreviousScene { get => _previousScene; set => _previousScene = value; }
        public List<GameObject> GameObjects { get => _gameObjects; set => _gameObjects = value; }
        public Dictionary<ConsoleKey, List<Action>> ActionsMap { get => _actionsMap; set => _actionsMap = value; }

        public Scene()
        {
            ActionsMap = new Dictionary<ConsoleKey, List<Action>>();
            GameObjects = new List<GameObject>();
        }
        ~Scene() { }


        public int Initialize(Scene previousScene)
        {
            PreviousScene = previousScene;
            return 0;
        }

        public virtual void Draw()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw();
            }
        }
        public abstract void Run();

        public virtual void Exit() { }

        public void RegisterAction(ConsoleKey consoleKey, Action action)
        {
            if (ActionsMap.ContainsKey(consoleKey) == false)
            {
                ActionsMap.Add(consoleKey, new List<Action>() { action });
                return;
            }
            ActionsMap[consoleKey].Add(action);
        }
        public void UnregisterAction(ConsoleKey consoleKey, Action action)
        {
            if (ActionsMap.ContainsKey(consoleKey) == false)
            {
                ActionsMap.Add(consoleKey, new List<Action>() { action });
                return;
            }
            ActionsMap[consoleKey].Add(action);
        }
    }
}