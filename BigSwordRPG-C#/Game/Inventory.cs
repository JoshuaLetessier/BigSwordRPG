using BigSwordRPG.Game;
using BigSwordRPG_C_.Game;
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
        private List<Equipement> _inventoryEquipement;
        
        public Inventory() 
        {
            // Define the initial size of the inventory
            _inventory = new List<Item>( new Item[10] );
            _inventoryEquipement = new List<Equipement>(10); 
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
               // newItem?.Destroy();
                // Possible add of a menu for exchange
            }
        }

        public void Store(Equipement newEquipement)
        {
            if (newEquipement != null && _inventory.Count != 10)
            {
                _inventory.Add(newEquipement);
                Console.WriteLine($"{newEquipement.Name} a été ajouter à votre inventaire !");
            }
            else
            {
                Console.WriteLine("L'inventaire est plein !");
                // newItem?.Destroy();
                // Possible add of a menu for exchange
            }
        }

        public void Open(List<Hero> heroes)
        {
            int indexInventoryType = 0;
            ConsoleKey keyPressed;
            do
            {
                List<String> value = new List<String>();
                value[0] = "Item";
                value[1] = "Equipements";
                foreach(String str in value)
                {
                    bool isSelected = str == value[indexInventoryType];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{value}");
                }

                keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.DownArrow && indexInventoryType + 1 < value.Count)
                {
                    indexInventoryType++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && indexInventoryType - 1 >= 0)
                {
                    indexInventoryType--;
                }

            } while (keyPressed != ConsoleKey.Enter);
            if (indexInventoryType == 0)
                UseEquipement(heroes);
            else
                UseItem(heroes);
                
        }

        private void UseEquipement(List<Hero> heroes)
        {

            int indexEquipement = 0;
            ConsoleKey keyPressed;

            do
            {
                foreach (Equipement equipement in _inventoryEquipement)
                {
                    bool isSelected = equipement == _inventoryEquipement[indexEquipement];
                    ChangeLineColor(isSelected);
                    Console.WriteLine($"{(isSelected ? "> " : "  ")}{equipement}");
                }

                keyPressed = Console.ReadKey().Key;

                if (keyPressed == ConsoleKey.DownArrow && indexEquipement + 1 < _inventory.Count)
                {
                    indexEquipement++;
                }
                else if (keyPressed == ConsoleKey.UpArrow && indexEquipement - 1 >= 0)
                {
                    indexEquipement--;
                }

            } while (keyPressed != ConsoleKey.Enter);
            _inventoryEquipement[indexEquipement].Used(heroes);
        }

        private void UseItem(List<Hero> heroes)
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

            _inventory[indexInventory].Use(heroes);
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