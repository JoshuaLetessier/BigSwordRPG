using BigSwordRPG.Assets;
using BigSwordRPG.Utils.Graphics;
using BigSwordRPG.Core;


namespace BigSwordRPG.GameObjects
{
    public abstract class Interest : GameObject
    {
        public Interest(int[] position) : base(
                position,
                new Texture()
                {
                    Size = new int[2] { 8, 4 },
                    PixelsBuffer = new List<Pixel>() {
                        new Pixel(228, 228),
                        new Pixel(228, 58),
                        new Pixel(228, 58),
                        new Pixel(228, 58),
                        new Pixel(228, 58),
                        new Pixel(228, 58),
                        new Pixel(228, 58),
                        new Pixel(228, 228),

                        new Pixel(58, 58),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(58, 58),

                        new Pixel(58, 58),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(228, 228),
                        new Pixel(58, 58),

                        new Pixel(228, 228),
                        new Pixel(58, 228),
                        new Pixel(58, 228),
                        new Pixel(58, 228),
                        new Pixel(58, 228),
                        new Pixel(58, 228),
                        new Pixel(58, 228),
                        new Pixel(228, 228)
                    }
                }
        )
        { }

        /*public void Interact()
        {
            DialogScene dialogScene = new DialogScene();
            GameManager.Instance.SwitchScene(dialogScene);
        }*/
        public abstract void Interact();
        public void Interact<NewSceneType>() where NewSceneType : Scene, new()
        {
            GameManager.Instance.SwitchScene(new NewSceneType());
        }
    }
}
