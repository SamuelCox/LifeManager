using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeManager.ListsService.Handlers;
using LifeManager.ListsService.Services;
using LifeManager.Messages.Lists;
using LifeManager.Models;
using Moq;
using NServiceBus;
using NUnit.Framework;

namespace LifeManager.ListsService.Tests.Handlers
{
    [TestFixture]
    public class ListsEventHandlerTests
    {
        private Mock<IListService> _mockListService;
        private Mock<IMessageHandlerContext> _mockMessageContext;

        [SetUp]
        public void SetUp()
        {
            _mockListService = new Mock<IListService>();
            _mockMessageContext = new Mock<IMessageHandlerContext>();
        }

        [Test]
        public async Task HandleAddCommand_ShouldCallListServiceCreate()
        {
            // Data
            var model = new ListModel {Id = Guid.NewGuid(), Name = "Todo list"};
            var addListCommand = new AddListCommand {Model = model};


            // Setup
            _mockListService.Setup(x => x.CreateList(model)).Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new ListsEventHandler(_mockListService.Object);
            await handler.Handle(addListCommand, _mockMessageContext.Object);
            
            // Analysis
            _mockListService.Verify();
        }

        [Test]
        public async Task HandleUpdateCommand_ShouldCallListServiceUpdate()
        {
            // Data
            var model = new ListModel { Id = Guid.NewGuid(), Name = "Todo list" };
            var updateListCommand = new UpdateListCommand { Model = model };


            // Setup
            _mockListService.Setup(x => x.UpdateList(model)).Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new ListsEventHandler(_mockListService.Object);
            await handler.Handle(updateListCommand, _mockMessageContext.Object);

            // Analysis
            _mockListService.Verify();
        }

        [Test]
        public async Task HandleDeleteCommand_ShouldCallListServiceDelete()
        {
            // Data            
            var deleteListCommand = new DeleteListCommand { Id = Guid.NewGuid(), UserId = "username"};


            // Setup
            _mockListService.Setup(x => x.DeleteList(deleteListCommand.Id, deleteListCommand.UserId)).Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new ListsEventHandler(_mockListService.Object);
            await handler.Handle(deleteListCommand, _mockMessageContext.Object);

            // Analysis
            _mockListService.Verify();
        }

        [Test]
        public async Task HandleGetCommand_ShouldCallListServiceGet()
        {
            // Data
            var model = new ListModel { Id = Guid.NewGuid(), Name = "Todo list" };
            var getListCommand = new GetListCommand { Model = model };
            IEnumerable<ListModel> modelsList = new List<ListModel>
            {
                model
            };

            // Setup
            _mockListService.Setup(x => x.GetList(model)).Returns(Task.FromResult(modelsList)).Verifiable();
            _mockMessageContext.Setup(x => x.Reply(It.Is<GetResponse>(y => y.Models == modelsList), It.IsAny<ReplyOptions>()))
                .Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new ListsEventHandler(_mockListService.Object);
            await handler.Handle(getListCommand, _mockMessageContext.Object);

            // Analysis
            _mockListService.Verify();
            _mockMessageContext.Verify();

        }

        [Test]
        public async Task HandleGetAllCommand_ShouldCallListServiceGetAll()
        {
            // Data
            var model = new ListModel { Id = Guid.NewGuid(), Name = "Todo list" };
            var getAllListsCommand = new GetAllListsCommand { UserId = "username"};
            IEnumerable<ListModel> modelsList = new List<ListModel>
            {
                model
            };

            // Setup
            _mockListService.Setup(x => x.GetAllLists(getAllListsCommand.UserId)).Returns(Task.FromResult(modelsList)).Verifiable();
            _mockMessageContext.Setup(x => x.Reply(It.Is<GetResponse>(y => y.Models == modelsList), It.IsAny<ReplyOptions>()))
                .Returns(Task.CompletedTask).Verifiable();

            // Test
            var handler = new ListsEventHandler(_mockListService.Object);
            await handler.Handle(getAllListsCommand, _mockMessageContext.Object);

            // Analysis
            _mockListService.Verify();
            _mockMessageContext.Verify();

        }


    }
}
