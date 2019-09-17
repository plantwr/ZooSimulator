namespace ZooSimulator.Simulator.Animal
{
    delegate AnimalHealthStatus GetStatus(double currentHealth, AnimalHealthStatus currentStatus);

    delegate string GetName(int animalIndex);

    /// <summary>
    /// Provides the configuration for creating a species in the simulation
    /// </summary>
    interface ISpeciesConfiguration
    {
        /// <summary>
        /// How many animals in this species should be created
        /// </summary>
        int Count { get; }

        /// <summary>
        /// The name of the species to be created
        /// </summary>
        string Species { get; }

        /// <summary>
        /// The strategy to use for determining the current status of the animals in the species
        /// </summary>
        GetStatus GetStatus { get; }

        /// <summary>
        /// The strategy to use for determining the name of an animal in the species
        /// </summary>
        GetName GetName { get; }
    }
}
