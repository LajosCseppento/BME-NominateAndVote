using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NominateAndVote.DataModel.Model.Tests
{
    [TestClass]
    public class ModelPocoTests
    {
        private Administrator administrator;
        private News news;
        private Nomination nomination;
        private Poll poll;
        private PollSubject pollSubject;
        private User user;
        private Vote vote;

        [TestInitialize]
        public void Initialize()
        {
            administrator = new Administrator();
            news = new News();
            nomination = new Nomination();
            poll = new Poll();
            pollSubject = new PollSubject();
            user = new User();
            vote = new Vote();
        }

        [TestMethod]
        public void Administrator_Constructor()
        {
            Assert.AreEqual(Guid.Empty, administrator.UserID);
        }

        [TestMethod]
        public void News_Constructor()
        {
            Assert.AreEqual(Guid.Empty, news.ID);
            Assert.IsNull(news.Title);
            Assert.IsNull(news.Text);
            Assert.AreEqual(DateTime.MinValue, news.PublicationDate);
        }

        [TestMethod]
        public void Nomination_Constructor()
        {
            Assert.AreEqual(Guid.Empty, nomination.ID);
            Assert.IsNull(nomination.Poll);
            Assert.IsNull(nomination.User);
            Assert.IsNull(nomination.Subject);
            Assert.IsNull(nomination.Text);
            Assert.AreEqual(0, nomination.Votes.Count);
            Assert.AreEqual(0, nomination.VoteCount);
        }

        [TestMethod]
        public void Poll_Constructor()
        {
            Assert.AreEqual(Guid.Empty, poll.ID);
            Assert.IsNull(poll.Text);
            Assert.AreEqual(PollState.NOMINATION, poll.State);
            Assert.AreEqual(DateTime.MinValue, poll.PublicationDate);
            Assert.AreEqual(DateTime.MinValue, poll.NominationDeadline);
            Assert.AreEqual(DateTime.MinValue, poll.VotingStartDate);
            Assert.AreEqual(DateTime.MinValue, poll.VotingDeadline);
            Assert.AreEqual(DateTime.MinValue, poll.AnnouncementDate);
            Assert.AreEqual(0, poll.Nominations.Count);
        }

        [TestMethod]
        public void PollSubject_Constructor()
        {
            Assert.AreEqual(0, pollSubject.ID);
            Assert.IsNull(pollSubject.Title);
            Assert.AreEqual(0, pollSubject.Year);
        }

        [TestMethod]
        public void User_Constructor()
        {
            Assert.AreEqual(Guid.Empty, user.ID);
            Assert.IsNull(user.Name);
            Assert.AreEqual(false, user.IsBanned);
            Assert.AreEqual(0, user.Nominations.Count);
        }

        [TestMethod]
        public void Vote_Constructor()
        {
            Assert.IsNull(vote.User);
            Assert.IsNull(vote.Nomination);
            Assert.AreEqual(DateTime.MinValue, vote.Date);
        }
    }
}