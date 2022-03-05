using SimpleEncryptions.Constants;
using SimpleEncryptions.Encryptions.Interfaces;
using SimpleEncryptions.Validation;
using System.Text;

namespace SimpleEncryptions.Encryptions
{
    public class Vizhiner : IEncryption
    {
        public bool Decrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? decryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                decryptedString = String.Empty;
                return false;
            }

            StringBuilder longKey = new StringBuilder();
            for (int i = 0; i < inputText.Length; i++)
            {
                longKey.Append(key[i % key.Length]);
            }

            StringBuilder str = new StringBuilder();
            int counter = 0;
            foreach (char letter in inputText)
            {
                int keyIndex = alphabet.First(x => x.Value == longKey[counter]).Key;
                int inputStringIndex = alphabet.First(x => x.Value == inputText[counter]).Key;

                int newIndex = -keyIndex + inputStringIndex;
                while (newIndex < 0)
                {
                    newIndex += alphabet.Count;
                }

                char newChar = alphabet[newIndex % alphabet.Count];
                str.Append(newChar);
                counter++;
            }

            decryptedString = str.ToString();
            return true;
        }

        public bool Encrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? encryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                encryptedString = String.Empty;
                return false;
            }

            StringBuilder longKey = new StringBuilder();
            for (int i = 0; i < inputText.Length; i++)
            {
                longKey.Append(key[i % key.Length]);
            }
            string keyString = longKey.ToString();
            StringBuilder str = new StringBuilder();
            int counter = 0;
            foreach (char letter in inputText)
            {
                int keyIndex = alphabet.First(x => x.Value == keyString[counter]).Key;
                int inputStringIndex = alphabet.First(x => x.Value == letter).Key;

                char newChar = alphabet[(keyIndex + inputStringIndex) % alphabet.Count];
                str.Append(newChar);
                counter++;
            }

            encryptedString = str.ToString();
            return true;
        }

        public bool ValidateKey(string key, Dictionary<int, char> alphabet)
        {
            if (Validator.CheckIfFromAlphabet(key, alphabet) is null)
            {
                Console.WriteLine(UIConstants.KeyIsNotFromAlphabet);
                return false;
            }
            return true;
        }
    }
}
