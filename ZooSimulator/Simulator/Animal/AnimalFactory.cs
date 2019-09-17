namespace ZooSimulator.Simulator.Animal
{
    class AnimalFactory : IAnimalFactory
    {
        public IAnimal Create(string name, string species, IHealth health)
        {
            return new Animal(name, species, health);
        }
    }
}
