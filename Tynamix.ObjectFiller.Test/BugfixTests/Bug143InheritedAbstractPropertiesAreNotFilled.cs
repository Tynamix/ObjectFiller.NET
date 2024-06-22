using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test.BugfixTests
{
    internal abstract class EntityBase
    {
        public string Name { get; set; }
        public abstract string AbstractName { get; set; }
    }

    internal class CustomEntity : EntityBase
    {
        public override string AbstractName { get; set; }
    }

    [TestClass]
    public class Bug143InheritedAbstractPropertiesAreNotFilled
    {
        [TestMethod]
        public void InheritedAbstractPropertiesShouldBeFilled()
        {
            Filler<CustomEntity> filler = new Filler<CustomEntity>();
            filler
                .Setup(true)
                .OnProperty(x => x.Name).Use("Alice")
                .OnProperty(x => x.AbstractName).Use("John");
            var entity = filler.Create();
            
            Assert.IsNotNull(entity);
            Assert.AreEqual("Alice", entity.Name);
            Assert.AreEqual("John", entity.AbstractName);
        }
    }
}
