using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
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
    public class PollAdminControllerMemoryTests : PollAdminControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            return new SampleDataModel().CreateDataManager();
        }
    }

    [TestClass]
    public class PollAdminControllerTableStorageTests : PollAdminControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            // TODO Lali
            return new SampleDataModel().CreateDataManager();
        }
    }

    public abstract class PollAdminControllerGenericTests
    {
        private PollAdminController _controller;
        private IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = CreateDataManager();
            _controller = new PollAdminController(_dataManager);
        }

        protected abstract IDataManager CreateDataManager();

        //Correct object
        [TestMethod]
        public void SavePoll_Correct()
        {
            // Arrange
            var bindingModel = new SavePollBindingModel
            {
                Title = "title",
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
            Assert.AreEqual("title", result.Content.Title);
            Assert.AreEqual("text", result.Content.Text);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryPoll(result.Content.Id));
        }
    }
}