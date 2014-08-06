using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class ObjectFillerRecursiveTests
    {

        // ReSharper disable ClassNeverInstantiated.Local

        private class TestParent
        {
            public TestChild Child { get; set; }
        }

        private class TestChild
        {
            public TestParent Parent { get; set; }
        }

        private class TestGrandParent
        {
            public TestParent SubObject { get; set; }
        }

        private class TestEmptyNode
        {
            public string Value { get; set; }
        }

        private class TestSelf
        {
            public TestSelf Self { get; set; }

            public string Foo { get; set; }
        }

        private class TestDuplicate
        {
            public TestEmptyNode Node1 { get; set; }
            public TestEmptyNode Node2 { get; set; }
        }

        public class Children
        {
            public Parent Parent { get; set; }
        }

        public class Parent
        {
            public List<Children> Childrens { get; set; }
        }

        public class ParentDictionary
        {
            public Dictionary<string, Children> Childrens { get; set; }
        }

        // ReSharper restore ClassNeverInstantiated.Local

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RecursiveFill_RecursiveType_ThrowsException()
        {
            var filler = new Filler<TestParent>();
            filler.Setup().OnCircularReference().ThrowException();
            filler.Create();
        }

        [TestMethod]
        public void RecursiveFill_WithIgnoredProperties_Succeeds()
        {
            var filler = new Filler<TestParent>();
            filler.Setup().OnProperty(p => p.Child).IgnoreIt();
            var result = filler.Create();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RecursiveFill_WithFunc_Succeeds()
        {
            var filler = new Filler<TestParent>();
            filler.Setup().OnProperty(p => p.Child).Use(() => new TestChild());
            var result = filler.Create();

            Assert.IsNotNull(result.Child);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RecursiveFill_RecursiveType_Parent_First_Fails()
        {
            var filler = new Filler<TestParent>();
            filler.Setup().OnCircularReference().ThrowException();
            filler.Create();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RecursiveFill_RecursiveType_Child_First_Fails()
        {
            var filler = new Filler<TestChild>();
            filler.Setup().OnCircularReference().ThrowException();
            filler.Create();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RecursiveFill_DeepRecursiveType_Fails()
        {
            var filler = new Filler<TestGrandParent>();
            filler.Setup().OnCircularReference().ThrowException();
            filler.Create();

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RecursiveFill_SelfReferencing_Fails()
        {
            var filler = new Filler<TestSelf>();
            filler.Setup().OnCircularReference().ThrowException();
            filler.Create();
        }

        [TestMethod]
        public void RecursiveFill_DuplicateProperty_Succeeds()
        {
            // reason: a filler should not complain if it encounters the same type 
            // in two separate branches of a type hierarchy
            var filler = new Filler<TestDuplicate>();
            var result = filler.Create();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RecursiveFill_RecursiveType_Parent_First_Succeeds()
        {
            var filler = new Filler<TestParent>();
            var r = filler.Create();
            Assert.IsNull(r.Child.Parent.Child);
        }

        [TestMethod]
        public void RecursiveFill_RecursiveType_Child_First_Succeeds()
        {
            var filler = new Filler<TestChild>();
            var r = filler.Create();
            Assert.IsNull(r.Parent.Child.Parent);

        }

        [TestMethod]
        public void RecursiveFill_DeepRecursiveType_Succeeds()
        {
            var filler = new Filler<TestGrandParent>();
            var r = filler.Create();
            Assert.IsNull(r.SubObject.Child.Parent);
        }

        [TestMethod]
        public void RecursiveFill_SelfReferencing_Succeeds()
        {
            var filler = new Filler<TestSelf>();
            var r = filler.Create();

            Assert.IsNull(r.Self.Self);
        }

        [TestMethod]
        public void RecursiveFill_ParentList_Succeeds()
        {
            var filler = new Filler<Parent>();
            var r = filler.Create();

            Assert.IsNull(r.Childrens[0].Parent.Childrens);
        }

        [TestMethod]
        public void RecursiveFill_ParentDictionary_Succeeds()
        {
            var filler = new Filler<ParentDictionary>();
            var r = filler.Create();
        }


    }
}