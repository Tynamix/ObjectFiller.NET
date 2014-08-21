using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{

    public abstract class Parent
    {
        public Parent(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }

    public class ChildOne : Parent
    {
        public ChildOne(int id)
            : base(id)
        {
        }

        public string Name { get; set; }
    }

    public class ChildTwo : Parent
    {
        public ChildTwo(int id)
            : base(id)
        {
        }

        public string OtherName { get; set; }
    }

    public class ParentOfParent
    {
        public ParentOfParent()
        {
            Parents = new List<Parent>();
        }

        public List<Parent> Parents { get; private set; }

    }


    [TestClass]
    public class CreateInstanceTest
    {

        [TestMethod]
        public void TestCreateInstanceOfChildOne()
        {
            Filler<ParentOfParent> p = new Filler<ParentOfParent>();

            p.Setup().OnType<Parent>().CreateInstanceOf<ChildOne>()
                .SetupFor<ChildOne>()
                .OnProperty(x => x.Name).Use(() => "TEST");

            var pop = p.Create();

            Assert.IsNotNull(pop);
            Assert.IsNotNull(pop.Parents);
            Assert.IsTrue(pop.Parents.All(x => x is ChildOne));
            Assert.IsFalse(pop.Parents.Any(x => x is ChildTwo));

            Assert.IsTrue(pop.Parents.Cast<ChildOne>().All(x => x.Name == "TEST"));


        }

    }
}