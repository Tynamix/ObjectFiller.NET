using System.Collections.Generic;

namespace ObjectFiller.Test.TestPoco.Library
{
    public class LibraryConstructorList : Library
    {
        public LibraryConstructorList(List<Book> books, string name)
        {
            Books = new List<IBook>(books);
            Name = name;
        }

        public LibraryConstructorList(List<IBook> books)
        {
            Books = books;
        }
    }
}