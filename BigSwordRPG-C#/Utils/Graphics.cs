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

    public class Texture
    {
        private List<Pixel> _pixelsBuffer;
        private int[] _size;

        public List<Pixel> PixelsBuffer { get => _pixelsBuffer; set => _pixelsBuffer = value; }
        public int[] Size { get => _size; set => _size = value; }
    }
}
