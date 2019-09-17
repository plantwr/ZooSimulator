using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    class SimulatableHealthFactory : ISimulatableHealthFactory
    {
        public ISimulatableHealth Create(GetStatus getStatus)
        {
            return new SimulatableHealth(getStatus);
        }
    }
}
