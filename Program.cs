using SimpleEncryptions.Constants;
using SimpleEncryptions.Encryptions;
using SimpleEncryptions.Helpers;
using SimpleEncryptions.Validation;
using System;

namespace SimpleEncryptions
{
    public class Program
    {
        public async static Task<int> Main(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0] == "/?" || args[0] == "/?.")
                {
                    Console.WriteLine(UIConstants.Manual + UIConstants.EndLine + UIConstants.ArgsList);
                    return 1;
                }
            }

            if (args.Length != 5 && args.Length != 6)
            {
                Console.WriteLine(UIConstants.NotEnoughArgs + UIConstants.EndLine + UIConstants.ArgsList + args.Length);
                return 1;
            }

            if (!File.Exists(args[2]))
            {
                Console.WriteLine(UIConstants.InputFileNotFound + UIConstants.WhiteSpace + args[2]);
                return 1;
            }

            if (!File.Exists(args[3]))
            {
                Console.WriteLine(UIConstants.KeyFileNotFound + UIConstants.WhiteSpace + args[3]);
                return 1;
            }

            string alphabetPath = PathConstants.DefaultAlphabetFilePath;

            if (args.Length == 6 && !File.Exists(args[5]))
            {
                Console.WriteLine(UIConstants.AlphabetFileNotFound + UIConstants.WhiteSpace + args[5]);
                return 1;
            }
            else if (args.Length == 6)
            {
                alphabetPath = args[5];
            }

            Dictionary<int, char>? alphabet = Validator.CheckAlphabet(await FileManager.ReadAsync(alphabetPath));

            if(alphabet is null)
            {
                Console.WriteLine(UIConstants.WrongAlphabet);
                return 1;
            }

            string? inputText = Validator.CheckIfFromAlphabet(await FileManager.ReadAsync(args[2]), alphabet);

            if (inputText is null)
            {
                Console.WriteLine(UIConstants.InputTextWrong);
                return 1;
            }

            bool successfulResult = false;

            switch (args[0].ToLower())
            {
                case OperationTypesConstants.Encrypt:
                    successfulResult = await Encrypt(args[1], args[2], args[3], args[4], alphabet);
                    break;
                case OperationTypesConstants.Decrypt:
                    successfulResult = await Decrypt(args[1], args[2], args[3], args[4], alphabet);
                    break;
                default:
                    break;
            }
            if (successfulResult == true)
            {
                Console.WriteLine("Operation succeeded.");
            }
            else
            {
                Console.WriteLine("Operation failed.");
            }
            return 0;
        }

        private static async Task<bool> Encrypt(
            string encryptionType,
            string sourceFilePath, 
            string keyFilePath, 
            string encryptedFilePath,
            Dictionary<int, char> alphabet)
        {
            switch (encryptionType)
            {
                case EncryptionTypesConstants.Shift:
                    {
                        return await HelperEncryptionMaster.EncryptMaster<Shift>(new Shift(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Affine:
                    {
                        return await HelperEncryptionMaster.EncryptMaster<Affine>(new Affine(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.SimpleChange:
                    {
                        return await HelperEncryptionMaster.EncryptMaster<SimpleChange>(new SimpleChange(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Hill:
                    {
                        return await HelperEncryptionMaster.EncryptMaster<Hill>(new Hill(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Vizhiner:
                    {
                        return await HelperEncryptionMaster.EncryptMaster<Vizhiner>(new Vizhiner(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Change:
                    {
                        return await HelperEncryptionMaster.EncryptMaster<Change>(new Change(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                default:
                    break;
            }
            return false;
        }

        private static async Task<bool> Decrypt(
            string encryptionType,
            string sourceFilePath,
            string keyFilePath,
            string encryptedFilePath,
            Dictionary<int, char> alphabet)
        {
            switch (encryptionType)
            {
                case EncryptionTypesConstants.Shift:
                    {
                        return await HelperEncryptionMaster.DecryptMaster<Shift>(new Shift(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Affine:
                    {
                        return await HelperEncryptionMaster.DecryptMaster<Affine>(new Affine(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.SimpleChange:
                    {
                        return await HelperEncryptionMaster.DecryptMaster<SimpleChange>(new SimpleChange(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Hill:
                    {
                        return await HelperEncryptionMaster.DecryptMaster<Hill>(new Hill(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Vizhiner:
                    {
                        return await HelperEncryptionMaster.DecryptMaster<Vizhiner>(new Vizhiner(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                case EncryptionTypesConstants.Change:
                    {
                        return await HelperEncryptionMaster.DecryptMaster<Change>(new Change(), sourceFilePath, keyFilePath, encryptedFilePath, alphabet);
                    }
                default:
                    break;
            }
            return false;
        }
    }
}