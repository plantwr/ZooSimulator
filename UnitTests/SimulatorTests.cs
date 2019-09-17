using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnitTests;
using ZooSimulator.Simulator.Animal;
using ZooSimulator.Simulator.Simulator;

namespace UnitTests
{
    public class SimulatorTests
    {
        private ISimulator _sut;

        private TestConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            _sut = null;
            _configuration = new TestConfiguration();
        }

        private void CreateSut()
        {
            // Note these could be mocked later if they need dependencies, but for now a stronger test will be to use the actual implementations
            var animalFactory = new AnimalFactory(); 
            var healthFactory = new SimulatableHealthFactory();

            _sut = new Simulator(_configuration, animalFactory, healthFactory);
        }

        [Test]
        public void Construction_WhenRealConfigurationsAreUsed_ShouldCreateTheAnimals()
        {
            // Assign
            _configuration.AnimalConfigurations = new SimulatorConfiguration().AnimalConfigurations;

            // Act
            CreateSut();

            // Assert
            Assert.AreEqual(15, _sut.Animals.Count());

            var expectedSpecies = new[] {"Monkey", "Giraffe", "Elephant"};

            foreach (var species in expectedSpecies)
            {
                // Check there are 5 per species
                Assert.AreEqual(5, _sut.Animals.Count(a => a.Species == species));

                // Check they are named species 1-5
                for (var i = 0; i < _sut.Animals.Count(a => a.Species == species); i++)
                {
                    // Will throw an exception if none or more than one found and fail the test
                    _sut.Animals.Single(a => a.Name == species + " " + (i + 1));
                }
            }
        }

        [Test]
        public void Tick_ShouldReduceTheHealthOfEachAnimalInASpecies()
        {
            // Assign
            var decayCallCount = 0;
            _configuration.AnimalConfigurations = new [] { SimulatorConfiguration.MonkeyConfiguration };
            _configuration.GenerateDecay = () =>
            {
                decayCallCount++;
                return decayCallCount * 0.1;
            };
            CreateSut();

            // Act
            _sut.Tick();

            // Assert
            var animals = _sut.Animals.ToArray();
            Assert.True(animals[0].Health.Value.IsCloseEnoughTo(0.9));
            Assert.True(animals[1].Health.Value.IsCloseEnoughTo(0.8));
            Assert.True(animals[2].Health.Value.IsCloseEnoughTo(0.7));
            Assert.True(animals[3].Health.Value.IsCloseEnoughTo(0.6));
            Assert.True(animals[4].Health.Value.IsCloseEnoughTo(0.5));
        }

        [Test]
        public void Tick_ShouldNotAllowHealthToBeLessThanZero()
        {
            // Assign
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.MonkeyConfiguration };
            _configuration.GenerateDecay = () => 1000;
            CreateSut();

            // Act
            _sut.Tick();

