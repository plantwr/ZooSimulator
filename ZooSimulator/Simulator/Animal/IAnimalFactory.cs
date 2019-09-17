namespace ZooSimulator.Simulator.Animal
{
    interface IAnimalFactory
    {
        /// <summary>
        /// Creates an animal instance
        /// </summary>
        /// <param name="name">The name of the individual animal</param>
        /// <param name="species">The group of animals the animal belongs too</param>
        /// <param name="health">The health status of the animal</param>
        /// <returns>An animal instance</returns>
        IAnimal Create(string name, string species, IHealth health);
    }
}
