using System.Text;

//Izvor stranice na osnovu kojeg je prepravljen algoritam iz JavaScript - a u C#:https://cipher-tools.appspot.com/

namespace CryptoAlgLib
{
    public class Myszkowski
    {
        public string MyszkEncrypt(string key, string ptws)
        {
            string pt = ptws.Replace(" ", "");

            string alphabet = MakeAlphabet(key);
            StringBuilder ct = new StringBuilder();
            for (int i = 0; i < alphabet.Length; i++)
            {
                for (int row = 0; row * key.Length < pt.Length; row++)
                {
                    for (int col = 0; col < key.Length; col++)
                    {
                        if (row * key.Length + col >= pt.Length)
                            continue;
                        if (key[col] == alphabet[i])
                        {
                            ct.Append(pt[row * key.Length + col]);
                        }
                    }
                }
            }
            return ct.ToString();
        }
        public string MakeAlphabet(string str)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder alphabet = new StringBuilder();
            for (int let = 0; let < letters.Length; let++)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (letters[let] == str[i])
                    {
                        alphabet.Append(letters[let]);
                        break;
                    }
                }
            }
            return alphabet.ToString();
        }
    }
}