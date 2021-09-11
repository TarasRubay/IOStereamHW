using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace IOStereamHW
{
    class DataManager
    {
        public string Path { get; }
        public DataManager(string path) { Path = path; }
        public bool SaveDataBIN(SortedDictionary<DateTime, string> collectionEvent)
        {
            try
            {
            using (Stream stream = new FileStream(Path, FileMode.Create, FileAccess.Write))
            using (BinaryWriter binary = new BinaryWriter(stream))
                    try
                    {

                    foreach (var item in collectionEvent)
                    {
                    binary.Write(item.Key.Ticks.ToString());
                    binary.Write(item.Value);
                    }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        return false;
                    }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public SortedDictionary<DateTime, string> LoadDataBIN()
        {
            SortedDictionary<DateTime, string> collectionEvent = new SortedDictionary<DateTime, string>();
            try
            {

            using (Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(stream))
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    collectionEvent.Add(new DateTime(Convert.ToInt64(reader.ReadString())), reader.ReadString());
                }
            return collectionEvent;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;
            }

        }
        public List<DateTime> LoadDataTXT()
        {
            List<DateTime> dateTimes = new List<DateTime>();
            try
            {
                using (Stream stream = new FileStream(Path, FileMode.Open, FileAccess.Read))
                {
                    StreamReader reader = new StreamReader(stream, Encoding.ASCII);
                    string strDate = "";
                    while (!reader.EndOfStream)
                    {
                        strDate += reader.ReadLine();
                        strDate += " ";
                        stream.Seek(stream.Position + 1, SeekOrigin.Current);
                    }
                    string[] s = strDate.Split('.', ' ', '\r', '\n');
                    for (int i = 0; i < s.Length - 1; i += 3)
                    {
                        dateTimes.Add(new DateTime(
                            Convert.ToInt32(s[i + 2]),
                            Convert.ToInt32(s[i + 1]),
                            Convert.ToInt32(s[i])));
                    }
                    return dateTimes;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
