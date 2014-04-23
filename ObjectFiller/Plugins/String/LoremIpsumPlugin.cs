using System;
using Tynamix.ObjectFiller.Properties;

namespace Tynamix.ObjectFiller.Plugins
{
    public class LoremIpsumPlugin : IRandomizerPlugin<string>
    {
        private readonly int _wordCount;
        private readonly string[] _loremIpsumWords;

        public LoremIpsumPlugin(int wordCount)
        {
            _wordCount = wordCount;
            _loremIpsumWords = Resources.loremIpsum.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public string GetValue()
        {
            string result = string.Empty;
            for (int i = 0; i < _wordCount; i++)
            {
                result += _loremIpsumWords[i % _loremIpsumWords.Length] + " ";
            }

            return result;
        }
    }
}