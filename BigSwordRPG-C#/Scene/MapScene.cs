using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.Json.Nodes;

namespace BigSwordRPG.Assets
{
    public class MapScene : Scene
    {
        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);

            Console.SetBufferSize(854, 480);

            StreamReader sr = new StreamReader("../../../Asset/Image/map.txt");//Remettre le fichier dans Debug pour le déploiement
            //StreamReader sr = new StreamReader("map.txt");//Remettre le fichier dans Debug pour le déploiement
            string s2 = sr.ReadToEnd().Replace("\\e","\x1b");

            Console.Write(s2);
            sr.Dispose();

            //Pour le player
            int playerX = 156;
            int playerY = 90;
            
            Console.SetCursorPosition(playerX, playerY);
            Console.SetWindowPosition(playerX - 70, playerY - 25);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}