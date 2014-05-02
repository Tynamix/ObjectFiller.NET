using System;
using Tynamix.ObjectFiller.Properties;

namespace Tynamix.ObjectFiller
{
    public enum RealNameStyle
    {
        FirstNameOnly,
        LastNameOnly,
        FirstNameLastName,
        LastNameFirstName
    }

    public class RealNames : IRandomizerPlugin<string>
    {
        private readonly RealNameStyle _rnStyle;

        private string[] _firstNames;
        private string[] _lastNames;

        public RealNames(RealNameStyle realNameStyle)
        {
            _rnStyle = realNameStyle;


            if (_rnStyle == RealNameStyle.FirstNameLastName
                || _rnStyle == RealNameStyle.FirstNameOnly)
            {
                _firstNames = Resources.firstNames.Split(';');
            }
            if (_rnStyle == RealNameStyle.LastNameFirstName
                || _rnStyle == RealNameStyle.LastNameOnly)
            {
                _lastNames = Resources.lastNames.Split(';');
            }
        }

        public string GetValue()
        {
            if (_rnStyle == RealNameStyle.FirstNameLastName || _rnStyle == RealNameStyle.LastNameFirstName)
            {
                string firstName = _firstNames[Random.Next(_firstNames.Length)];
                string lastName = _lastNames[Random.Next(_lastNames.Length)];


                return _rnStyle == RealNameStyle.FirstNameLastName ? firstName + " " + lastName : lastName + " " + firstName;
            }
            if (_rnStyle == RealNameStyle.FirstNameOnly)
            {
                return _firstNames[Random.Next(_firstNames.Length)];
            }
            if (_rnStyle == RealNameStyle.LastNameOnly)
            {
                return _lastNames[Random.Next(_lastNames.Length)];
            }

            return null;
        }
    }
}
