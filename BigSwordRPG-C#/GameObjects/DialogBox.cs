using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;

namespace BigSwordRPG.GameObjects
{
    public class DialogBox : GameObject
    {
        public DialogBox():
            base(new int[2] {
                GameManager.Instance.Renderer.Camera.Position[0],
                GameManager.Instance.Renderer.Camera.Position[1] + GameManager.Instance.Renderer.Camera.Size[1] - 10
                },
                new Texture()
                {
                    Size = new int[2] { 3, 10 },
                    PixelsBuffer = new List<Pixel>() {
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),

                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),

                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0),
                        new Pixel(0, 0)
                    }
                }
            )
        { }
    }
}
