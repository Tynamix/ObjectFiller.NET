// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FillerSetup.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   Contains the setup per type
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains the setup per type
    /// </summary>
    public class FillerSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FillerSetup"/> class.
        /// </summary>
        public FillerSetup()
        {
            this.TypeToFillerSetup = new Dictionary<Type, FillerSetupItem>();
            this.MainSetupItem = new FillerSetupItem();
        }

        /// <summary>
        /// Gets the main setup item. This is always used when no type is explicitly configured
        /// </summary>
        internal FillerSetupItem MainSetupItem { get; private set; }

        /// <summary>
        /// Gets the <see cref="FillerSetupItem"/> for a specific <see cref="Type"/>
        /// </summary>
        internal Dictionary<Type, FillerSetupItem> TypeToFillerSetup { get; private set; }
    }
}