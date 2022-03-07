using SimpleEncryptions.Constants;
using SimpleEncryptions.Encryptions.Interfaces;
using SimpleEncryptions.Helpers;
using SimpleEncryptions.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions.Encryptions
{
    public class Hill : IEncryption
    {
        public bool Decrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? decryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                decryptedString = string.Empty;
                return false;
            }

            if (inputText.Length % 2 == 1)
            {
                decryptedString = string.Empty;
                Console.WriteLine("Input text length should devide by 2!");
                return false;
            }

            int a11 = alphabet.First(x => x.Value == key[0]).Key;
            int a12 = alphabet.First(x => x.Value == key[1]).Key;
            int a21 = alphabet.First(x => x.Value == key[2]).Key;
            int a22 = alphabet.First(x => x.Value == key[3]).Key;
            int m = alphabet.Count;
            int det = a11 * a22 - a12 * a21;
            while (det < 0)
            {
                det += m;
            }


            int detInvert = HelperMath.Invert(det, alphabet.Count);

            int[] reversed = new int[4] { (a22*detInvert) % m, (-a12 * detInvert) % m, (-a21 * detInvert) % m, (a11 * detInvert) % m};

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < inputText.Length - 1; i += 2)
            {
                int firstCharIndex = alphabet.First(x => x.Value == inputText[i]).Key;
                int secondCharIndex = alphabet.First(x => x.Value == inputText[i + 1]).Key;

                int newIndexfirst = (firstCharIndex * reversed[0] + secondCharIndex * reversed[2]) % alphabet.Count;
                int newIndexsecond = (firstCharIndex * reversed[1] + secondCharIndex * reversed[3]) % alphabet.Count;

                while (newIndexfirst < 0)
                {
                    newIndexfirst += m;
                }
                while (newIndexsecond < 0)
                {
                    newIndexsecond += m;
                }

                char newChar1 = alphabet[newIndexfirst];
                char newChar2 = alphabet[newIndexsecond];

                builder.Append(newChar1);
                builder.Append(newChar2);
            }

            decryptedString = builder.ToString();
            return true;
        }

        public bool Encrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? encryptedString)
        {
            if (!ValidateKey(key, alphabet))
            {
                encryptedString = string.Empty;
                return false;
            }
            if(inputText.Length % 2 == 1)
                inputText = (new StringBuilder(inputText)).Append(alphabet[0]).ToString();

            int a11 = alphabet.First(x => x.Value == key[0]).Key;
            int a12 = alphabet.First(x => x.Value == key[1]).Key;
            int a21 = alphabet.First(x => x.Value == key[2]).Key;
            int a22 = alphabet.First(x => x.Value == key[3]).Key;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < inputText.Length - 1; i+=2)
            {
                int firstCharIndex = alphabet.First(x => x.Value == inputText[i]).Key;
                int secondCharIndex = alphabet.First(x => x.Value == inputText[i + 1]).Key;

                char newChar1 = alphabet[(firstCharIndex * a11 + secondCharIndex * a21) % alphabet.Count];
                char newChar2 = alphabet[(firstCharIndex * a12 + secondCharIndex * a22) % alphabet.Count];

                builder.Append(newChar1);
                builder.Append(newChar2);
            }
            
            encryptedString = builder.ToString();
            return true;
        }

        public bool ValidateKey(string key, Dictionary<int, char> alphabet)
        {
            if (key.Length != 4)
            {
                Console.WriteLine(UIConstants.KeyHasWrongLength + " expected: 4, but got " + key.Length);
;                return false;
            }

            if (Validator.CheckIfFromAlphabet(key, alphabet) is null)
            {
                Console.WriteLine(UIConstants.KeyIsNotFromAlphabet);
                return false;
            }

            int a11 = alphabet.First(x => x.Value == key[0]).Key;
            int a12 = alphabet.First(x => x.Value == key[1]).Key;
            int a21 = alphabet.First(x => x.Value == key[2]).Key;
            int a22 = alphabet.First(x => x.Value == key[3]).Key;

            int det = a11 * a22 - a12 * a21;
            if (det % alphabet.Count == 0)
            {
                Console.WriteLine("Error: (det, M) == 0");
                return false;
            }

            if (HelperMath.GCD(Math.Abs(det), alphabet.Count) != 1)
            {
                Console.WriteLine("Error: GCD(det, M) != 1");
                return false;
            }

            return true;
        }
    }
}
