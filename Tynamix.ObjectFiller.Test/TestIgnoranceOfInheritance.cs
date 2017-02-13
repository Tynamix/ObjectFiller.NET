using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectFiller.Test
{
    using ObjectFiller.Test.TestPoco.Person;

    
    using Tynamix.ObjectFiller;

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
