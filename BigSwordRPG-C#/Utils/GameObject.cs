using BigSwordRPG.Utils.Graphics;

namespace BigSwordRPG.Utils
{
    public abstract class GameObject
    {
        public GameObject() { }
        ~GameObject() { }


        public abstract void Updtate();


        private int[] _position = { 0, 0 };
        private Texture _texture;
        private int _spriteIndex = 0;

        public int[] Position { get => _position; set => _position = value; }
        public Texture Texture { get => _texture; set => _texture = value; }
        public int SpriteIndex { get => _spriteIndex; set => _spriteIndex = value; }

        public GameObject(int[] position, Texture texture)
        {
            Texture = texture;
            Position = position;
        }

        public virtual void Draw()
        {
            GameManager.Instance.Renderer.DrawTexture(Position, Texture);
        }
        /*public int Move(int[] newPosition) { 
            Renderer->Move()
        }*/
    }
}