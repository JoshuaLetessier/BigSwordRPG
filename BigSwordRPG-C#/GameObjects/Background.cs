using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG.GameObjects
{
    public class Background : GameObject
    {
        public Background(int[] position, Texture texture): base(position, texture) { }

        public override void Updtate()
        {
            throw new NotImplementedException();
        }
    }
}
