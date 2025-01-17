﻿using BigSwordRPG.GameObjects;

namespace BigSwordRPG.Game
{
    public class Item : GameObject
    {
        // Champs
        private string _name;
        private int _value;
        private int _rarety;

        public Item() { }

        public Item(string name, int value, int rarety)
        {
            _name = name;
            _value = value;
            _rarety = rarety;
        }

        ~Item() { }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public virtual void Use(List<Hero> heroes)
        {
            int indexInventory = 0;
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                foreach (Hero hero in heroes)
                {
                    bool isSelected = hero == heroes[indexInventory];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{hero}");
                }

                keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.DownArrow && indexInventory + 1 < heroes.Count)
                {
                    indexInventory++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && indexInventory - 1 >= 0)
                {
                    indexInventory--;
                }

            } while (keyPressed != ConsoleKey.Enter);

            string heroIndex = heroes[indexInventory].SelectHero(heroes);

            for (int i = 0; i < heroes.Count; i++)
            {
                if (heroes[i].Name == heroIndex)
                {
                    if (this.Name == "Régénérateur Bio-Électrique" || this.Name == "Serum Électro-Cellulaire")
                    {
                        heroes[i].Heal(this.Value);
                        break;
                    }
                    else
                    {
                        heroes[i].ManaHeal(this.Value);
                        break;
                    }
                }
            }

        }


        // Getter
        public string Name { get => _name; set => _name = value; }
        public int Value { get => _value; set => _value = value; }
        public int Rarety { get => _rarety; set => _rarety = value; }

        private static void ChangeLineColor(bool shouldHighlight)
        {
            Console.BackgroundColor = shouldHighlight ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = shouldHighlight ? ConsoleColor.Black : ConsoleColor.White;
        }
    }

    public struct Potion
    {
        public Potion()
        {
        }

        private Item potionMineur = new Item("Régénérateur Bio-Électrique", 25, 0);
        private Item potionMajeur = new Item("Serum Électro-Cellulaire", 75, 1);

        private Item manaMineur = new Item("Booster Synaptique", 25, 0);
        private Item manaMajeur = new Item("Elixir Neuro-Énergétique", 75, 1);

        public List<Item> ListItem()
        {
            List<Item> listItems = new List<Item>();

            listItems.Add(potionMineur);
            listItems.Add(potionMajeur);

            listItems.Add(manaMineur);
            listItems.Add(manaMajeur);

            return listItems;
        }
    }
}