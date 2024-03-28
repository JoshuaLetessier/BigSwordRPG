using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG_C_.Utils
{
    public class Camera
    {
        public Player joueur;
        private string _size;
        private Func<string> returnSize;
        private int[] cameraSize;

        public Camera(string size)
        {
            joueur = new Player(new int[2] { 150, 60 });
            _size = size;
            if(size == "Fullscreen")
            {
                cameraSize = new int[2] { Console.LargestWindowWidth, Console.LargestWindowHeight };
            }
            else if(size == "QuatreTier")
            {
                cameraSize = new int[2] { 100, 35 };
            }
            else if(size == "TroisDemi")
            {
                cameraSize = new int[2] { 90, 28 };
            }   
        }

        public Camera(Func<string> returnSize)
        {
            this.returnSize = returnSize;
        }

        public string Size { get => _size; set => _size = value; }

        public void setPositionCamera(int[] posJoueur)
        {
            int x = 0;
            int y = 0;
            string sizeWindow = _size;

            if (cameraSize[0] / 2 - posJoueur[0] == 0)
            {
                x = cameraSize[0] / 2;
            }
            else if (cameraSize[0] - posJoueur[0] < 0)
            {
                x = cameraSize[0] - posJoueur[0] / 2;
            }
            else
            {
                x = (cameraSize[0] / 2) + (posJoueur[0] - cameraSize[0]);
            }

            if (cameraSize[1] / 2 - posJoueur[1] == 0)
            {
                y = cameraSize[1] / 2;
            }
            else if (cameraSize[1] - posJoueur[1] < 0)
            {
                y = cameraSize[1] - posJoueur[1] / 2;
            }
            else
            {
                y = (cameraSize[1] / 2) + (posJoueur[1] - cameraSize[1]);
            }

            Console.SetWindowPosition(x, y);
        }
    }
}
