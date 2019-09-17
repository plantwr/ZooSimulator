namespace ZooSimulator.Simulator.Animal
{
    class SpeciesConfiguration : ISpeciesConfiguration
    {
        public int Count { get; set;  }

        public string Species { get; set; }

        public GetStatus GetStatus { get; set; }

        public GetName GetName { get; set; }
    }
}
