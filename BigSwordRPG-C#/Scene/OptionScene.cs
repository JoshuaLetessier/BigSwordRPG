using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BigSwordRPG_C_;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG.Assets
{
    public class OptionScene : Scene
    {
        private SelectMenu test;

        public OptionScene() 
        {
            test = new SelectMenu();
        }
        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            StreamReader srName = new StreamReader("../../../Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srResolution = new StreamReader("../../../Asset/Image/resolution.txt");
            string Resolution = srResolution.ReadToEnd();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;

            List<SelectMenu> optionMenu = new List<SelectMenu>()
            {
                new SelectMenu { menuChoix = Resolution, ToDo = Update }
            };

            test.LoadAndDisplayMenu(optionMenu);

            srName.Dispose();
            srResolution.Dispose();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
