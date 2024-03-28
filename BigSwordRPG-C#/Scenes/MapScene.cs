using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.Json.Nodes;
using BigSwordRPG_C_;
using BigSwordRPG_C_.Utils;
using BigSwordRPG.Utils;
using BigSwordRPG_C_.Game;

namespace BigSwordRPG.Assets
{
    public class MapScene : Scene
    {
        private Camera testCam;
        private Player testPlayer;
        private GameManager gameManager;
        private Music music;

        private string FilePath = "../../../Asset/Music/robot.mp3";


        public MapScene(Camera testCam, Player testPlayer)
        {
            this.testCam = testCam;
            this.testPlayer = testPlayer;
            music = new Music();
        }

        public override void Draw()
        {
            music.CloseMusic();
            Task audioTask = Task.Run(() => music.ImporterMP3(FilePath));

            Console.SetCursorPosition(0, 0);

            Console.SetBufferSize(854, 480);

            StreamReader sr = new StreamReader("../../../Asset/Image/map.txt");//Remettre le fichier dans Debug pour le déploiement
            //StreamReader sr = new StreamReader("map.txt");//Remettre le fichier dans Debug pour le déploiement
            string s2 = sr.ReadToEnd().Replace("\\e","\x1b");

            Console.Write(s2);
            sr.Dispose();          

            Console.SetCursorPosition(testCam.joueur.Position[0], testCam.joueur.Position[1]);

            testCam.setPositionCamera(testCam.Size);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}