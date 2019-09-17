namespace ZooSimulator.Simulator.Animal
{
    class Animal : IAnimal
    {
        public Animal(string name, string species, IHealth health)
        {
            Name = name;
            Species = species;
            Health = health;
        }

        public string Name { get; }
        public string Species { get; }
        public IHealth Health { get; }
    }
}
