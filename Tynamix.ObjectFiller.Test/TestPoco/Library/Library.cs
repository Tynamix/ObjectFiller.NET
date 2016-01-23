using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectFiller.Test.TestPoco.Library
{
    public abstract class Library
    {
        public ICollection<IBook> Books { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int CountOfBooks { get; set; }
    }
}
