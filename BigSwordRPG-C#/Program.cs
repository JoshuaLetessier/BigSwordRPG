using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG_C_;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace BigSwordRPG
{
    internal class Program
    {
        public Program()
        {
            //throw new NotImplementedException();
        }

        ~Program()
        {
            //throw new NotImplementedException();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            List<Hero> heroes = new List<Hero>();
            Program p = new Program();

            heroes = p.CreateHero();

            foreach (Hero hero in heroes)
            {
                Console.WriteLine($"Hero: {hero.Name}, Health: {hero.Health}");
                Console.WriteLine("Abilities:");
                foreach (Abilities ability in hero.CAbilities)
                {
                    Console.WriteLine($"- {ability.Name}");
                }
                Console.WriteLine();
            }
        }







        private string name;
        private int health;
        private int maxHealth;
        private int level;
        private float healthMultiplier;
        private float attMultiplier;
        private float healMultiplier;
        private int speed;
        private List<Abilities> abilities;
        private bool isDead;

        public List<Hero> CreateHero()//pour une new Game uniquement sinon on prend dans le fichier de save
        {
            List<Hero> heroes = new List<Hero>();

            string filePath = "../../../Game/Stat/HerosStat.csv";

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] heroData = sr.ReadLine().Split(',');

                        string stringHealthMultiplier = heroData[4].Replace("\"", "");
                        Console.WriteLine(stringHealthMultiplier);
                        string stringAttMultiplier = heroData[5].Replace("\"", "");
                        string stringHealMultiplier = heroData[6].Replace("\"", "");

                        Hero hero = new Hero(name, health, maxHealth, level, healthMultiplier, attMultiplier, healMultiplier, speed, abilities, isDead)
                        {
                            Name = heroData[0],
                            Health = int.Parse(heroData[1]),
                            MaxHealth = int.Parse(heroData[2]),
                            Level = int.Parse(heroData[3]),

                            HealthMultiplier = float.Parse(stringHealthMultiplier),
                           // AttMultiplier = float.Parse(stringAttMultiplier),
/*                            HealMultiplier = float.Parse(stringHealMultiplier),
                            Speed = int.Parse(heroData[7]),
                            CAbilities = CreateAbilities(),*/
                            IsDead = false,
                        };
                        heroes.Add(hero);
                    }
                    return heroes;

                }
            }
            else
            {
                Console.WriteLine("Fichier " + filePath + " entrouvable");
                return null;//excption
            }
        }
        private List<Abilities> CreateAbilities()
        {

            string filePath = "Game/Stat/AbilitiesStat.csv";
            List<Abilities> abilities = new List<Abilities>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] abilitiesData = sr.ReadLine().Split(',');

                        Abilities ability = new Abilities
                        {
                            Name = abilitiesData[0],
                            Type = int.Parse(abilitiesData[1]),
                            Damage = int.Parse(abilitiesData[2]),
                            Cooldown = int.Parse(abilitiesData[3]),
                            Cost = int.Parse(abilitiesData[4]),
                            Heal = int.Parse(abilitiesData[5]),
                            Zone = int.Parse(abilitiesData[6]),
                            SpeedUp = float.Parse(abilitiesData[7]),

                        };
                        abilities.Add(ability);
                    }
                    return abilities;
                }
            }
            else
            {
                Console.WriteLine("Fichier " + filePath + " entrouvable");
                return null;//excption
            }
        }
    }
}