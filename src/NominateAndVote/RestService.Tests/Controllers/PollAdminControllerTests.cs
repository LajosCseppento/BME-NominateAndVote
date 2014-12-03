using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage.Tests;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    public abstract class PollAdminControllerTests
    {
        private PollAdminController _controller;
        private PollController _controllerPoll;
        private IDataManager _dataManager;

        public abstract void Initialize();

        private void DoInitialize(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _controller = new PollAdminController(_dataManager);
            _controllerPoll = new PollController(_dataManager);
        }

        public abstract void Save();

        private void DoSave()
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

        public abstract void Save_Update();

        private void DoSave_Update()
        {
            // Arrange
            var polls = _controllerPoll.ListNominationPolls() as List<Poll>;

            var bindingModel = new SavePollBindingModel
            {
                Title = "xxx",
                Text = polls[0].Text,
                State = polls[0].State.ToString(),
                AnnouncementDate = polls[0].AnnouncementDate,
                VotingDeadline = polls[0].VotingDeadline,
                VotingStartDate = polls[0].VotingStartDate,
                PublicationDate = polls[0].PublicationDate,
                NominationDeadline = polls[0].NominationDeadline,
                Id=polls[0].Id.ToString()
            };

            // Act
            var result = _controller.Save(bindingModel) as OkNegotiatedContentResult<News>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result.Content.Id);
            Assert.AreEqual("xxx", result.Content.Title);
            Assert.AreNotEqual(DateTime.MinValue, result.Content.PublicationDate);
            Assert.AreEqual(result.Content, _dataManager.QueryPoll(polls[0].Id));
        }

        public abstract void Save_Null();

        private void DoSave_Null()
        {
            // Act
            var result = _controller.Save(null) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, "No data");
        }

        [TestClass]
        public class PollAdminControllerMemoryTests : PollAdminControllerTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                DoInitialize(new SampleDataModel().CreateDataManager());
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/PollAdminController")]
            public override void Save()
            {
                DoSave();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/PollAdminController")]
            public override void Save_Update()
            {
                DoSave_Update();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/PollAdminController")]
            public override void Save_Null()
            {
                DoSave_Null();
            }
        }

        [TestClass]
        public class PollAdminControllerTableStorageTests : PollAdminControllerTests
        {
            private DataTableStorageTestHelper _helper;

            [TestInitialize]
            public override void Initialize()
            {
                _helper = new DataTableStorageTestHelper();
                _helper.Initialize(new SampleDataModel());
                DoInitialize(_helper.TableStorageDataManager);
            }

            [TestCleanup]
            public void Cleanup()
            {
                _helper.CleanUp();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/PollAdminController")]
            public override void Save()
            {
                DoSave();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/PollAdminController")]
            public override void Save_Update()
            {
                DoSave_Update();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/PollAdminController")]
            public override void Save_Null()
            {
                DoSave_Null();
            }
        }
    }
}