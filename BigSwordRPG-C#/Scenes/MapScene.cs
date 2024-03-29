﻿using BigSwordRPG_C_;
using BigSwordRPG_C_.Utils;

namespace BigSwordRPG.Assets
{
    public class MapScene : Scene
    {
        private Camera testCam;
        private Player testPlayer;

        public MapScene(Camera testCam, Player testPlayer)
        {
            this.testCam = testCam;
            this.testPlayer = testPlayer;


        }

        public override void Draw()
        {

            Console.SetCursorPosition(0, 0);

            Console.SetBufferSize(854, 480);

            StreamReader sr = new StreamReader("../../../Asset/Image/map.txt");//Remettre le fichier dans Debug pour le déploiement
            //StreamReader sr = new StreamReader("map.txt");//Remettre le fichier dans Debug pour le déploiement
            string s2 = sr.ReadToEnd().Replace("\\e", "\x1b");

            Console.Write(s2);
            sr.Dispose();

            Console.SetCursorPosition(testCam.joueur.Position[0], testCam.joueur.Position[1]);

            testCam.setPositionCamera();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}