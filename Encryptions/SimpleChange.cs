using SimpleEncryptions.Constants;
using SimpleEncryptions.Encryptions.Interfaces;
using SimpleEncryptions.Validation;
using System.Text;

namespace SimpleEncryptions.Encryptions
{
    public class SimpleChange : IEncryption
    {
        public bool Decrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? decryptedString)
        {
            if(!ValidateKey(key, alphabet))
            {
                decryptedString = String.Empty;
                return false;   
            }
            StringBuilder stringBuilder = new StringBuilder();

            int counter = 0;
            foreach (char letter in inputText)
            {
                int indexInKey = key.IndexOf(letter);
                stringBuilder.Append(alphabet[indexInKey]);
                counter++;
            }
            decryptedString = stringBuilder.ToString();
            return true;
        }

        public bool Encrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? encryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                encryptedString = String.Empty;
                return false;
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char letter in inputText)
            {
                int indexLetter = alphabet.First(x => x.Value == letter).Key;
                stringBuilder.Append(key[indexLetter]);
            }
            encryptedString = stringBuilder.ToString();
            return true;
        }

        public bool ValidateKey(string key, Dictionary<int, char> alphabet)
        {
            if (key.Length != key.Cast<char>().ToHashSet().Count)
            {
                Console.WriteLine(UIConstants.KeyCharactersNotUnique);
                return false;
            }

            if (Validator.CheckIfFromAlphabet(key, alphabet) is null)
            {
                Console.WriteLine(UIConstants.KeyIsNotFromAlphabet);
                return false;
            }

            if (key.Length != alphabet.Count)
            {
                Console.WriteLine(UIConstants.KeyHasWrongLength + " expected: " + alphabet.Count + ", but got " + key.Length);
                return false;
            }

            return true;
        }
    }
}
