using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tynamix.ObjectFiller.Setup
{
    public class FluentCircularApi<TTargetObject> where TTargetObject : class
    {
        private readonly FluentFillerApi<TTargetObject> _callback;
        private readonly SetupManager _setupManager;

        internal FluentCircularApi(FluentFillerApi<TTargetObject> callback, SetupManager setupManager)
        {
            _callback = callback;
            _setupManager = setupManager;
        }

        /// <summary>
        /// Call this when you want to get an exception in case of a circular reference in your filled model.
        /// By default the ObjectFiller recognizes circular references and stop filling them without throwing an exception.
        /// When you want to get an explicit exception on circular refernce call this method!
        /// </summary>
        /// <param name="throwException">True (default) when you want to get exception on a circular reference</param>
        public FluentFillerApi<TTargetObject> ThrowException(bool throwException = true)
        {
            _setupManager.GetFor<TTargetObject>().ThrowExceptionOnCircularReference = throwException;

            return _callback;
        }
    }
}
