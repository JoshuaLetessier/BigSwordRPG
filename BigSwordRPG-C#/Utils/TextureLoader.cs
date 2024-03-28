using BigSwordRPG.Utils.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG_C_.Utils
{
    public class TextureLoader
    {
        string groundLevel;
        string foregroundColor;
        string backgroundColor;

        public TextureLoader() { }

        public Texture getTexture(string s2, Texture texture)
        {

            for (int i = 0; i < s2.Length; i++)
            {
                if (s2[i] == '\\' && s2[i + 3] != 'm')
                {
                    groundLevel = "";
                    foregroundColor = "";
                    backgroundColor = "";

                    groundLevel += s2[i + 3];
                    groundLevel += s2[i + 4];
                    if (groundLevel == "38")
                    {
                        i += 8;
                        while (s2[i] != ';')
                        {
                            foregroundColor += s2[i];
                            i++;
                        }
                        i += 6;
                        while (s2[i] != 'm')
                        {
                            backgroundColor += s2[i];
                            i++;
                        }
                        i++;
                        while (s2[i] == '▄')
                        {
                            texture.PixelsBuffer.Add(new Pixel(int.Parse(foregroundColor), int.Parse(backgroundColor)));
                            i++;
                        }
                        i--;
                    }
                    else
                    {

                        i += 8;
                        while (s2[i] != 'm')
                        {
                            backgroundColor += s2[i];
                            i++;
                        }
                        i++;
                        if (i < s2.Length)
                        {
                            while (s2[i] == ' ')
                            {
                                texture.PixelsBuffer.Add(new Pixel(int.Parse(backgroundColor), int.Parse(backgroundColor)));
                                i++;
                            }
                            i--;

                        }
                    }
                }
            }

            return texture;
        }       
    }
}
