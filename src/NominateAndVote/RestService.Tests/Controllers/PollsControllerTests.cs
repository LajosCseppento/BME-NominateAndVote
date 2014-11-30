using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;

namespace NominateAndVote.RestService.Tests.Controllers
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PollsControllerMemoryTests : PollsControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            return new SampleDataModel().CreateDataManager();
        }
    }

    [TestClass]
    public class PollsControllerTableStorageTests : PollsControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            // TODO Lali 
            return new SampleDataModel().CreateDataManager();
        }
    }

    public abstract class PollsControllerGenericTests
    {
        private PollsController _controller;
        private IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = CreateDataManager();
            _controller = new PollsController(_dataManager);
        }

        protected abstract IDataManager CreateDataManager();

        [TestMethod]
        public void GetClosed()
        {
            // Arrange

            // Act
            var result = _controller.GetClosedPolls() as List<Poll>;

            // Assert
            Assert.IsNotNull(result);
            var polls = _dataManager.QueryPolls(PollState.Closed);
            var originalCount = polls.Count;
            Assert.AreEqual(originalCount, result.Count);
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var poll = _dataManager.QueryPolls()[0];

            // Act
            var result = _controller.Get(poll.Id.ToString());

            // Assert
            Assert.AreEqual(poll, result);
        }
       
    }
    
}