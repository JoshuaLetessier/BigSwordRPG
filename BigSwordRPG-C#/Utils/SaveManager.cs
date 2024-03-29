using BigSwordRPG.Game;
using BigSwordRPG.Utils;
using BigSwordRPG.Utils.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigSwordRPG_C_.Utils
{
    internal class SaveManager
    {     
#if DEBUG
        const string SAVE_PATH = "../../../Asset/Image/";
#else
        const string SAVE_PATH = "./Data/Assets/Textures/";
#endif
        const string SAVE_EXTENSION = ".csv";
        private string filePath = $"{SAVE_PATH}Save.csv";

        public void Save(Dictionary<string, Hero> _heroes, List<Item> _items, int[] pos)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            FileInfo fileInfo = new FileInfo(filePath);
            using (StreamWriter streamWriter = fileInfo.CreateText())
            {
                streamWriter.WriteLine(pos[0].ToString() + "," + pos[1].ToString() + "+");
              
                for (int i = 0; i < _heroes.Count; i++)
                {
                    streamWriter.WriteLine(_heroes.ElementAt(i).Value.Name +","+ _heroes.ElementAt(i).Value.Health + "," + _heroes.ElementAt(i).Value.Level + "," + _heroes.ElementAt(i).Value.PM + "," + _heroes.ElementAt(i).Value.IsDead + "+");
                }

                for(int i = 0; i < _items.Count ; i++)
                {
                    streamWriter.WriteLine(_items[i].Name + "," + _items[i].Value + "+");
                }
            }
        }

        public void Load(Dictionary<string, Hero> _heroes, List<Item> _items) 
        { 
            if(File.Exists(filePath))
            {
                using(StreamReader streamReader = new StreamReader(filePath))
                {
                    
                    //StreamReader srSave = new StreamReader("./Save/Save.csv");
                    StreamReader srSave = new StreamReader($"{SAVE_PATH}Save.csv");
                    string[] data = srSave.ReadToEnd().Replace("\r\n", "").Split("+");

                    string[] pos = data[0].Split(",");
                    int[] position = new int[2] { int.Parse(pos[0]), int.Parse(pos[1]) };

                    GameManager.Instance.Player.Position = position;

                    for (int i = 0;i < _heroes.Count;i++) 
                    {
                        string[] heroes = data[i+1].Split(",");
                        _heroes.ElementAt(i).Value.Name = heroes[0];
                        _heroes.ElementAt(i).Value.Health = int.Parse(heroes[1]);
                        _heroes.ElementAt(i).Value.Level = int.Parse(heroes[2]);
                        _heroes.ElementAt(i).Value.PM = int.Parse(heroes[3]);
                        _heroes.ElementAt(i).Value.IsDead = bool.Parse(heroes[4]);
                    }

                    for (int i = 0; i < _items.Count; i++)
                        {
                        _items.ElementAt(i).Name = data[0];
                        _items.ElementAt(i).Value = int.Parse(data[1]);
                    }
                    srSave.Dispose();
                    
                }
            }
        }
    }
}
