using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    /// <summary>
    /// For creating health instances which can be manipulated for simulation purposes
    /// </summary>
    interface ISimulatableHealthFactory
    {
        /// <summary>
        /// Creates a new health instance which can be manipulated for simulation purposes
        /// </summary>
        /// <param name="getStatus">The strategy to use when determining the health status</param>
        /// <returns>A new health instance</returns>
        ISimulatableHealth Create(GetStatus getStatus);
    }
}
