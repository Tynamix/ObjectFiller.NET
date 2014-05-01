namespace ObjectFiller.Test.TestPoco.Person
{
    public class OrderedPersonProperties
    {
        private int _currentCount = 0;
        private string _name;
        private int _age;
        private string _lastName;
        private int _nameCount;
        private int _ageCount;
        private int _lastNameCount;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _currentCount++;
                _nameCount = _currentCount;
            }
        }

        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;

                _currentCount++;
                _ageCount = _currentCount;
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                _currentCount++;
                _lastNameCount = _currentCount;
            }
        }


        public int NameCount
        {
            get { return _nameCount; }
        }

        public int AgeCount
        {
            get { return _ageCount; }
        }

        public int LastNameCount
        {
            get { return _lastNameCount; }
        }
    }
}