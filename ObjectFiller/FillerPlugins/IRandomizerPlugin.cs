namespace ObjectFiller.FillerPlugins
{
    /// <summary>
    /// Implement this interface to create a custom randomizer of type <see cref="T"/>
    /// </summary>
    /// <typeparam name="T">Type for which the randomizer will generate data</typeparam>
    public interface IRandomizerPlugin<out T>
    {
        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        T GetValue();
    }
}