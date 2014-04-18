using System;
using Tynamix.ObjectFiller.Properties;

namespace Tynamix.ObjectFiller.Plugins
{
    public class RealNamePlugin : IRandomizerPlugin<string>
    {
        private readonly bool _firstName;
        private readonly bool _lastName;
        private readonly bool _fullName;
        private readonly Random _random;

        private string[] _firstNames;
        private string[] _lastNames;

        public RealNamePlugin(bool firstName, bool lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            _fullName = firstName && lastName;

            if (firstName)
            {
                _firstNames = Resources.firstNames.Split(';');
            }
            if (lastName)
            {
                _lastNames = Resources.lastNames.Split(';');
            }

            _random = new Random();
        }

        public RealNamePlugin(bool fullName)
            : this(true, fullName)
        {

        }

        public string GetValue()
        {
            if (_fullName)
            {
                string firstName = _firstNames[_random.Next(_firstNames.Length)];
                string lastName = _lastNames[_random.Next(_lastNames.Length)];

                return firstName + " " + lastName;
            }
            if (_firstName)
            {
                return _firstNames[_random.Next(_firstNames.Length)];
            }
            if (_lastName)
            {
                return _lastNames[_random.Next(_lastNames.Length)];
            }

            return null;
        }
    }
}
