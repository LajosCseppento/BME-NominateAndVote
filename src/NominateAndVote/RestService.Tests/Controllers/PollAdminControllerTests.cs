using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    /// <summary>
    /// Summary description for PollAdminController
    /// </summary>
    [TestClass]
    public class PollAdminControllerTests
    {
        private PollAdminController _controller;
        private DataModelManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            var model = new SimpleDataModel();
            model.LoadSampleData();
            _dataManager = new DataModelManager(model);

            _controller = new PollAdminController(_dataManager);
        }

        //Correct object
        [TestMethod]
        public void SavePoll_Correct()
        {
            // Arrange
            var bindingModel = new PollBindingModell
            {
                Text = "text",
                State = "VOTING",
                AnnouncementDate = DateTime.Now.AddDays(+5),
                VotingDeadline = DateTime.Now.AddDays(+2),
                VotingStartDate = DateTime.Now.AddDays(-5),
                PublicationDate = DateTime.Now.AddDays(-15),
                NominationDeadline = DateTime.Now.AddDays(-6)
            };

            // Act
            var result = _controller.Save(bindingModel) as OkNegotiatedContentResult<Poll>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual(PollState.Voting, result.Content.State);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryPoll(result.Content.Id));
        }
    }
}