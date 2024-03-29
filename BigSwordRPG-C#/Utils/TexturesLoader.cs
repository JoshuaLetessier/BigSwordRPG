using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigSwordRPG.Utils.Graphics;

namespace BigSwordRPG.Utils
{
    static public class TexturesLoader
    {

        private static Dictionary<string, Texture> _textures = new Dictionary<string, Texture>();
        public static Dictionary<string, Texture> Textures { get => _textures; set => _textures = value; }

#if DEBUG
        const string TEXTURE_PATH = "../../../Asset/Image/";
#else
        const string TEXTURE_PATH = "./Data/Assets/Textures/";
#endif
        const string TEXTURE_EXTENSION = ".txt";
        const int SUCCESSFULLY_LOADED = 0;
        const int CORRUPTED_FILE = -1;
        const int INVALID_TEXTURE_VERSION = -2;

        public static Texture GetTexture(string filename)
        {
            if (Textures.ContainsKey(filename))
            {
                return Textures[filename];
            }
            else
            {
                if (LoadTexture(filename) == SUCCESSFULLY_LOADED)
                {
                    return Textures[filename];
                }
                else
                {
                    throw new Exception($"Texture : {filename}{TEXTURE_EXTENSION} couldn't be loaded."); // Should eventually return an empty texture
                }
            }
        }

        public static Texture GenerateSolidRectangleTexture(int EightBitColorCode, int[] Size)
        {
            Texture texture = new Texture();
            texture.Size = Size;
            texture.PixelsBuffer = new List<Pixel>();
            for (int i = 0; i < Size[0] * Size[1]; i++)
            {
                texture.PixelsBuffer.Add(new Pixel(EightBitColorCode, EightBitColorCode));
            }
            return texture;
        }

        /// <summary>
        /// Should only be called to preload textures. Call GetTexture to get a texture instead.
        /// </summary>
        /// <param name="filename">Filename shouldn't contain the extension and the should be texture stored in Asset/Image</param>
        /// <returns></returns>
        public static int LoadTexture(string filename)
        {
            StreamReader textureStreamReader = new StreamReader($"{TEXTURE_PATH}{filename}{TEXTURE_EXTENSION}"); //Remettre le fichier dans Debug pour le déploiement
            /* 
            * Should introduce checksum to check file corruption
            * and only use version to indicate which parsing method to use.
            */
            if(textureStreamReader.ReadLine().Split(':')[0] != "version")
            {
                return CORRUPTED_FILE;
            }
            string[] textureSize = textureStreamReader.ReadLine().Split(':')[1].Replace(" ", "").Split(',');
            string rawTextureString = textureStreamReader.ReadToEnd();
            textureStreamReader.Dispose();

            Texture texture = new Texture();
            texture.Size = new int[2] { int.Parse(textureSize[0]), int.Parse(textureSize[1]) };
            texture.PixelsBuffer = new List<Pixel>();

            string groundLevel;
            string foregroundColor;
            string backgroundColor;
            char pixelChar;
            // Could be optimized in terms of code repetition and encapsulated into "ParseTextureData()"
            for (int i = 0; i < rawTextureString.Length; i++) 
            {
                if (rawTextureString[i] == '\\' && rawTextureString[i + 3] != 'm')
                {
                    groundLevel = "";
                    foregroundColor = "";
                    backgroundColor = "";

                    groundLevel += rawTextureString[i + 3];
                    groundLevel += rawTextureString[i + 4];
                    i += 8;
                    if (groundLevel == "38")
                    {
                        pixelChar = '▄';
                        
                        while (rawTextureString[i] != ';')
                        {
                            foregroundColor += rawTextureString[i];
                            i++;
                        }
                        i += 6;
                        while (rawTextureString[i] != 'm')
                        {
                            backgroundColor += rawTextureString[i];
                            i++;
                        }
                    }
                    else
                    {
                        pixelChar = ' ';
                        while (rawTextureString[i] != 'm')
                        {
                            backgroundColor += rawTextureString[i];
                            i++;
                        }
                        foregroundColor = backgroundColor;
                    }
                    i++;
                    while (rawTextureString[i] == pixelChar)
                    {
                        texture.PixelsBuffer.Add(new Pixel(int.Parse(foregroundColor), int.Parse(backgroundColor)));
                        i++;
                    }
                    i--;
                }
            }
            Textures.Add(filename, texture);
            return SUCCESSFULLY_LOADED;
        }            
    }
}
