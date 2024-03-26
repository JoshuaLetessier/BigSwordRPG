using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


enum EquipementType
{
    Plasma = 1,
    Antimatiere,
    Nucleaire,
    Bioelectrique
}
namespace BigSwordRPG_C_.Game
{
    public class Equipement
    {
        private string _name;
        private Enum _type;
        private int efficciencyMin;
        private int efficciencyMax;




        public Equipement(string name, Enum type, int efficciencyMin, int efficciencyMax)
        {
            Name = name;
            Type = type;
            this.EfficciencyMin = efficciencyMin;
            this.EfficciencyMax = efficciencyMax;
        }

        public string Name { get => _name; set => _name = value; }
        public Enum Type { get => _type; set => _type = value; }
        public int EfficciencyMin { get => efficciencyMin; set => efficciencyMin = value; }
        public int EfficciencyMax { get => efficciencyMax; set => efficciencyMax = value; }

    }

    public class CreateEquipement
    {

        private string _name;
        private Enum _type;
        private int efficciencyMin;
        private int efficciencyMax;

        public Dictionary<string, Equipement> CreateDictionaryEquipement() {

            Dictionary<string, Equipement> equipements = new Dictionary<string, Equipement>();

            string filePath = "../../../Game/Stat/EquipementStat.csv";

            if(File.Exists(filePath))
            {
                using(StreamReader streamReader = new StreamReader(filePath))
                {
                    while(!streamReader.EndOfStream)
                    {
                        string[] line = streamReader.ReadLine().Split(',');

                        Equipement equipement = new Equipement(_name, _type, efficciencyMin, efficciencyMax)
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
