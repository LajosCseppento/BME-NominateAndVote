using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Get_Closed()
        {
            // Arrange

            // Act
            var result = controller.GetClosedPolls() as List<Poll>;

            // Assert
            Assert.IsTrue((dataManager.QueryPolls(PollState.CLOSED)).Count == result.Count);

        }

        [TestMethod]
        public void Get_Nomination()
        {
            // Arrange

            // Act
            var result = controller.GetNominationPolls() as List<Poll>;

            // Assert
            Assert.IsTrue((dataManager.QueryPolls(PollState.NOMINATION)).Count == result.Count);

        }

        [TestMethod]
        public void Get_Voting()
        {
            // Arrange

            // Act
            var result = controller.GetVotingPolls() as List<Poll>;

            // Assert
            Assert.IsTrue((dataManager.QueryPolls(PollState.VOTING)).Count == result.Count);

        }

        [TestMethod]
        public void Get_ByID()
        {
            // Arrange

            // Act
            Poll poll=dataManager.QueryPolls().ElementAt(0);
            var result = controller.Get(poll.ID.ToString()) as Poll;

            // Assert
            Assert.AreEqual(poll, result);

        }
    }
}
