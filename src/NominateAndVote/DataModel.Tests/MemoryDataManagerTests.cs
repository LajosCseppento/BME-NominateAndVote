using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Common;

namespace NominateAndVote.DataModel.Tests
{
    [TestClass]
    public class MemoryDataManagerTests : DataManagerTests
    {
        protected override IDataManager _createDataManager(IDataModel dataModel)
        {
            return new MemoryDataManager(dataModel);
        }

        [TestInitialize]
        public override void DoInitialize()
        {
            base.DoInitialize();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void IsAdmin()
        {
            base.IsAdmin();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryNews()
        {
            base.QueryNews();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SaveNews()
        {
            base.SaveNews();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void DeleteNews()
        {
            base.DeleteNews();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryNominationsWithPoll()
        {
            base.QueryNominationsWithPoll();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryNominationsWithUser()
        {
            base.QueryNominationsWithUser();
        }

        [TestMethod]
        [ExpectedException(typeof(DataException))]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryNominationsWithWrongUser()
        {
            base.QueryNominationsWithWrongUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryNominations()
        {
            base.QueryNominations();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SaveNomination()
        {
            base.SaveNomination();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void DeleteNomination()
        {
            base.DeleteNomination();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryPolls()
        {
            base.QueryPolls();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryPollsWithNominationState()
        {
            base.QueryPollsWithNominationState();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryPoll()
        {
            base.QueryPoll();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SavePoll()
        {
            base.SavePoll();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryPollSubject()
        {
            base.QueryPollSubject();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SearchPollSubjects()
        {
            base.SearchPollSubjects();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SavePollSubject()
        {
            base.SavePollSubject();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SavePollSubjectsBatch()
        {
            base.SavePollSubjectsBatch();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryBannedUsers()
        {
            base.QueryBannedUsers();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryUser()
        {
            base.QueryUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SearchUsers()
        {
            base.SearchUsers();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SaveUser()
        {
            base.SaveUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryVoteUserVoted()
        {
            base.QueryVoteUserVoted();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryVoteUserNotVotes()
        {
            base.QueryVoteUserNotVotes();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryVoteNotExistingPoll()
        {
            base.QueryVoteNotExistingPoll();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void QueryVoteNotExistingUser()
        {
            base.QueryVoteNotExistingUser();
        }

        [TestMethod]
        [TestCategory("Integration/DataModel/MemoryDataManager")]
        public override void SaveVote()
        {
            base.SaveVote();
        }
    }
}