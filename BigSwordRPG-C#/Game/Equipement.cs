using BigSwordRPG.Game;
using System.Diagnostics;

public enum EquipementType
{
    Plasma = 1,
    Antimatiere,
    Nucleaire,
    Bioelectrique
}

namespace BigSwordRPG_C_.Game
{
    public class Equipement : Item
    {
        private EquipementType _type;
        private int efficciencyMin;
        private int efficciencyMax;

        public Equipement(string name, int value, int _rarety, int efficciencyMin, int efficciencyMax, EquipementType type) : base(name, value, _rarety)
        {

            Type = type;
            this.EfficciencyMin = efficciencyMin;
            this.EfficciencyMax = efficciencyMax;

            value = RandomBonusEquipement(EfficciencyMin, EfficciencyMax);
        }

        public EquipementType Type { get => _type; set => _type = value; }
        public int EfficciencyMin { get => efficciencyMin; set => efficciencyMin = value; }
        public int EfficciencyMax { get => efficciencyMax; set => efficciencyMax = value; }

        public int typeHiarachy(Equipement charachaterVise, Equipement characheterPlay)
        {
            if (characheterPlay.Type == charachaterVise.Type)
            {
                return 0;
            }
            else if (characheterPlay.Type == EquipementType.Plasma && charachaterVise.Type == EquipementType.Antimatiere)
            {
                return Value;
            }
            else if (characheterPlay.Type == EquipementType.Antimatiere && charachaterVise.Type == EquipementType.Nucleaire)
            {
                return Value;
            }
            else if (characheterPlay.Type == EquipementType.Nucleaire && charachaterVise.Type == EquipementType.Plasma)
            {
                return Value;
            }
            else if (characheterPlay.Type == EquipementType.Bioelectrique)
            {
                return Value;
            }
            else
            {
                throw new Exception("Aucun type d'Equipement reconnue");
            }

        }

        private int RandomBonusEquipement(int efficciencyMin, int efficciencyMax)
        {
            Random random = new Random();
            return random.Next(efficciencyMin, efficciencyMax); ;
        }

        public void Used(List<Hero> heroes)
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
                    if ((this.Name == "Defibrilateur Nanite" || this.Name == "Stimulateur Neuro-Electrique") && heroes[i].Name == "Seraph")
                    {
                        heroes[i].Equipements.Add(this.Name, this);
                        break;
                    }
                    else if ((this.Name == "Defibrilateur Nanite" || this.Name == "Stimulateur Neuro-Electrique") && heroes[i].Name != "Seraph")
                    {
                        break;
                    }
                    else
                        heroes[i].Equipements.Add(this.Name, this);
                }
            }
        }
        private static void ChangeLineColor(bool shouldHighlight)
        {
            Console.BackgroundColor = shouldHighlight ? ConsoleColor.White : ConsoleColor.Black;
            Console.ForegroundColor = shouldHighlight ? ConsoleColor.Black : ConsoleColor.White;
        }
    }



    public class CreateEquipement
    {

        private string _name;
        private EquipementType _type;
        private int efficciencyMin;
        private int efficciencyMax;
        private int rarety;

        private int value;

        public Dictionary<string, Equipement> CreateDictionaryEquipement()
        {

            Dictionary<string, Equipement> equipements = new Dictionary<string, Equipement>();


            #if DEBUG
                const string filePath = "../../../Game/Stat/EquipementStat.csv"; ;
            #else
                const string filePath = "./Data/Stat/EquipementStat.csv";
            #endif



            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] line = streamReader.ReadLine().Split(',');

                        Equipement equipement = new Equipement(_name, value, rarety, efficciencyMin, efficciencyMax, _type)
                        {
                            Name = line[0],
                            Type = (EquipementType)Enum.Parse(typeof(EquipementType), line[1]),
                            EfficciencyMin = int.Parse(line[2]),
                            EfficciencyMax = int.Parse(line[3])
                        };
                        equipements.Add(line[0], equipement);
                    }
                    return equipements;
                }
            }
            else
            {
                throw new FileNotFoundException("Fichier " + filePath + " entrouvable");
            }
        }
    }
}
