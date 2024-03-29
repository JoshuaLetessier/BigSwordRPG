using BigSwordRPG.Game;

namespace BigSwordRPG_C_.Utils
{
    public class SaveManager
    {
        private int[] position;

        private string filePath = "../../../Save/Save.csv";

        public int[] Position { get => position; set => position = value; }

        public void Save(Dictionary<string, Hero> _heroes, List<Item> _items)
        {


            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            FileInfo fileInfo = new FileInfo(filePath);
            using (StreamWriter streamWriter = fileInfo.CreateText())
            {
                streamWriter.WriteLine(Position + ",");

                for (int i = 0; i < _heroes.Count; i++)
                {
                    streamWriter.WriteLine(_heroes.ElementAt(i).Value.Name + "," + _heroes.ElementAt(i).Value.Health + "," + _heroes.ElementAt(i).Value.Level + "," + _heroes.ElementAt(i).Value.PM + "," + _heroes.ElementAt(i).Value.IsDead + ",");
                }

                for (int i = 0; i < _items.Count; i++)
                {
                    streamWriter.WriteLine(_items[i].Name + "," + _items[i].Value + ",");
                }
            }
        }

        public void Load(Dictionary<string, Hero> _heroes, List<Item> _items)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string[] data = streamReader.ReadLine().Split(",");

                        for (int i = 0; i < Position.Length; i++)
                        {
                            Position[i] = int.Parse(data[i]);
                        }

                        for (int i = 0; i < _heroes.Count; i++)
                        {
                            _heroes.ElementAt(i).Value.Name = data[0];
                            _heroes.ElementAt(i).Value.Health = int.Parse(data[1]);
                            _heroes.ElementAt(i).Value.Level = int.Parse(data[2]);
                            _heroes.ElementAt(i).Value.PM = int.Parse(data[3]);
                            _heroes.ElementAt(i).Value.IsDead = bool.Parse(data[4]);
                        }

                        for (int i = 0; i < _items.Count; i++)
                        {
                            _items.ElementAt(i).Name = data[0];
                            _items.ElementAt(i).Value = int.Parse(data[1]);
                        }
                    }
                }
            }
        }
    }
}
