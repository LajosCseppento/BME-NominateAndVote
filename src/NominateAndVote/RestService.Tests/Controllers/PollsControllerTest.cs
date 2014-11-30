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
    public class PollsControllerTest
    {
        private PollsController controller;
        private DataModelManager dataManager;

        [TestInitialize]
        public void Initialize()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);

            controller = new PollsController(dataManager);
        }

        [TestMethod]
        public void GetClosed()
        {
            // Arrange

            // Act
            var result = controller.GetClosedPolls() as List<Poll>;

            // Assert
            Assert.IsTrue((dataManager.QueryPolls(PollState.CLOSED)).Count == result.Count);
        }

        [TestMethod]
        public void GetByID()
        {
            // Arrange

            // Act
            Poll poll = dataManager.QueryPolls()[0];
            var result = controller.Get(poll.ID.ToString()) as Poll;

            // Assert
            Assert.AreEqual(poll, result);
        }
    }
}