using SimpleEncryptions.Constants;
using SimpleEncryptions.Encryptions.Interfaces;
using SimpleEncryptions.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions.Encryptions
{
    public struct ForSort
    {
        public int PrimaryIndex { get; set; }
        public int IndexInAlphabet { get; set; } = 0;
        public int IndexSorted { get; set; } = 0;
    }

    public class Change : IEncryption
    {
        

        public bool Decrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? decryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                decryptedString = string.Empty;
                return false;
            }

            int[] change = GetChange(key, alphabet);

            int[] revertChange = GetInvertedChange(change);

            int toAdd = inputText.Length % key.Length;

            if (toAdd != 0)
            {
                decryptedString = string.Empty;
                Console.WriteLine("Input text length should devide by key length");
                return false;
            }

            StringBuilder result = new StringBuilder(inputText);
            Console.WriteLine(result.ToString());

            for (int i = 0; i < inputText.Length; i += key.Length)
            {
                for (int k = 0; k < key.Length; k++)
                {
                    char charToPut = inputText[k + i];
                    int indexToPut = revertChange[k] + i;
                    result[indexToPut] = charToPut;
                }
            }

            Console.WriteLine(result.ToString());
            decryptedString = result.ToString();
            return true;
        }

        public int[] GetChange(string key, Dictionary<int, char> alphabet)
        {
            IEnumerable<char> collection = key.Cast<char>();
            List<ForSort> sortingList = new List<ForSort>();
            int counter = 0;
            foreach (char letter in key)
            {
                sortingList.Add(new ForSort() { IndexInAlphabet = alphabet.First(x => x.Value == letter).Key, PrimaryIndex = counter });
                counter++;
            }

            sortingList.Sort((x, y) => x.IndexInAlphabet > y.IndexInAlphabet ? 1 : -1);

            for (int i = 0; i < key.Length; i++)
            {
                sortingList[i] = new ForSort() { IndexSorted = i, IndexInAlphabet = sortingList[i].IndexInAlphabet, PrimaryIndex = sortingList[i].PrimaryIndex };
            }

            sortingList.Sort((x, y) => x.PrimaryIndex > y.PrimaryIndex ? 1 : -1);

            int[] change = new int[key.Length];

            for (int i = 0; i < key.Length; i++)
            {
                change[i] = sortingList[i].IndexSorted;
            }

            return change;
        }

        public int[] GetInvertedChange(int[] change)
        {
            List<ForSort> sortingList = new List<ForSort>();

            for (int i = 0;i < change.Length; i++)
            {
                sortingList.Add(new ForSort() { IndexSorted = change[i], PrimaryIndex = i});
            }

            sortingList.Sort((x, y) => x.IndexSorted > y.IndexSorted ? 1 : -1);

            int[] invertedChage = new int[sortingList.Count];

            for (int i = 0; i < sortingList.Count; i++)
            {
                invertedChage[i] = sortingList[i].PrimaryIndex;
            }

            return invertedChage;
        }

        public bool Encrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? encryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                
                encryptedString = string.Empty;
                return false;
            }

            int[] change = GetChange(key, alphabet);

            int toAdd = inputText.Length % key.Length;

            if (toAdd != 0)
            {
                StringBuilder inpuStringBuilder = new StringBuilder(inputText);

                for (int i = 0; i < key.Length - toAdd; i++)
                {
                    inpuStringBuilder.Append(alphabet[0]);
                }

                inputText = inpuStringBuilder.ToString();
            }
            /*for (int i = 0; i < key.Length; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            for (int i = 0; i < key.Length; i++)
            {
                Console.Write(change[i]);
            }*/
            //Console.WriteLine();
            StringBuilder result = new StringBuilder(inputText);
            Console.WriteLine(result.ToString());

            for (int i = 0; i < inputText.Length; i+=key.Length)
            {
                for (int k = 0; k < key.Length; k++)
                {
                    char charToPut = inputText[k + i];
                    int indexToPut = change[k] + i;
                    result[indexToPut] = charToPut;
                }
            }

            Console.WriteLine(result.ToString());
            encryptedString = result.ToString();
            return true;
        }

        public bool ValidateKey(string key, Dictionary<int, char> alphabet)
        {
            if (Validator.CheckIfFromAlphabet(key, alphabet) is null)
            {
                Console.WriteLine(UIConstants.KeyIsNotFromAlphabet);
                return false;
            }

            if (key.Length != key.Cast<char>().ToHashSet().Count)
            {
                Console.WriteLine(UIConstants.KeyCharactersNotUnique);
                return false;
            }

            return true;
        }
    }
}
