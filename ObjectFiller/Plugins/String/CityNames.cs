namespace Tynamix.ObjectFiller.Plugins.String
{
    using System.Collections.Generic;

    public class CityNames : IRandomizerPlugin<string>
    {
        protected readonly List<string> _names = new List<string>
                                                  {
                                                      "Lodon",
                                                      "Paris",
                                                      "Dresden",
                                                      "Berlin",
                                                      "Rom",
                                                      "New York"
                                                  };

        public string GetValue()
        {
            var index = Random.Next(_names.Count - 1);
            return _names[index];
        }
    }
}
