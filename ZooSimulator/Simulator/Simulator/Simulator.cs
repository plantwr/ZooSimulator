using System.Collections.Generic;
using System.Linq;
using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    /// <summary>
    /// Runs a simulation of a Zoo with the provided configuration
    /// </summary>
    class Simulator : ISimulator
    {
        private readonly Dictionary<string, ISimulatableHealth[]> _animalHealths;
        private readonly ISimulatorConfiguration _configuration;

        public Simulator(
            ISimulatorConfiguration configuration,
            IAnimalFactory animalFactory,
            ISimulatableHealthFactory healthFactory
            )
        {
            _animalHealths = new Dictionary<string, ISimulatableHealth[]>();
            _configuration = configuration;

            var animals = new List<IAnimal>();

            foreach (var animalConfig in configuration.AnimalConfigurations)
            {
                _animalHealths[animalConfig.Species] = Enumerable.Repeat(0, animalConfig.Count).Select(i => healthFactory.Create(animalConfig.GetStatus)).ToArray();

                for (var i = 0; i < _animalHealths[animalConfig.Species].Length; i++)
                {
                    animals.Add(animalFactory.Create(animalConfig.GetName(i), animalConfig.Species, _animalHealths[animalConfig.Species][i]));
                }
            }

            Animals = animals.ToArray();
            SecondsPerHour = configuration.SecondsPerHour;
        }

        public IEnumerable<IAnimal> Animals { get; }

        public int SecondsPerHour { get; }

        public int Hour { get; private set; }

        public void Tick()
        {
            foreach (var species in _animalHealths.Keys)
            {
                foreach (var health in _animalHealths[species])
                {
                    health.OnTick(_configuration.GenerateDecay());
                }
            }

            Hour++;
        }

        public void Feed()
        {
            foreach (var species in _animalHealths.Keys)
            {
                var speciesFeed = _configuration.GenerateFeed();

                foreach (var health in _animalHealths[species])
                {
                    health.Feed(speciesFeed);
                }
            }
        }
    }
}
