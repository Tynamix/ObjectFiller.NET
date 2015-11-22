using System.Collections.Generic;

namespace ObjectFiller.Test.TestPoco.Library
{
    public class LibraryConstructorPoco : Library
    {
        public LibraryConstructorPoco(Book book)
        {
            Books = new List<IBook>();
            Books.Add(book);
        }
    }
}