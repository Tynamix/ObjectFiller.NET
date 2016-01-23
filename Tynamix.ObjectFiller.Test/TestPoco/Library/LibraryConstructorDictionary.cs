using System.Collections.Generic;

namespace ObjectFiller.Test.TestPoco.Library
{
    public class LibraryConstructorDictionary : Library
    {
        public LibraryConstructorDictionary(Dictionary<IBook, string> dictionary)
        {
            Books = new List<IBook>();
            foreach (IBook book in dictionary.Keys)
            {
                Book b = (Book)book;
                b.Name = dictionary[book];

                Books.Add(b);
            }
        }

        public LibraryConstructorDictionary(Dictionary<Book, string> dictionary, string libName)
        {
            Name = libName;
            Books = new List<IBook>();
            foreach (Book book in dictionary.Keys)
            {
                book.Name = dictionary[book];

                Books.Add(book);
            }
        }
    }
}