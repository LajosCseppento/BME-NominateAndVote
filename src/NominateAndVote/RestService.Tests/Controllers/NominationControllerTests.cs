using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Linq;
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
            var poll = _dataManager.QueryPolls()[0];
            var user = new User { Id = 888, IsBanned = false, Name = "Kis Bela" };
            _dataManager.SaveUser(user);
            var subject = _dataManager.QueryPollSubject(1);

            var bindingModel = new SaveNominationBindingModel
            {
                Text = "text",
                PollId = poll.Id.ToString(),
                UserId = user.Id,
                SubjectId = subject.Id
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
            var poll = _dataManager.QueryPolls()[1];
            var nomination = poll.Nominations.First();

            // Act
            var result = _controller.Delete(nomination.Id.ToString());

            // Assert
            poll = _dataManager.QueryPolls()[1];
            Assert.IsTrue(result);
            Assert.IsFalse(poll.Nominations.Contains(nomination));
        }
    }
}