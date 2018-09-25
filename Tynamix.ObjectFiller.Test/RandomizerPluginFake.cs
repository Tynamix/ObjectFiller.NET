using System;

namespace Tynamix.ObjectFiller.Test
{
    public class FakeRandomizerPlugin<T> : IRandomizerPlugin<T>
    {
        public Func<T> OnGetValue;

        public T ReturnValue { get; set; }

        public FakeRandomizerPlugin()
        {
        }

        public FakeRandomizerPlugin(T returnValue)
        {
            ReturnValue = returnValue;
        }

        public T GetValue()
        {
            if (OnGetValue != null)
            {
                return OnGetValue();
            }
            
            return ReturnValue;
        }
    }
}
