using System;
using System.Collections.Generic;

namespace Tynamix.ObjectFiller
{
    public class FillerSetup
    {
        public FillerSetup()
        {
            TypeToFillerSetup = new Dictionary<Type, FillerSetupItem>();
            MainSetupItem = new FillerSetupItem();
        }
        internal FillerSetupItem MainSetupItem { get; private set; }

        internal Dictionary<Type, FillerSetupItem> TypeToFillerSetup { get; private set; }
    }
}