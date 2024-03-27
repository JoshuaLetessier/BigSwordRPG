using BigSwordRPG.Assets;
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

        public Camera(string size)
        {
            joueur = new Player(new int[2] {150,60});
            //resize = new ResizeWindow();
            _size = size;
        }

        public Camera(Func<string> returnSize)
        {
            this.returnSize = returnSize;
        }

        public string Size { get => _size; set => _size = value; }

        public void setPositionCamera(string sizeWindow)
        {
            int[] posJoueur = joueur.Position;
            int x = 0;
            int y = 0;
            if (sizeWindow == "Fullscreen")
            {
                if (Console.LargestWindowWidth / 2 - posJoueur[0] == 0)
                {
                    x = Console.LargestWindowWidth / 2;
                } else if (Console.LargestWindowWidth - posJoueur[0] < 0)
                {
                    x = Console.LargestWindowWidth - posJoueur[0] / 2;
                } else
                {
                    x = (Console.LargestWindowWidth / 2) + (posJoueur[0] - Console.LargestWindowWidth);
                }

                if (Console.LargestWindowHeight / 2 - posJoueur[1] == 0)
                {
                    y = Console.LargestWindowHeight / 2;
                }
                else if (Console.LargestWindowHeight - posJoueur[1] < 0)
                {
                    y = Console.LargestWindowHeight - posJoueur[1] / 2;
                }
                else
                {
                    y = (Console.LargestWindowHeight / 2) + (posJoueur[1] - Console.LargestWindowHeight);
                }
                
            }
            else 
                if (sizeWindow == "QuatreTier")
            {
                if (100 / 2 - posJoueur[0] == 0)
                {
                    x = 100 / 2;
                }
                else if (100 - posJoueur[0] < 0)
                {
                    x = posJoueur[0] - 50;
                }
                else
                {
                    x = (100 / 2) + (posJoueur[0] - 100);
                }

                if (35 / 2 - posJoueur[1] == 0)
                {
                    y = 35 / 2;
                }
                else if (35 - posJoueur[1] < 0)
                {
                    y = posJoueur[1] - 35 / 2;
                }
                else
                {
                    y = (35 / 2) + (posJoueur[1] - 35);
                }
            }
            else
                if (sizeWindow == "TroisDemi")
            {
                if (90 / 2 - posJoueur[0] == 0)
                {
                    x = 90 / 2;
                }
                else if (90 - posJoueur[0] < 0)
                {
                    x = posJoueur[0] - 45;
                }
                else
                {
                    x = (90 / 2) + (posJoueur[0] - 90);
                }

                if (28 / 2 - posJoueur[1] == 0)
                {
                    y = 28 / 2;
                }
                else if (28 - posJoueur[1] < 0)
                {
                    y = posJoueur[1] - 28 / 2;
                }
                else
                {
                    y = (28 / 2) + (posJoueur[1] - 28);
                }
            }

            Console.SetWindowPosition(x, y);
        }
    }
}
