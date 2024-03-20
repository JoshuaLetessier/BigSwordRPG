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
            StreamReader sr = new StreamReader("../../../Asset/Image/map.txt");
            string s2 = sr.ReadToEnd().Replace("\\e","\x1b");
            sr.Close();

            Console.Write(s2);

        }
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}