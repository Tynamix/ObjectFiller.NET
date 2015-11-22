// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentCircularApi.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   This API is for setup the behavior on circular dependencies
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// This API is for setup the behavior on circular dependencies
    /// </summary>
    /// <typeparam name="TTargetObject">Type which will be configured for the object filler</typeparam>
    public class FluentCircularApi<TTargetObject>
        where TTargetObject : class
    {
        /// <summary>
        /// The callback function
        /// </summary>
        private readonly FluentFillerApi<TTargetObject> callback;

        /// <summary>
        /// The object filler setup manager.
        /// </summary>
        private readonly SetupManager setupManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentCircularApi{TTargetObject}"/> class.
        /// </summary>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <param name="setupManager">
        /// The object filler setup manager.
        /// </param>
        internal FluentCircularApi(FluentFillerApi<TTargetObject> callback, SetupManager setupManager)
        {
            this.callback = callback;
            this.setupManager = setupManager;
        }

        /// <summary>
        /// Call this when you want to get an exception in case of a circular reference in your filled model.
        /// By default the ObjectFiller recognizes circular references and stop filling them without throwing an exception.
        /// When you want to get an explicit exception on circular reference call this method!
        /// </summary>
        /// <param name="throwException">
        /// True (default) when you want to get exception on a circular reference
        /// </param>
        /// <returns>
        /// The <see cref="FluentFillerApi{TTargetObject}"/>.
        /// </returns>
        public FluentFillerApi<TTargetObject> ThrowException(bool throwException = true)
        {
            this.setupManager.GetFor<TTargetObject>().ThrowExceptionOnCircularReference = throwException;

            return this.callback;
        }
    }
}
