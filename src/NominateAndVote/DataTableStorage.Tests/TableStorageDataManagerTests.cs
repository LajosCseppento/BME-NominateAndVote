using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Common;
using NominateAndVote.DataModel.Tests;

namespace NominateAndVote.DataTableStorage.Tests
{
    [TestClass]
    public class TableStorageDataManagerTests : DataManagerTests
    {
        private DataTableStorageTestHelper _helper;

        protected override IDataManager _createDataManager(IDataModel dataModel)
        {
            _helper = new DataTableStorageTestHelper();
            _helper.Initialize(dataModel);
            return _helper.TableStorageDataManager;
        }

        [TestInitialize]
        public override void DoInitialize()
        {
            base.DoInitialize();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _helper.CleanUp();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void IsAdmin()
        {
            base.IsAdmin();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryNews()
        {
            base.QueryNews();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SaveNews()
        {
            base.SaveNews();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void DeleteNews()
        {
            base.DeleteNews();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryNominationsWithPoll()
        {
            base.QueryNominationsWithPoll();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryNominationsWithUser()
        {
            base.QueryNominationsWithUser();
        }

        [TestMethod]
        [ExpectedException(typeof(DataException))]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryNominationsWithWrongUser()
        {
            base.QueryNominationsWithWrongUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryNominations()
        {
            base.QueryNominations();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SaveNomination()
        {
            base.SaveNomination();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void DeleteNomination()
        {
            base.DeleteNomination();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryPolls()
        {
            base.QueryPolls();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryPollsWithNominationState()
        {
            base.QueryPollsWithNominationState();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryPoll()
        {
            base.QueryPoll();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SavePoll()
        {
            base.SavePoll();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryPollSubject()
        {
            base.QueryPollSubject();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SearchPollSubjects()
        {
            base.SearchPollSubjects();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SavePollSubject()
        {
            base.SavePollSubject();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SavePollSubjectsBatch()
        {
            base.SavePollSubjectsBatch();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryBannedUsers()
        {
            base.QueryBannedUsers();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryUser()
        {
            base.QueryUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SearchUsers()
        {
            base.SearchUsers();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SaveUser()
        {
            base.SaveUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryVoteUserVoted()
        {
            base.QueryVoteUserVoted();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryVoteUserNoVote()
        {
            base.QueryVoteUserNoVote();
        }

        [TestMethod]
        [ExpectedException(typeof(DataException))]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryVoteNotExistingPoll()
        {
            base.QueryVoteNotExistingPoll();
        }

        [TestMethod]
        [ExpectedException(typeof(DataException))]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void QueryVoteNotExistingUser()
        {
            base.QueryVoteNotExistingUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataTableStorage/TableStorageDataManager")]
        public override void SaveVote()
        {
            base.SaveVote();
        }
    }
}