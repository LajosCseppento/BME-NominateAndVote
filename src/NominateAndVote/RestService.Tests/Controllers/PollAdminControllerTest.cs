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
    public class PollAdminControllerTest
    {
        private PollAdminController controller;
        private DataModelManager dataManager;

        [TestInitialize]
        public void Initialize()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);

            controller = new PollAdminController(dataManager);
        }

        //Correct object
        [TestMethod]
        public void SavePoll_Correct()
        {
            // Arrange
            PollBindingModell bindingModel = new PollBindingModell()
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
            var result = controller.Save(bindingModel) as OkNegotiatedContentResult<Poll>;

            // Assert
            Assert.AreNotEqual(Guid.Empty, result.Content.ID);
            Assert.AreEqual(PollState.VOTING, result.Content.State);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, dataManager.QueryPoll(result.Content.ID));
        }
    }
}