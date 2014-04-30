using System;
using Tynamix.ObjectFiller.Properties;

namespace Tynamix.ObjectFiller
{
    public class RealNames : IRandomizerPlugin<string>
    {
        private readonly bool _firstName;
        private readonly bool _lastName;
        private readonly bool _fullName;

        private string[] _firstNames;
        private string[] _lastNames;

        public RealNames(bool firstName, bool lastName)
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
        }

        public RealNames(bool fullName)
            : this(true, fullName)
        {

        }

        public string GetValue()
        {
            if (_fullName)
            {
                string firstName = _firstNames[Random.Next(_firstNames.Length)];
                string lastName = _lastNames[Random.Next(_lastNames.Length)];

                return firstName + " " + lastName;
            }
            if (_firstName)
            {
                return _firstNames[Random.Next(_firstNames.Length)];
            }
            if (_lastName)
            {
                return _lastNames[Random.Next(_lastNames.Length)];
            }

            return null;
        }
    }
}
