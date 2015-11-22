using System.Runtime.CompilerServices;

namespace ObjectFiller.Test.TestPoco.Library
{
    public class Book : IBook
    {
        public Book(string name, string isbn)
        {
            Name = name;
            ISBN = isbn;
        }

        public string Name { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}