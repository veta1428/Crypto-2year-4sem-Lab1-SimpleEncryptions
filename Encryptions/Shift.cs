using SimpleEncryptions.Constants;
using SimpleEncryptions.Encryptions.Interfaces;
using SimpleEncryptions.Validation;
using System.Text;

namespace SimpleEncryptions.Encryptions
{
    public class Shift : IEncryption
    {
        public bool Decrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? decryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                decryptedString = string.Empty;
                return false;
            }
                

            int shift = alphabet.First(x => x.Value == key[0]).Key;
            StringBuilder decryptedStringBuilder = new StringBuilder();

            int module = alphabet.Count;

            foreach (char letter in inputText)
            {
                int newIndex = alphabet.First(x => x.Value == letter).Key - shift;
                if (newIndex < 0)
                {
                    newIndex += module;
                }
                decryptedStringBuilder.Append(alphabet[newIndex]);
            }

            decryptedString = decryptedStringBuilder.ToString();
            return true;
        }

        public bool Encrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? encryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                encryptedString = string.Empty;
                return false;
            }

            int shift = alphabet.First(x => x.Value == key[0]).Key;

            StringBuilder encryptedStringBuilder = new StringBuilder();

            int module = alphabet.Count;

            foreach (char letter in inputText)
            {
                int newIndex = alphabet.First(x => x.Value == letter).Key + shift;
                newIndex %= module;
                
                encryptedStringBuilder.Append(alphabet[newIndex]);
            }
            encryptedString = encryptedStringBuilder.ToString();
            return true;
        }

        public bool ValidateKey(string key, Dictionary<int, char> alphabet)
        {
            if (key.Length != 1)
            {
                Console.WriteLine(UIConstants.KeyHasWrongLength + " expected 1, but got " + key.Length);
                return false;
            }

            if (Validator.CheckIfFromAlphabet(key, alphabet) is null)
            {
                Console.WriteLine(UIConstants.KeyIsNotFromAlphabet);
                return false;
            }

            return true;
        }     
    }
}
