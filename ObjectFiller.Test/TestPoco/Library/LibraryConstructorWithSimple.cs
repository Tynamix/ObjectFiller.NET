namespace ObjectFiller.Test.TestPoco.Library
{
    public class LibraryConstructorWithSimple : Library
    {
        public LibraryConstructorWithSimple(string name, string city, int countOfBooks)
        {
            City = city;
            CountOfBooks = countOfBooks;
            Name = name;
        }
    }
}