            // Assert
            Assert.True(_sut.Animals.All(a => a.Health.Value.IsCloseEnoughTo(0)));
        }

        [Test]
        public void Feed_ShouldIncreaseTheHealthOfEachSpecies()
        {
            // Assign
            var feedCallCount = 0;
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.MonkeyConfiguration };
            _configuration.GenerateDecay = () => 0.2;
            _configuration.GenerateFeed = () =>
            {
                feedCallCount++;
                return feedCallCount * 0.1;
            };
            CreateSut();
            _sut.Tick(); // reduce the health a bit to ensure feed increase
            Assert.True(_sut.Animals.All(a => a.Health.Value.IsCloseEnoughTo(0.8))); // sanity check setup is correct

            // Act
            _sut.Feed();

            // Assert
            Assert.True(_sut.Animals.All(a => a.Health.Value.IsCloseEnoughTo(0.9)));
        }

        [Test]
        public void Feed_ShouldNotAllowHealthToBeGreaterThanOne()
        {
            // Assign
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.MonkeyConfiguration };
            _configuration.GenerateFeed = () => 1000;
            CreateSut();

            // Act
            _sut.Feed();

            // Assert
            Assert.True(_sut.Animals.All(a => a.Health.Value.IsCloseEnoughTo(1)));
        }

        [Test]
        public void Feed_DeadAnimal_ShouldNotIncreaseHealthOrUpdateStatus()
        {
            // Assign
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.MonkeyConfiguration };
            _configuration.GenerateDecay = () => 0.9;
            _configuration.GenerateFeed = () => 0.6;
            CreateSut();
            _sut.Tick(); // reduce the health so animals have dead status
            Assert.True(_sut.Animals.All(a => a.Health.Value.IsCloseEnoughTo(0.1))); // sanity check setup is correct
            Assert.True(_sut.Animals.All(a => a.Health.Status == AnimalHealthStatus.Dead));

            // Act
            _sut.Feed();

            // Assert
            Assert.True(_sut.Animals.All(a => a.Health.Value.IsCloseEnoughTo(0.1)));
            Assert.True(_sut.Animals.All(a => a.Health.Status == AnimalHealthStatus.Dead));
        }

        [Test]
        public void Tick_WhenAGiraffeHealthFallsBelow0dot5_ShouldUpdateStatusToDead()
        {
            // Assign
            var decayCallCount = 0;
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.GiraffeConfiguration };
            _configuration.GenerateDecay = () =>
            {
                decayCallCount++;
                return decayCallCount == 1 ? 0.51 : 0.5;
            };
            CreateSut();

            // Act
            _sut.Tick();

            // Assert
            var animals = _sut.Animals.ToArray();
            Assert.AreEqual(AnimalHealthStatus.Dead, animals[0].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[1].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[2].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[3].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[4].Health.Status);
        }

        [Test]
        public void Tick_WhenMonkeyHealthFallsBelow0dot3_ShouldUpdateStatusToDead()
        {
            // Assign
            var decayCallCount = 0;
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.MonkeyConfiguration };
            _configuration.GenerateDecay = () =>
            {
                decayCallCount++;
                return decayCallCount == 3 ? 0.71 : 0.7;
            };
            CreateSut();

            // Act
            _sut.Tick();

            // Assert
            var animals = _sut.Animals.ToArray();
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[0].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[1].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Dead, animals[2].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[3].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[4].Health.Status);
        }

        [Test]
        public void Tick_WhenElephantHealthFallsBelow0dot7_ShouldUpdateStatusToCannotWalk()
        {
            // Assign
            var decayCallCount = 0;
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.ElephantConfiguration };
            _configuration.GenerateDecay = () =>
            {
                decayCallCount++;
                return decayCallCount == 4 ? 0.31 : 0.3;
            };
            CreateSut();

            // Act
            _sut.Tick();

            // Assert
            var animals = _sut.Animals.ToArray();
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[0].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[1].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[2].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.CannotWalk, animals[3].Health.Status);
            Assert.AreEqual(AnimalHealthStatus.Alive, animals[4].Health.Status);
        }

        [Test]
        public void Feed_WhenElephantStatusIsCannotWalk_ShouldUpdateStatusToAlive()
        {
            // Assign
            var decayCallCount = 0;
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.ElephantConfiguration };
            _configuration.GenerateDecay = () =>
            {
                decayCallCount++;
                return decayCallCount == 4 ? 0.31 : 0.3;
            };
            _configuration.GenerateFeed = () => 0.1;
            CreateSut();
            _sut.Tick();
            // sanity check the setup is correct
            Assert.AreEqual(AnimalHealthStatus.CannotWalk, _sut.Animals.ToArray()[3].Health.Status);

            // Act
            _sut.Feed();

            // Assert
            Assert.AreEqual(AnimalHealthStatus.Alive, _sut.Animals.ToArray()[3].Health.Status);
        }

        [Test]
        public void Tick_WhenElephantStatusIsCannotWalk_ShouldUpdateStatusToDead()
        {
            // Assign
            var decayCallCount = 0;
            _configuration.AnimalConfigurations = new[] { SimulatorConfiguration.ElephantConfiguration };
            _configuration.GenerateDecay = () =>
            {
                decayCallCount++;
                return decayCallCount == 4 ? 0.31 : 0.3;
            };
            _configuration.GenerateFeed = () => 0.1;
            CreateSut();
            _sut.Tick();
            // sanity check the setup is correct
            Assert.AreEqual(AnimalHealthStatus.CannotWalk, _sut.Animals.ToArray()[3].Health.Status);

            // Act
            _sut.Tick();

            // Assert
            Assert.AreEqual(AnimalHealthStatus.Dead, _sut.Animals.ToArray()[3].Health.Status);
        }
    }

    /// <summary>
    /// Decorate the main configuration so that decay and feed rates are predictable
    /// </summary>
    class TestConfiguration : ISimulatorConfiguration
    {
        public int SecondsPerHour { get; set; }

        public IEnumerable<ISpeciesConfiguration> AnimalConfigurations { get; set; }

        public GenerateFeed GenerateFeed { get; set; }

        public GenerateFeed GenerateDecay { get; set; }
    }
}