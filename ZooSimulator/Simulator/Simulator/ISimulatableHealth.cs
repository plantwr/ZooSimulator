using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    /// <summary>
    /// Provides an interface with which the health of an animal can be manipulated for simulation purposes
    /// </summary>
    interface ISimulatableHealth : IHealth
    {
        /// <summary>
        /// Simulates the health value being boosted due to receiving food
        /// </summary>
        /// <param name="increaseBy">The amount to increase the health value by (capped at 1.0)</param>
        /// <exception>ArgumentException when negative</exception>
        void Feed(double increaseBy);

        /// <summary>
        /// Simulates the health value being degraded due to not receiving food for one hour
        /// </summary>
        /// <param name="decreaseBy">The amount to decrease the health value by (minimum is 0)</param>
        /// /// <exception>ArgumentException when negative</exception>
        void OnTick(double decreaseBy);
    }
}
