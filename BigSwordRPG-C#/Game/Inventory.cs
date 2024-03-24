using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG.Game
{
    public class Inventory
    {
        // Champs
        private List<Item> _inventory;
        
        public Inventory() 
        {
            // Define the initial size of the inventory
            _inventory = new List<Item>( new Item[10] );
            //_inventory = new List<Item>(10);
        }

        ~Inventory() { _inventory.Clear(); }

        public void Store(Item newItem)
        {
            if ( newItem != null && _inventory.Count != 10 )
            {
                _inventory.Add(newItem);
                Console.WriteLine($"{newItem.Name} a été ajouter à votre inventaire !");
            }
            else
            {
                Console.WriteLine("L'inventaire est plein !");
                newItem?.Destroy();
                // Possible add of a menu for exchange
            }
        }

        public void Open()
        {
            int indexInventory = 0;
            ConsoleKey keyPressed;
            do
            {
                foreach (Item item in _inventory)
                {
                    bool isSelected = item == _inventory[indexInventory];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{item}");
                }

                keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.DownArrow && indexInventory + 1 < _inventory.Count)
                {
                    indexInventory++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && indexInventory - 1 >= 0)
                {
                    indexInventory--;
                }
            } while (keyPressed != ConsoleKey.Enter);

            _inventory[indexInventory].Use();
        }

        private static void ChangeLineColor(bool shouldHighlight)
        {
            Console.BackgroundColor = shouldHighlight ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = shouldHighlight ? ConsoleColor.Black : ConsoleColor.White;
        }

        // Getter
        public List<Item> PlayerInventory { get => _inventory; set => _inventory = value; }
    }
}