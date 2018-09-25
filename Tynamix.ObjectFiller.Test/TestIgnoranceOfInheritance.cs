using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller.Test.TestPoco.Person;

namespace Tynamix.ObjectFiller.Test
{
    public class Student : Person
    {
        public string Class { get; set; }
    }

    [TestClass]
    public class TestIgnoranceOfInheritance
    {
        [TestMethod]
        public void IfIgnoreInheritanceIsSetToTrueTheNameOfTheStudentShouldBeNull()
        {
            Filler<Student> filler = new Filler<Student>();
            filler.Setup().IgnoreInheritance();
            var student = filler.Create();

            Assert.IsNull(student.FirstName);
            Assert.IsNotNull(student.Class);
        }

        [TestMethod]
        public void IfIgnoreInheritanceIsSetToFalseTheNameOfTheStudentShouldNotBeNull()
        {
            Filler<Student> filler = new Filler<Student>();
            filler.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>();
            var student = filler.Create();

            Assert.IsNotNull(student.FirstName);
            Assert.IsNotNull(student.Class);
        }
    }
}
