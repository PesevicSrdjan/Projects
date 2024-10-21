using System.Text.Json;

namespace DatabaseLib
{
    public class Database
    {
        private string filePath;

        public Database(string filePath)
        {
            this.filePath = filePath;
        }
        public void SerializeDictionary(Dictionary<string, Tuple<string, string>> dictionary)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }

                string json = JsonSerializer.Serialize(dictionary);

                // Čuvanje JSON-a u fajlu
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Došlo je do problema pri serijalizaciji Dictionary-ja!");
            }
        }
        public Dictionary<string, Tuple<string, string>> DeserializeDictionary(string json)
        {
            Dictionary<string, Tuple<string, string>> myDictionary = new Dictionary<string, Tuple<string, string>>();

            try
            {
                    //string json = File.ReadAllText(filePath);
                    myDictionary = JsonSerializer.Deserialize<Dictionary<string, Tuple<string, string>>>(json);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Došlo je do problema pri deserijalizaciji Dictionary-ja!");
            }

            return myDictionary;
        }

        public bool IsEmpty(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (fileStream.Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}