using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;

namespace NominateAndVote.RestService.Tests.Controllers
{
    [TestClass]
    public class PollControllerTest
    {
        private PollController controller;
        private DataModelManager dataManager;

        [TestInitialize]
        public void Initialize()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);

            controller = new PollController(dataManager);
        }

        [TestMethod]
        public void GetNomination()
        {
            // Arrange

            // Act
            var result = controller.GetNominationPolls() as List<Poll>;

            // Assert
            Assert.IsTrue((dataManager.QueryPolls(PollState.NOMINATION)).Count == result.Count);
        }

        [TestMethod]
        public void GetVoting()
        {
            // Arrange

            // Act
            var result = controller.GetVotingPolls() as List<Poll>;

            // Assert
            Assert.IsTrue((dataManager.QueryPolls(PollState.VOTING)).Count == result.Count);
        }
    }
}