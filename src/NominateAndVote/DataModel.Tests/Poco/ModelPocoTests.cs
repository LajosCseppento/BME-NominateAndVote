using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataModel.Tests.Poco
{
    [TestClass]
    public class ModelPocoTests
    {
        private Administrator _administrator;
        private News _news;
        private Nomination _nomination;
        private Poll _poll;
        private PollSubject _pollSubject;
        private User _user;
        private Vote _vote;

        [TestInitialize]
        public void Initialize()
        {
            _administrator = new Administrator();
            _news = new News();
            _nomination = new Nomination();
            _poll = new Poll();
            _pollSubject = new PollSubject();
            _user = new User();
            _vote = new Vote();
        }

        [TestMethod]
        public void Administrator_Constructor()
        {
            Assert.AreEqual(0, _administrator.UserId);
        }

        [TestMethod]
        public void News_Constructor()
        {
            Assert.AreEqual(Guid.Empty, _news.Id);
            Assert.IsNull(_news.Title);
            Assert.IsNull(_news.Text);
            Assert.AreEqual(DateTime.MinValue, _news.PublicationDate);
        }

        [TestMethod]
        public void Nomination_Constructor()
        {
            Assert.AreEqual(Guid.Empty, _nomination.Id);
            Assert.IsNull(_nomination.Poll);
            Assert.IsNull(_nomination.User);
            Assert.IsNull(_nomination.Subject);
            Assert.IsNull(_nomination.Text);
            Assert.AreEqual(0, _nomination.Votes.Count);
            Assert.AreEqual(0, _nomination.VoteCount);
        }

        [TestMethod]
        public void Poll_Constructor()
        {
            Assert.AreEqual(Guid.Empty, _poll.Id);
            Assert.IsNull(_poll.Title);
            Assert.IsNull(_poll.Text);
            Assert.AreEqual(PollState.Nomination, _poll.State);
            Assert.AreEqual(DateTime.MinValue, _poll.PublicationDate);
            Assert.AreEqual(DateTime.MinValue, _poll.NominationDeadline);
            Assert.AreEqual(DateTime.MinValue, _poll.VotingStartDate);
            Assert.AreEqual(DateTime.MinValue, _poll.VotingDeadline);
            Assert.AreEqual(DateTime.MinValue, _poll.AnnouncementDate);
            Assert.AreEqual(0, _poll.Nominations.Count);
        }

        [TestMethod]
        public void PollSubject_Constructor()
        {
            Assert.AreEqual(0, _pollSubject.Id);
            Assert.IsNull(_pollSubject.Title);
            Assert.AreEqual(0, _pollSubject.Year);
        }

        [TestMethod]
        public void User_Constructor()
        {
            Assert.AreEqual(0, _user.Id);
            Assert.IsNull(_user.Name);
            Assert.AreEqual(false, _user.IsBanned);
            Assert.AreEqual(0, _user.Nominations.Count);
        }

        [TestMethod]
        public void Vote_Constructor()
        {
            Assert.IsNull(_vote.User);
            Assert.IsNull(_vote.Nomination);
            Assert.AreEqual(DateTime.MinValue, _vote.Date);
        }
    }
}