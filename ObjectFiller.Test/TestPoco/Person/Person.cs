using System;
using System.Collections.Generic;

namespace ObjectFiller.Test.TestPoco.Person
{
    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; private set; }

        public IList<string> SureNames { get; set; }
        
        public int Age { get; set; }

        public DateTime Birthdate { get; set; }

        public Address Address { get; set; }

        public string Title { get; set; }

        public Dictionary<string, IAddress> StringToIAddress { get; set; }

        public IList<IAddress> Addresses { get; set; }

        public ICollection<IAddress> AddressCollection { get; set; }

        public Dictionary<string, List<Address>> StringToListOfAddress { get; set; }
    }
}
