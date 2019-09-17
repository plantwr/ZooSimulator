using System;
using System.Collections.Generic;
using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    /// <summary>
    /// Configures the simulator as per the technical test specification
    /// </summary>
    class SimulatorConfiguration : ISimulatorConfiguration
    {
        internal static SpeciesConfiguration MonkeyConfiguration = 
            new SpeciesConfiguration()
            {
                Count = 5,
                GetName = index => "Monkey " + (index + 1), // note we use the same name generator here, but the strategy-based API allows us to customise if need be
                GetStatus = (health, status) => health < 0.3 ? AnimalHealthStatus.Dead : AnimalHealthStatus.Alive,
                Species = "Monkey"
            };

        internal static SpeciesConfiguration GiraffeConfiguration =
            new SpeciesConfiguration()
            {
                Count = 5,
                GetName = index => "Giraffe " + (index + 1),
                GetStatus = (health, status) => health < 0.5 ? AnimalHealthStatus.Dead : AnimalHealthStatus.Alive,
                Species = "Giraffe"
            };

        internal static SpeciesConfiguration ElephantConfiguration =
            new SpeciesConfiguration()
            {
                Count = 5,
                GetName = index => "Elephant " + (index + 1),
                GetStatus = (health, status) =>
                {
                    if (status == AnimalHealthStatus.Dead)
                    {
                        return AnimalHealthStatus.Dead;
                    }

                    if (health < 0.7)
                    {
                        return status != AnimalHealthStatus.CannotWalk
                            ? AnimalHealthStatus.CannotWalk
                            : AnimalHealthStatus.Dead;
                    }

                    return AnimalHealthStatus.Alive;
                },
                Species = "Elephant"
            };

        public int SecondsPerHour => 20;

        public IEnumerable<ISpeciesConfiguration> AnimalConfigurations => new[] { MonkeyConfiguration, GiraffeConfiguration, ElephantConfiguration };

        public GenerateFeed GenerateFeed => () => new Random().NextDouble() * (0.25 - 0.1) + 0.1;

        public GenerateFeed GenerateDecay => () => new Random().NextDouble() * 0.2;
    }
}
