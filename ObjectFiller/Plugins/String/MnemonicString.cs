using System;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// This randomizer plugin generates words which can be talked naturally.
    /// It always takes one vocal after a consonant. This follow up to words like: buwizalo
    /// </summary>
    public class MnemonicString : IRandomizerPlugin<string>
    {
        private readonly int _wordCount;
        private readonly int _wordMinLength;
        private readonly int _wordMaxLength;

        public MnemonicString(int wordCount)
            : this(wordCount, 3, 15)
        {

        }

        public MnemonicString(int wordCount, int wordMinLength, int wordMaxLength)
        {
            _wordCount = wordCount;
            _wordMinLength = wordMinLength;
            _wordMaxLength = wordMaxLength;
        }

        public string GetValue()
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            char[] consonants = { 'w', 'r', 't', 'z', 'p', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'c', 'v', 'b', 'n', 'm' };
            int wordLength = Random.Next(_wordMinLength, _wordMaxLength);

            string result = string.Empty;

            for (int i = 0; i < _wordCount; i++)
            {
                string currentWord = null;

                bool nextIsVowel = Random.Next(0, 1) == 1;

                bool upperLetter = i == 0 || Random.Next(0, 2) == 1;

                for (int j = 0; j < wordLength; j++)
                {
                    char currentChar;
                    if (nextIsVowel)
                    {
                        currentChar = vowels[Random.Next(0, vowels.Length)];
                    }
                    else
                    {
                        currentChar = consonants[Random.Next(0, consonants.Length)];
                    }


                    currentWord += upperLetter ? char.ToUpper(currentChar) : currentChar;
                    upperLetter = false;
                    nextIsVowel = !nextIsVowel;
                }
                result += currentWord + " ";
            }
            return result.Trim();
        }
    }
}