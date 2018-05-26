using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LifeManager.Data.Repositories;
using LifeManager.ListsService.Services;
using LifeManager.Models;
using Moq;
using NUnit.Framework;

namespace LifeManager.ListsService.Tests.Services
{
    [TestFixture]
    public class ListServiceTests
    {
        private Mock<IListRepository> _mockListRepository;

        [SetUp]
        public void SetUp()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ListModel, Data.Entities.List>().ReverseMap();                
            });
            _mockListRepository = new Mock<IListRepository>();
        }

        [Test]
        public async Task CreateList_ShouldCallListRepositoryAdd()
        {
            // Data
            var listModel = new ListModel{ Name="todo list", Id = Guid.NewGuid()};

            // Setup
            _mockListRepository.Setup(x => x.Add(It.Is<Data.Entities.List>(y => y.Name == listModel.Name)))
                .Returns(Task.CompletedTask).Verifiable();

            // Test
            var listService = new ListService(_mockListRepository.Object);
            await listService.CreateList(listModel);

            // Analysis
            _mockListRepository.Verify();
        }        

        [Test]
        public async Task UpdateList_ShouldCallListRepositoryUpdate()
        {
            // Data
            var listModel = new ListModel { Name = "todo list" };

            // Setup
            _mockListRepository.Setup(x => x.Update(It.Is<Data.Entities.List>(y => y.Name == listModel.Name)))
                .Returns(Task.CompletedTask).Verifiable();

            // Test
            var listService = new ListService(_mockListRepository.Object);
            await listService.UpdateList(listModel);

            // Analysis
            _mockListRepository.Verify();
        }

        [Test]
        public async Task DeleteList_ShouldCallListRepositoryDelete()
        {
            // Data
            var listModel = new ListModel { Name = "todo list" };

            // Setup
            _mockListRepository.Setup(x => x.Delete(listModel.Id, listModel.UserId))
                .Returns(Task.CompletedTask).Verifiable();

            // Test
            var listService = new ListService(_mockListRepository.Object);
            await listService.DeleteList(listModel.Id, listModel.UserId);

            // Analysis
            _mockListRepository.Verify();
        }

        [Test]
        public async Task GetList_ShouldCallListRepositoryGet()
        {
            // Data
            var listModel = new ListModel { Name = "todo list" };
            var listEntity = new Data.Entities.List{ Name = "todo list"};
            IEnumerable<Data.Entities.List> lists = new List<Data.Entities.List>
            {
                listEntity
            };
            // Setup
            _mockListRepository.Setup(x => x.Get(listModel.Id, listModel.UserId, listModel.Name))
                .Returns(Task.FromResult(lists)).Verifiable();

            // Test
            var listService = new ListService(_mockListRepository.Object);
            await listService.GetList(listModel);

            // Analysis
            _mockListRepository.Verify();
        }

        [Test]
        public async Task GetAllLists_ShouldCallListRepositoryGetAll()
        {
            // Data
            var listModel = new ListModel { Name = "todo list" };
            var listEntity = new Data.Entities.List { Name = "todo list" };
            IEnumerable<Data.Entities.List> lists = new List<Data.Entities.List>
            {
                listEntity
            };
            // Setup
            _mockListRepository.Setup(x => x.GetAll(listModel.UserId))
                .Returns(Task.FromResult(lists)).Verifiable();

            // Test
            var listService = new ListService(_mockListRepository.Object);
            await listService.GetAllLists(listModel.UserId);

            // Analysis
            _mockListRepository.Verify();
        }


    }
}