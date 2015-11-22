// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MnemonicString.cs" company="Tynamix">
//  © 2015 by Roman Köhler 
// </copyright>
// <summary>
//   This randomizer plugin generates words which can be talked naturally.
//   It always takes one vocal after a consonant. This follow up to words like: buwizalo
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// This randomizer plugin generates words which can be talked naturally.
    /// It always takes one vocal after a consonant. This follow up to words like: buwizalo
    /// </summary>
    public class MnemonicString : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The count of words which will be generated
        /// </summary>
        private readonly int wordCount;

        /// <summary>
        /// The word min length.
        /// </summary>
        private readonly int wordMinLength;

        /// <summary>
        /// The word max length.
        /// </summary>
        private readonly int wordMaxLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="MnemonicString"/> class.
        /// </summary>
        /// <param name="wordCount">
        /// The count of words which will be generated
        /// </param>
        public MnemonicString(int wordCount)
            : this(wordCount, 3, 15)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MnemonicString"/> class.
        /// </summary>
        /// <param name="wordCount">
        /// The count of words which will be generated
        /// </param>
        /// <param name="wordMinLength">
        /// The word min length.
        /// </param>
        /// <param name="wordMaxLength">
        /// The word max length.
        /// </param>
        public MnemonicString(int wordCount, int wordMinLength, int wordMaxLength)
        {
            this.wordCount = wordCount;
            this.wordMinLength = wordMinLength;
            this.wordMaxLength = wordMaxLength;
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public string GetValue()
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            char[] consonants = { 'w', 'r', 't', 'z', 'p', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'c', 'v', 'b', 'n', 'm' };
            int wordLength = Random.Next(this.wordMinLength, this.wordMaxLength);

            string result = string.Empty;

            for (int i = 0; i < this.wordCount; i++)
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