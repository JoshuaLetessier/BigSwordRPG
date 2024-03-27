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

        public Camera()
        {
            joueur = new Player(new int[2] {150,60});
        }

        public void setPositionCamera()
        {
            Console.SetWindowPosition(joueur.Position[0] - 70, joueur.Position[1] - 25);
        }
    }
}
