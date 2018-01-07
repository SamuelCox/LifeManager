using System;
using LifeManager.Data.Entities;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using LifeManager.Data.Repositories;

namespace LifeManager.Data.Tests.Repositories
{
    [TestFixture]
    public class CalendarRepositoryTests
    {
        private Mock<MongoClient> _mockMongoClient;
        private Mock<IMongoDatabase> _mockMongoDatabase;

        [SetUp]
        public void SetUp()
        {
            _mockMongoClient = new Mock<MongoClient>();
            _mockMongoDatabase = new Mock<IMongoDatabase>();
            var mockMongoDbSettings = new Mock<MongoDatabaseSettings>();
            _mockMongoDatabase.Setup(x => x.Settings).Returns(mockMongoDbSettings.Object);
            _mockMongoClient.Setup(x => x.GetDatabase("LifeManager", null)).Returns(_mockMongoDatabase.Object);
        }

        [Test]
        public async Task Add_ShouldGetCalendarEventsDatabase()
        {
            //Data
            var mockMongoCollection = new Mock<IMongoCollection<CalendarEvent>>();
            var calendarEvent = new CalendarEvent{Name = "Event1"};

            //Setup
            mockMongoCollection.Setup(x => x.InsertOneAsync(It.IsAny<CalendarEvent>(), null, default(CancellationToken))).Verifiable();
            mockMongoCollection.Setup(x => x.Settings).Returns(new Mock<MongoCollectionSettings>().Object);
            mockMongoCollection.Setup(x => x.Database).Returns(_mockMongoDatabase.Object);
            _mockMongoDatabase.Setup(x => x.GetCollection<CalendarEvent>("CalendarEvents", It.IsAny<MongoCollectionSettings>()))
                .Returns(mockMongoCollection.Object).Verifiable();

            //Test
            var calendarRepo = new CalendarRepository(_mockMongoDatabase.Object);
            await calendarRepo.Add(calendarEvent);

            //Anaylsis
            mockMongoCollection.Verify(x => x.InsertOneAsync(It.Is<CalendarEvent>(y => y.Name == calendarEvent.Name), null, default(CancellationToken)));
            mockMongoCollection.Verify();
            _mockMongoDatabase.Verify();
        }
    }
}
