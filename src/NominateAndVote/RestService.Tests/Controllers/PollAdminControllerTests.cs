using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage.Tests;
using NominateAndVote.RestService.Controllers;
using NominateAndVote.RestService.Models;
using System;
using System.Web.Http.Results;

namespace NominateAndVote.RestService.Tests.Controllers
{
    public abstract class PollAdminControllerTests
    {
        private PollAdminController _controller;
        private IDataManager _dataManager;

        public abstract void Initialize();

        private void DoInitialize(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _controller = new PollAdminController(_dataManager);
        }

        public abstract void Save();

        private void DoSave()
        {
            // TODO Ági ide csak egy teszteset van? egyébként a felhasználótól bejövő adatokat ellenőrizni kell! (pl datetime ki van-e töltve!)
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
        }
    }
}