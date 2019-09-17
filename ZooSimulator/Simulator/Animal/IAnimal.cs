namespace ZooSimulator.Simulator.Animal
{
    interface IAnimal
    {
        string Name { get; }
        string Species { get; }
        IHealth Health { get; }
    }
}
