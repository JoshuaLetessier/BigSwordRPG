using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG.Utils.Graphics
{
    public struct TextureRegion
    {
        public int offsetX;
        public int offsetY;
        public int sizeX;
        public int sizeY;
    }

    public struct Pixel // Ultimately, pixels should be a class or something that can inherit from a base to allow both 8BitPixels and RGBPixels.
    {
        public int foregroundColor;
        public int backgroundColor;

        public Pixel(int foregroundColor, int backgroundColor)
        { 
            this.foregroundColor = foregroundColor; 
            this.backgroundColor = backgroundColor; 
        }

        public string GetParsedForeground()
        {
            return $"38;5;{foregroundColor}";
        }
        public string GetParsedBackground()
        {
            return $"48;5;{foregroundColor}";
        }
    }

    public struct Texture // Should be a class or always passed by reference
    {
        public int[] Size;
        public List<Pixel> PixelsBuffer;
    }
}
