using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG_C_.Utils
{
    internal class Utils
    {
        Utils() { }
        public int RandomFonction(int value, int value2)
        {
            Random random = new Random();

            return random.Next(value, value2);
        }
    }
}
