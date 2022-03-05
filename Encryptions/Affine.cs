using SimpleEncryptions.Constants;
using SimpleEncryptions.Encryptions.Interfaces;
using SimpleEncryptions.Helpers;
using SimpleEncryptions.Validation;
using System.Text;

namespace SimpleEncryptions.Encryptions
{
    public class Affine : IEncryption
    {
        public bool Decrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? decryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                decryptedString = String.Empty;
                return false;
            }

            StringBuilder decryptedStringBuilder = new StringBuilder();

            foreach (char letter in inputText)
            {
                int charNumber = alphabet.First(pair => pair.Value == letter).Key;

                int a1 = alphabet.First(pair => pair.Value == key[0]).Key;
                int a2 = alphabet.First(pair => pair.Value == key[1]).Key;

                int newCharIndex = (charNumber - a2) * HelperMath.Invert(a1, alphabet.Count) % alphabet.Count;

                while (newCharIndex < 0)
                {
                    newCharIndex += alphabet.Count;
                }

                char encChar = alphabet[newCharIndex];
                decryptedStringBuilder.Append(encChar);
            }
            
            decryptedString = decryptedStringBuilder.ToString();
            return true;
        }

        public bool Encrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? encryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                encryptedString = String.Empty;
                return false;
            }
                

            StringBuilder encryptedStringBuilder = new StringBuilder();


            foreach(char letter in inputText)
            {
                int charNumber = alphabet.First(pair => pair.Value == letter).Key;

                int a1 = alphabet.First(pair => pair.Value == key[0]).Key;
                int a2 = alphabet.First(pair => pair.Value == key[1]).Key;

                int newCharIndex = (a1 * charNumber + a2) % alphabet.Count;
                char encChar = alphabet[newCharIndex];
                encryptedStringBuilder.Append(encChar);
            }

            encryptedString = encryptedStringBuilder.ToString();
            return true;
        }

        public bool ValidateKey(string key, Dictionary<int, char> alphabet)
        {
            if (key.Length != 2)
            {
                Console.WriteLine(UIConstants.KeyHasWrongLength + " expected: 2, but got " + key.Length);
                return false;
            }
        
            if(Validator.CheckIfFromAlphabet(key, alphabet) is null)
            {
                Console.WriteLine(UIConstants.KeyIsNotFromAlphabet);
                return false;
            }
                

            int k1 = alphabet.First(pair => pair.Value == key[0]).Key;
            int k2 = alphabet.First(pair => pair.Value == key[1]).Key;

            int gcd = HelperMath.GCD(k1, alphabet.Count);

            if (gcd != 1)
            {
                Console.WriteLine("GDC is not 1");
                return false;
            }
                
            return true;
        }
    }
}
