using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectFiller.TestObjects
{
    public sealed class ClassWithPrivateStuff
    {
		public int WithPrivateSetter { get; private set; }
		public int WithoutSetter { get { return 123; } }
    }
}
