﻿using BigSwordRPG.Assets;
using BigSwordRPG_C_;

using BigSwordRPG.Utils;
using BigSwordRPG.Game;
using BigSwordRPG_C_.Utils;

namespace BigSwordRPG
{
    internal class Program
    {
        
        private MenuScene testMenu;
        private CreateHero testCreateHeros;
        private CreateEnnemy createEnnemy;
        private SaveManager saveManager;
        private FightScene fightScene;
        private Player player;

        public Program()
        {
           

            testMenu = new MenuScene();
            testCreateHeros = new CreateHero();
            createEnnemy = new CreateEnnemy();
            saveManager = new SaveManager();
            player = new Player();

            Dictionary<string, Hero> heroes = testCreateHeros.CreateDictionaryHero();
            Dictionary<string, Ennemy> ennemies = createEnnemy.CreateDictionaryEnnemies();

            fightScene = new FightScene(player.Heroes, ennemies, player);
        }

        ~Program()
        {
            //throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth - 50, Console.LargestWindowHeight - 10);
            //LargestWindowWidth = 240
            //LargestWindowHeight = 63

            Program p = new Program();

            // p.testMenu.Draw();            

            //Console.Read();


            p.fightScene.Update();



            /*Dictionary<string, Hero> heroes = p.testCreateHeros.CreateDictionaryHero();
            Dictionary<string, Ennemy> ennemies = p.createEnnemy.CreateDictionaryEnnemies();



            foreach (KeyValuePair<string, Hero> kvp in heroes)
            {
                string heroName = kvp.Key;
                Hero hero = kvp.Value;

                Console.WriteLine($"Hero: {heroName}, Health: {hero.Health}");
                Console.WriteLine("Abilities:");

                foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
                {
                    string abilityName = abilityKvp.Key;
                    Abilities ability = abilityKvp.Value;

                    Console.WriteLine($"- {abilityName}");
                }

                Console.WriteLine();
            }

            foreach (KeyValuePair<string, Ennemy> kvp in ennemies)
            {
                string heroName = kvp.Key;
                Ennemy hero = kvp.Value;

                Console.WriteLine($"Ennemy: {heroName}, Health: {hero.Health}");
                Console.WriteLine("Abilities:");

                foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
                {
                    string abilityName = abilityKvp.Key;
                    Abilities ability = abilityKvp.Value;

                    Console.WriteLine($"- {abilityName}");
                }

                Console.WriteLine();
            }*/
            



            //p.saveManager.Save(heroes);

            /*

            foreach (KeyValuePair<string, Hero> kvp in heroes)
            {
                string heroName = kvp.Key;
                Hero hero = kvp.Value;

                Console.WriteLine($"Hero: {heroName}, Health: {hero.Health}");
                Console.WriteLine("Abilities:");

                foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
                {
                    string abilityName = abilityKvp.Key;
                    Abilities ability = abilityKvp.Value;

                    Console.WriteLine($"- {abilityName}");
                }

                Console.WriteLine();
            }

            foreach (KeyValuePair<string, Ennemy> kvp in ennemies)
            {
                string heroName = kvp.Key;
                Ennemy hero = kvp.Value;

                Console.WriteLine($"Ennemy: {heroName}, Health: {hero.Health}");
                Console.WriteLine("Abilities:");

                foreach (KeyValuePair<string, Abilities> abilityKvp in hero.CAbilities)
                {
                    string abilityName = abilityKvp.Key;
                    Abilities ability = abilityKvp.Value;

                    Console.WriteLine($"- {abilityName}");
                }

                Console.WriteLine();



            }*/
        }
    }
}

