using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Assets
{
    public class MenuScene : Scene
    {
        public override void Draw()
        {
            StreamReader srName = new StreamReader("../../../Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd().Replace("\\e", "\x1b");
            srName.Close();

            StreamReader srNouvellePartie = new StreamReader("../../../Asset/Image/nouvelle.txt");//Remettre le fichier dans Debug pour le déploiement
            string NouvellePartie = srNouvellePartie.ReadToEnd().Replace("\\e", "\x1b");
            srNouvellePartie.Close();

            StreamReader srContinuerPartie = new StreamReader("../../../Asset/Image/continuer.txt");//Remettre le fichier dans Debug pour le déploiement
            string ContinuerPartie = srContinuerPartie.ReadToEnd().Replace("\\e", "\x1b");
            srContinuerPartie.Close();

            StreamReader srOption = new StreamReader("../../../Asset/Image/option.txt");//Remettre le fichier dans Debug pour le déploiement
            string Option = srOption.ReadToEnd().Replace("\\e", "\x1b");
            srOption.Close();

            StreamReader srQuitter = new StreamReader("../../../Asset/Image/quitter.txt");//Remettre le fichier dans Debug pour le déploiement
            string Quitter = srQuitter.ReadToEnd().Replace("\\e", "\x1b");
            srQuitter.Close();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(NouvellePartie);
            Console.WriteLine(ContinuerPartie);
            Console.WriteLine(Option); 
            Console.WriteLine(Quitter);

        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}