namespace BigSwordRPG.Assets
{
    public abstract class Scene
    {

        public Scene()
        {

        }
        ~Scene() { }

        public abstract void Draw();
        public abstract void Update();
    }
}