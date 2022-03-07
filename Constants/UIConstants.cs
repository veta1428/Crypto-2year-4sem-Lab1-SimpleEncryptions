using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEncryptions.Constants
{
    public static class UIConstants
    {
        public const string EndLine = "\n";
        public const string NotEnoughArgs = "Not enough arguments for this command.";
        public const string ArgsList = "[enc|dec] [shift|affine|simpleChange|Hill|change|vizhiner] [<sourceFilePath>] [<keyFilePath>] [<destinationKeyFilePath>] [<alphabetFilePath>]?";
        public const string Manual = "This util can encrypt or decrypt text within specified key.";
        public const string InputFileNotFound = "Input file was not found:";
        public const string OutputFileNotFound = "Output file was not found:";
        public const string KeyFileNotFound = "File with key was not found:";
        public const string AlphabetFileNotFound = "Alphabet file was not found:";
        public const char WhiteSpace = ' ';
        public const string WrongAlphabet = "Wrong alphabet. It should consist of unique characters.";
        public const string InputTextWrong = "Input file is empty or from other alphabet.";
        public const string KeyIsNotFromAlphabet = "Error: Key is not from alphabet.";
        public const string KeyHasWrongLength = "Error: Key has wrong length.";
        public const string KeyCharactersNotUnique = "Key characters are not unique!";
    }
}
