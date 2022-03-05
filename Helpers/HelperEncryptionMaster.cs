using SimpleEncryptions.Encryptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions.Helpers
{
    public static class HelperEncryptionMaster
    {
        public static async Task<bool> DecryptMaster<T>(
            T encryptor, 
            string sourceFilePath,
            string keyFilePath,
            string encryptedFilePath,
            Dictionary<int, char> alphabet) where T: IEncryption
        {
            string textToDecrypt = await FileManager.ReadAsync(sourceFilePath);
            string key = await FileManager.ReadAsync(keyFilePath);

            bool success = encryptor.Decrypt(textToDecrypt, key, alphabet, out string? decryptedString);

            return success == true ? await FileManager.WriteAsync(decryptedString!, encryptedFilePath) : false;
        }

        public static async Task<bool> EncryptMaster<T>(
            T encryptor,
            string sourceFilePath,
            string keyFilePath,
            string encryptedFilePath,
            Dictionary<int, char> alphabet) where T : IEncryption
        {
            string textToEncrypt = await FileManager.ReadAsync(sourceFilePath);
            string key = await FileManager.ReadAsync(keyFilePath);

            bool success = encryptor.Encrypt(textToEncrypt, key, alphabet, out string? encryptedString);
            

            return success == true ? await FileManager.WriteAsync(encryptedString!, encryptedFilePath) : false;
        }
    }
}
