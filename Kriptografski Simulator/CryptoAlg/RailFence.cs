using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAlgLib
{
    //Izvor sa interneta od Rail Fence algoritma: https://www.geeksforgeeks.org/rail-fence-cipher-encryption-decryption/
    public class RailFence
    {   
        public static string EncryptRailFence(string text, int key)
        {

            // create the matrix to cipher plain text
            // key = rows, length(text) = columns
            char[,] rail = new char[key, text.Length];

            // filling the rail matrix to distinguish filled
            // spaces from blank ones
            for (int i = 0; i < key; i++)
                for (int j = 0; j < text.Length; j++)
                    rail[i, j] = '\n';

            bool dirDown = false;
            int row = 0, col = 0;

            for (int i = 0; i < text.Length; i++)
            {
                // check the direction of flow
                // reverse the direction if we've just
                // filled the top or bottom rail
                if (row == 0 || row == key - 1)
                    dirDown = !dirDown;

                // fill the corresponding alphabet
                rail[row, col++] = text[i];

                // find the next row using direction flag
                if (dirDown)
                    row++;
                else
                    row--;
            }

            // now we can construct the cipher using the rail
            // matrix
            string result = "";
            for (int i = 0; i < key; i++)
                for (int j = 0; j < text.Length; j++)
                    if (rail[i, j] != '\n')
                        result += rail[i, j];

            return result;
        }
    }
}
