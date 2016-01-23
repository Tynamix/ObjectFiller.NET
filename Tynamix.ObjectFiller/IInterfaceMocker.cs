// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInterfaceMocker.cs" company="Tynamix">
//   ©2015 by Roman Köhler
// </copyright>
// <summary>
//   Implement this interface to use a mocking framework for instantiate your interfaces.
//   Register this <see cref="IInterfaceMocker" /> in the setup of the ObjectFiller
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Implement this interface to use a mocking framework for instantiate your interfaces.
    /// Register this <see cref="IInterfaceMocker"/> in the setup of the ObjectFiller
    /// </summary>
    public interface IInterfaceMocker
    {
        /// <summary>
        /// Creates a mock of the interface with type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Type of the interface</typeparam>
        /// <returns>Mock of the interface</returns>
        T Create<T>() 
            where T : class;
    }
}