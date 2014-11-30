using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;

namespace NominateAndVote.RestService.Tests.Controllers
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PollsControllerTests
    {
        private PollsController _controller;
        private DataModelManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);

            _controller = new PollsController(_dataManager);
        }

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

            // Act
            var poll = _dataManager.QueryPolls()[0];
            var result = _controller.Get(poll.Id.ToString());

            // Assert
            Assert.AreEqual(poll, result);
        }
    }
}