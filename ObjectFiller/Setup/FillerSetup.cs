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

        /// <summary>
        /// Creates a setup for a specific type.
        /// </summary>
        /// <typeparam name="TTarget">The type which is configured.</typeparam>
        /// <returns>Setup you can use for Filler and Randomizer.</returns>
        public static FluentFillerApi<TTarget> Create<TTarget>() where TTarget : class
        {
            return new FluentFillerApi<TTarget>(new SetupManager());
        }

        /// <summary>
        /// Creates a setup based uppon another setup
        /// </summary>
        /// <typeparam name="TTarget">The type which is configured.</typeparam>
        /// <param name="baseSetup">The setup which is used as basis of the new one.</param>
        /// <returns>Setup you can use for Filler and Randomizer.</returns>
        public static FluentFillerApi<TTarget> Create<TTarget>(FillerSetup baseSetup) where TTarget : class
        {
            var setupManager = new SetupManager();
            setupManager.FillerSetup = baseSetup ?? new FillerSetup();

            return new FluentFillerApi<TTarget>(setupManager);
        }
    }
}