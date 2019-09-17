using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using ZooSimulator.Hubs;
using ZooSimulator.Simulator.Simulator;

namespace ZooSimulator.Services
{
    /// <summary>
    /// Runs the simulator instance on a timer, incrementing the hours according to the defined SecondsPerHour
    /// </summary>
    class SimulatorService : IHostedService
    {
        private Timer _timer;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly ISimulator _simulator;

        public SimulatorService(IHubContext<NotificationHub> hubContext, ISimulator simulator)
        {
            _hubContext = hubContext;
            _simulator = simulator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(
                DoWork, 
                null,
                TimeSpan.FromSeconds(_simulator.SecondsPerHour),
                TimeSpan.FromSeconds(_simulator.SecondsPerHour)
            );

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _simulator.Tick();
            _hubContext.Clients.All.SendAsync(NotificationHub.UpdateMethodName, _simulator);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Dispose();

            return Task.CompletedTask;
        }
    }
}
