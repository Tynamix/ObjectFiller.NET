namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Implement this interface to use a mockingframework for instantiate your interfaces.
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