using BigSwordRPG.Assets;
using BigSwordRPG_C_.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace BigSwordRPG.Assets
{
    public class MenuAccueil : Scene
    {
        private SelectMenu menuSelection;
        private MenuScene menu;
        private SaveManager saveManager;

        public MenuAccueil() 
        {
            menuSelection = new SelectMenu();
            menu = new MenuScene();
            saveManager = new SaveManager();
        }

        public override void Draw()
        {
            Console.Clear();
            Console.SetCursorPosition(50, 0);

            StreamReader srName = new StreamReader("../../../Asset/Image/nameGame.txt");//Remettre le fichier dans Debug pour le déploiement
            string Name = srName.ReadToEnd();

            StreamReader srReprendre = new StreamReader("../../../Asset/Image/reprendre.txt");
            string Reprendre = srReprendre.ReadToEnd();

            StreamReader srSauvegarde = new StreamReader("../../../Asset/Image/langues.txt");
            string Sauvegarde = srSauvegarde.ReadToEnd();

            StreamReader srQuitter = new StreamReader("../../../Asset/Image/quitter.txt");
            string Quitter = srQuitter.ReadToEnd();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Name);
            Console.ForegroundColor = ConsoleColor.White;

            List<SelectMenu> optionMenu = new List<SelectMenu>()
            {
                new SelectMenu { menuChoix = Reprendre, ToDo = ReturnInGame },
                new SelectMenu { menuChoix = Sauvegarde, ToDo = LaunchSave },
                new SelectMenu { menuChoix = Quitter, ToDo = QuitGame }
            };

            menuSelection.HandleUserInput(optionMenu);

            srName.Dispose();
            srReprendre.Dispose();
            srSauvegarde.Dispose();
            srQuitter.Dispose();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public void ReturnInGame()
        {
            throw new NotImplementedException();
        }

        public void LaunchSave()
        {
            //saveManager.Save();
        }

        public void QuitGame()
        {
            //saveManager.Save();
            menu.Draw();
        }
    }
}