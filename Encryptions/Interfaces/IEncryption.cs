using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions.Encryptions.Interfaces
{
    public interface IEncryption
    {
        bool Encrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? encryptedString);
        bool Decrypt(string inputText, string key, Dictionary<int, char> alphabet, out string? decryptedString);
        bool ValidateKey(string key, Dictionary<int, char> alphabet);
    }
}
