namespace BigSwordRPG_C_
{
    public class SelectMenu
    {
        public string menuChoix;
        public Action ToDo;

        public void LoadAndDisplayMenu(List<SelectMenu> menu)
        {
            // Affichage du menu
            Console.WriteLine("Menu :");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i].menuChoix}");
            }

            // Demande à l'utilisateur de choisir une option
            Console.WriteLine("Choisissez une option :");
            int choice;
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= menu.Count)
            {
                // Appel de l'action associée à l'option choisie
                menu[choice - 1].ToDo();
            }
            else
            {
                Console.WriteLine("Choix invalide !");
            }
        }
    }
}