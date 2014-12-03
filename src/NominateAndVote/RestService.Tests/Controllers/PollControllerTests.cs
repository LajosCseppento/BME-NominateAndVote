using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage.Tests;
using NominateAndVote.RestService.Controllers;
using System.Linq;

namespace NominateAndVote.RestService.Tests.Controllers
{
    public abstract class PollControllerTests
    {
        private PollController _controller;
        private IDataManager _dataManager;

        public abstract void Initialize();

        private void DoInitialize(IDataManager dataManager)
        {
            _dataManager = dataManager;
            _controller = new PollController(_dataManager);
        }

        public abstract void GetNominationPolls();

        private void DoGetNominationPolls()
        {
            // Arrange

            // Act
            var result = _controller.ListNominationPolls();

            // Assert
            var polls = _dataManager.QueryPolls(PollState.Nomination);
            var originalCount = polls.Count;
            Assert.AreEqual(originalCount, result.Count());
        }

        public abstract void GetVotingPolls();

        private void DoGetVotingPolls()
        {
            // Arrange

            // Act
            var result = _controller.ListVotingPolls();

            // Assert
            Assert.IsNotNull(result);

            var polls = _dataManager.QueryPolls(PollState.Voting);
            var originalCount = polls.Count;
            Assert.AreEqual(originalCount, result.Count());
        }

        public abstract void GetClosedPolls();

        private void DoGetClosedPolls()
        {
            // Arrange

            // Act
            var result = _controller.ListClosedPolls();

            // Assert
            Assert.IsNotNull(result);

            var polls = _dataManager.QueryPolls(PollState.Closed);
            var originalCount = polls.Count;
            Assert.AreEqual(originalCount, result.Count());
        }

        public abstract void GetById();

        private void DoGetById()
        {
            // Arrange
            var poll = _dataManager.QueryPolls()[0];

            // Act
            var result = _controller.Get(poll.Id.ToString());

            // Assert
            Assert.AreEqual(poll, result);
        }

        [TestClass]
        public class PollControllerMemoryTests : PollControllerTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                DoInitialize(new SampleDataModel().CreateDataManager());
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/PollController")]
            public override void GetNominationPolls()
            {
                DoGetNominationPolls();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/PollController")]
            public override void GetVotingPolls()
            {
                DoGetVotingPolls();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/PollController")]
            public override void GetClosedPolls()
            {
                DoGetClosedPolls();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/Memory/PollController")]
            public override void GetById()
            {
                DoGetById();
            }
        }

        [TestClass]
        public class PollControllerTableStorageTests : PollControllerTests
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
            [TestCategory("Integration/RestService/TableStorage/PollController")]
            public override void GetNominationPolls()
            {
                DoGetNominationPolls();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/PollController")]
            public override void GetVotingPolls()
            {
                DoGetVotingPolls();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/PollController")]
            public override void GetClosedPolls()
            {
                DoGetClosedPolls();
            }

            [TestMethod]
            [TestCategory("Integration/RestService/TableStorage/PollController")]
            public override void GetById()
            {
                DoGetById();
            }
        }
    }
}