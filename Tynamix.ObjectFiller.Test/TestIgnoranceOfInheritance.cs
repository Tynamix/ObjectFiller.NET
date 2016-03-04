using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectFiller.Test
{
    using ObjectFiller.Test.TestPoco.Person;

    using Xunit;
    using Tynamix.ObjectFiller;

    public class Student : Person
    {
        public string Class { get; set; }
    }

    public class TestIgnoranceOfInheritance
    {
        [Fact]
        public void IfIgnoreInheritanceIsSetToTrueTheNameOfTheStudentShouldBeNull()
        {
            Filler<Student> filler = new Filler<Student>();
            filler.Setup().IgnoreInheritance();
            var student = filler.Create();

            Assert.Null(student.FirstName);
            Assert.NotNull(student.Class);
        }

        [Fact]
        public void IfIgnoreInheritanceIsSetToFalseTheNameOfTheStudentShouldNotBeNull()
        {
            Filler<Student> filler = new Filler<Student>();
            filler.Setup()
                .OnType<IAddress>().CreateInstanceOf<Address>();
            var student = filler.Create();

            Assert.NotNull(student.FirstName);
            Assert.NotNull(student.Class);
        }
    }
}
