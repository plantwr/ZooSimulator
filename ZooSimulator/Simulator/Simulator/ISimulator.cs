using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    interface ISimulator
    {
        /// <summary>
        /// The current hour in the simulation
        /// </summary>
        int Hour { get; }

        /// <summary>
        /// How many seconds represent an hour in the simulation
        /// </summary>
        int SecondsPerHour { get; }

        /// <summary>
        /// The animals in the simulation
        /// </summary>
        IEnumerable<IAnimal> Animals { get; }

        /// <summary>
        /// Move the simulation on by one simulated hour
        /// </summary>
        void Tick();

        /// <summary>
        /// Feed the live animals in the zoo
        /// </summary>
        void Feed();
    }
}
