using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ObjectFiller.Test.TestPoco.ListTest
{
    public class EntityCollection
    {
        public List<Entity> EntityList { get; set; }

        public IEnumerable<Entity> EntityIEnumerable { get; set; }

        public ICollection<Entity> EntityICollection { get; set; }

        public ObservableCollection<Entity> ObservableCollection { get; set; }

        public IList<Entity> EntityIList { get; set; }

        public Entity[,] EntityArray { get; set; }
    }
}