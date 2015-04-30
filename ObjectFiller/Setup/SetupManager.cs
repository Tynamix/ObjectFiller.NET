// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupManager.cs" company="Tynamix">
//   © 2014 by Roman Köhler
// </copyright>
// <summary>
//   Responsible to get the right <see cref="FillerSetupItem" /> for a given type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;

    /// <summary>
    /// Responsible to get the right <see cref="FillerSetupItem"/> for a given type.
    /// </summary>
    internal class SetupManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetupManager"/> class. 
        /// </summary>
        internal SetupManager()
        {
            FillerSetup = new FillerSetup();
        }

        /// <summary>
        /// Gets or sets the filler setup.
        /// </summary>
        internal FillerSetup FillerSetup { get; set; }

        /// <summary>
        /// Gets the <see cref="FillerSetupItem"/> for a given type
        /// </summary>
        /// <typeparam name="TTargetObject">Type for which a <see cref="FillerSetupItem"/> will be get</typeparam>
        /// <returns><see cref="FillerSetupItem"/> for type <see cref="TTargetObject"/></returns>
        internal FillerSetupItem GetFor<TTargetObject>()
            where TTargetObject : class
        {
            return this.GetFor(typeof(TTargetObject));
        }

        /// <summary>
        /// Gets the <see cref="FillerSetupItem"/> for a given type
        /// </summary>
        /// <param name="targetType">Type for which a <see cref="FillerSetupItem"/> will be get</param>
        /// <returns><see cref="FillerSetupItem"/> for type <see cref="targetType"/></returns>
        internal FillerSetupItem GetFor(Type targetType)
        {
            if (FillerSetup.TypeToFillerSetup.ContainsKey(targetType))
            {
                return FillerSetup.TypeToFillerSetup[targetType];
            }

            return FillerSetup.MainSetupItem;
        }

        /// <summary>
        /// Sets a new <see cref="FillerSetupItem"/> for the given <see cref="TTargetObject"/>
        /// </summary>
        /// <typeparam name="TTargetObject">Type of target object for which a new <see cref="FillerSetupItem"/> will be set.</typeparam>
        /// <param name="useDefaultSettings">FALSE if the target object will take the settings of the parent object</param>
        internal void SetNewFor<TTargetObject>(bool useDefaultSettings)
            where TTargetObject : class
        {
            FillerSetup.TypeToFillerSetup[typeof(TTargetObject)] = useDefaultSettings ? new FillerSetupItem() : FillerSetup.MainSetupItem;
        }
    }
}
