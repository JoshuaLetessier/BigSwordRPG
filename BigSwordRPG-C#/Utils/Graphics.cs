using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG.Utils.Graphics
{
    public struct Pixel
    {
        public int foregroundColor;
        public int backgroundColor;

        public Pixel(int foregroundColor, int backgroundColor)
        { 
            this.foregroundColor = foregroundColor; 
            this.backgroundColor = backgroundColor; 
        }
    }

    public struct Texture
    {
        public int[] Size;
        public List<Pixel> PixelsBuffer;

    }
}
