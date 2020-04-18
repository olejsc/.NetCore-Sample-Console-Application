using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using Transport;
namespace NUnitTransportTests
{
    //[TestFixture("Ole Jakob Schjoeth", "A bus", "Methods related to implementation of ABus")]
    public class AbstractABusTestClass
    {
        public DieselBus bus;
        // [OneTimeSetUp]
        [SetUp]
        public void Setup ()
        {
            bus = new DieselBus();
        }

        [Test]
        public void BusGuid_ShouldNotBeNullAfterConstructor()
        {
            // Arrange


            // Act

            // Assert
            Assert.IsNotNull(bus.BusID);
        }

        [Test]
        public void Wheels_ShouldNotNegative ()
        {
            Assert.Positive(bus.Wheels);
        }
        [Test]
        public void Wheels_PropertyExist ()
        {
            Assert.That(bus,Has.Property("Wheels"));
        }

        [Test]
        public void Engine_PropertyExist ()
        {
            Assert.That(bus, Has.Property("Engine"));
        }
        [Test]
        public void DieselBus_AssignableFromABus ()
        {
            Assert.That(bus, Is.AssignableFrom<ABus>());
        }

        [Test]
        public void ABus_IsAtMaximumCapacityExistPropertu ()
        {
            Assert.That(bus, Has.Property("IsAtMaximumCapacity"));
        }

        [Test]
        public void ABus_HandicapSeatsPropertyExist ()
        {
            Assert.That(bus, Has.Property("HandicapSeats"));
        }

        [Test]
        public void Abus_PeopleInitializationValueIsEqualToSumOfAllSeatsAndDriver ()
        {
            Assert.AreEqual(bus.Seats+bus.StandingSpots+bus.HandicapSeats+1, bus.People.Capacity);
        }

        [Test]
        public void ABus_CanPersonWithNullTicketEnter ()
        {
            APerson person = new Adult();
            person.BusEntrance = (IEnterable)bus;
            bool canjoin = person.BusEntrance.CanEnter(null);
            Assert.AreEqual(canjoin, false);
        }
    }
}