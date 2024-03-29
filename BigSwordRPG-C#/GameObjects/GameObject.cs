using BigSwordRPG.Utils.Graphics;
using BigSwordRPG.Core;

namespace BigSwordRPG.GameObjects
{
    public abstract class GameObject
    {
        public GameObject() { }
        ~GameObject() { }



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

        public bool IsColliding(GameObject comparedGameObject)
        {
            if (
                comparedGameObject.Position[0] + comparedGameObject.Texture.Size[0] < Position[0]
                || comparedGameObject.Position[0] > Position[0] + Texture.Size[0]
                || comparedGameObject.Position[1] + comparedGameObject.Texture.Size[1] < Position[1]
                || comparedGameObject.Position[1] > Position[1] + Texture.Size[1]
            )
            {
                return false;
            }
            return true;
        }
    }
}