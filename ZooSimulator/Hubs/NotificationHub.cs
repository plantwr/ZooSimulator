using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ZooSimulator.Simulator.Simulator;

namespace ZooSimulator.Hubs
{
    /// <summary>
    /// Used by SignalR to communicate with clients
    /// </summary>
    class NotificationHub : Hub
    {
        public const string UpdateMethodName = "UpdateSimulator";

        private readonly ISimulator _simulator;

        public NotificationHub(ISimulator simulator)
        {
            _simulator = simulator;
        }

        /// <summary>
        /// Updates the simulator data for the caller
        /// </summary>
        /// <returns></returns>
        public Task UpdateSimulator()
        {
            return Clients.Caller.SendAsync(UpdateMethodName, _simulator);
        }

        /// <summary>
        /// Triggers a feed operation in the simulator
        /// </summary>
        /// <returns></returns>
        public Task Feed()
        {
            _simulator.Feed();
            return Clients.All.SendAsync(UpdateMethodName, _simulator);
        }
    }
}
