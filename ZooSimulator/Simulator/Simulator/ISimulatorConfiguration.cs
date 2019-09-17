using System.Collections.Generic;
using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    /// <summary>
    /// For generating a value to increase the health of an animal species on feed
    /// </summary>
    /// <returns>The amount to increase the health by</returns>
    delegate double GenerateFeed();

    /// <summary>
    /// For generating a value to decrease the health of an animal species on feed
    /// </summary>
    /// <returns>The amount to increase the health by</returns>
    delegate double GenerateDecay();

    interface ISimulatorConfiguration
    {
        /// <summary>
        /// How many seconds represent 1 hour in the simulation
        /// </summary>
        int SecondsPerHour { get; }

        /// <summary>
        /// The configuration of the animals in the simulation
        /// </summary>
        IEnumerable<ISpeciesConfiguration> AnimalConfigurations { get; }

        /// <summary>
        /// The strategy to use for generating the amount to increase the health value of an animal on feed
        /// </summary>
        GenerateFeed GenerateFeed { get; }

        /// <summary>
        /// The strategy to use for generating the amount to decrease the health value of an animal on an hour passed
        /// </summary>
        GenerateFeed GenerateDecay { get; }
    }
}
