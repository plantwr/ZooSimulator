namespace ZooSimulator.Simulator.Animal
{
    /// <summary>
    /// Represents the current health state of an animal
    /// </summary>
    interface IHealth
    {
        /// <summary>
        /// The percentage of health/life remaining
        /// </summary>
        double Value { get; }

        /// <summary>
        /// The categorised status of the animal
        /// </summary>
        AnimalHealthStatus Status { get; }
    }
}
