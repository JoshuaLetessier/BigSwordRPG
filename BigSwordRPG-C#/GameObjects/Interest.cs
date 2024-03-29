using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigSwordRPG.Assets;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;

namespace BigSwordRPG.GameObjects
{
    public class Interest : GameObject
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

        public void Interact<NewSceneType>() where NewSceneType : Scene, new()
        {
            GameManager.Instance.SwitchScene(new NewSceneType());
        }
    }
}
