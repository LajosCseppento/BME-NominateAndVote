using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{

    [TestClass]
    public class NominationControllerMemoryTests : NominationControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            return new SampleDataModel().CreateDataManager();
        }
    }

    [TestClass]
    public class NominationControllerTableStoregeTests : NominationControllerGenericTests
    {
        protected override IDataManager CreateDataManager()
        {
            // TODO Lali
            return new SampleDataModel().CreateDataManager();
        }
    }

    public abstract class NominationControllerGenericTests
    {
        private NominationController _controller;
        private IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = CreateDataManager();
            _controller = new NominationController(_dataManager);
        }

        protected abstract IDataManager CreateDataManager();

        [TestMethod]
        public void SaveNomination()
        {
            // Arrange
            Poll poll = _dataManager.QueryPolls()[0];
            User user = new User { Id = Guid.NewGuid(), IsBanned = false, Name = "Kis Bela" };
            _dataManager.SaveUser(user);
            PollSubject subject = _dataManager.QueryPollSubject(1);
            var bindingModel = new NominationBindingModel
            {
                Text = "text",
                VoteCount = 0,
                PollId=poll.Id.ToString(),
                UserId=user.Id.ToString(),
                PollSubjectId=subject.Id
            };

            // Act
            var result = _controller.Save(bindingModel) as OkNegotiatedContentResult<Nomination>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual("text", result.Content.Text);
        }

        [TestMethod]
        public void DeleteNomination()
        {
            // Arrange
            var nomination = _dataManager.QueryPolls()[1].Nominations[0];

            // Act
            _controller.Delete(nomination.Id.ToString());

            // Assert
            Assert.IsFalse(_dataManager.QueryPolls()[1].Nominations.Contains(nomination));
        }
    }
}