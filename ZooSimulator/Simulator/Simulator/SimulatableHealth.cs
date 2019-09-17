using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ZooSimulator.Simulator.Animal;

namespace ZooSimulator.Simulator.Simulator
{
    class SimulatableHealth : ISimulatableHealth
    {
        private const double MaxValue = 1;
        private const double MinValue = 0;

        // Use a lock to ensure an animal cannot die and feed concurrently (which could cause odd statuses)
        private readonly object _valueLock = new object();
        private readonly GetStatus _getStatus;

        public SimulatableHealth(GetStatus getStatus)
        {
            _getStatus = getStatus;
            Value = MaxValue;
        }

        public double Value { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AnimalHealthStatus Status { get; private set; }

        public void Feed(double increaseBy)
        {
            if (increaseBy < 0)
            {
                throw new ArgumentException("increaseBy cannot be less than 0");
            }

            lock (_valueLock)
            {
                if (Status == AnimalHealthStatus.Dead) // safety check in case we try to feed a dead animal
                {
                    return;
                }
                Value += increaseBy;
                Value = Math.Min(Value, MaxValue);
                Status = _getStatus(Value, Status);
            }
        }

        public void OnTick(double decreaseBy)
        {
            if (decreaseBy < 0)
            {
                throw new ArgumentException("decreaseBy cannot be less than 0");
            }

            lock (_valueLock)
            {
                Value -= decreaseBy;
                Value = Math.Max(Value, MinValue);
                Status = _getStatus(Value, Status);
            }
        }
    }
}
