using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions.Validation
{
    public static class Validator
    {
        public static Dictionary<int, char>? CheckAlphabet(string textFromFile)
        {
            Dictionary<int, char>? alphabet = new Dictionary<int, char>();
            
            int counter = 0;

            HashSet<char> unique = new HashSet<char>();

            foreach (char letter in textFromFile)
            {
                alphabet.Add(counter, letter);
                unique.Add(letter);
                counter++;
            }

            if (unique.Count != alphabet.Count)
            {
                alphabet = null;
            }

            return alphabet;
        }

        public static string? CheckIfFromAlphabet(string textFromFile, Dictionary<int, char> alphabet)
        {
            foreach (char letter in textFromFile)
            {
                if (!alphabet.ContainsValue(letter))
                {
                    return null;
                }
            }

            return textFromFile;
        }
    }
}
