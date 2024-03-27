using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigSwordRPG_C_
{
    public enum actionType
    {
        ATT = 1,
        HEAL,
        CAPA,
        BUFF,
        ESCAPED
    }

    enum ZoneAction
    {
        Unique = 1,
        Near,
        All
    }

    public class Abilities
    {
        public Abilities() { }
        ~Abilities() { }

        //Champ
        private string _name;
        private Enum _type; 
        private float _damage;
        private float _heal;
        private float _speedUp;
        private int _cooldown;
        private float _cost;
        private Enum _zone;


        //Property
        public string Name { get => _name; set => _name = value; }
        public Enum Type { get => _type; set => _type = value; }
        public float Damage { get => _damage; set => _damage = value; }
        public int Cooldown { get => _cooldown; set => _cooldown = value; }
        public float Cost { get => _cost; set => _cost = value; }
        public float Heal { get => _heal; set => _heal = value; }
        public Enum Zone { get => _zone; set => _zone = value; }
        public float SpeedUp { get => _speedUp; set => _speedUp = value; }


}

    class CreateListAbilities
    {
        //return la list des abilities du jeux.

        private Dictionary<string, Abilities> _abilitiesList = CreateAbilities();

        public Dictionary<string, Abilities> AbilitiesList { get => _abilitiesList;}

        private static Dictionary<string, Abilities> CreateAbilities()
        {
            
            string filePath = "../../../Game/Stat/AbilitiesStat.csv";
            Dictionary<string, Abilities> abilities = new Dictionary<string, Abilities>(StringComparer.OrdinalIgnoreCase);
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] abilitiesData = sr.ReadLine().Split(',');

                        Abilities ability = new Abilities
                        {
                            Name = abilitiesData[0],
                            Type = (actionType)Enum.Parse(typeof(actionType), abilitiesData[1]),
                            Damage = float.Parse(abilitiesData[2].Replace(".", ",")),
                            Heal = float.Parse(abilitiesData[3].Replace(".", ",")),
                            SpeedUp = float.Parse(abilitiesData[4].Replace(".", ",")),
                            Cooldown = int.Parse(abilitiesData[5]),
                            Cost = float.Parse(abilitiesData[6].Replace(".", ",")),
                            Zone = (ZoneAction)Enum.Parse(typeof(ZoneAction), abilitiesData[7]),


                        };
                        abilities.Add(ability.Name, ability);
                    }
                    return abilities;
                }
            }
            else
            {
                throw new FileNotFoundException("Fichier " + filePath + " entrouvable");
            }
        }
    }
}