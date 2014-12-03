using Microsoft.VisualStudio.TestTools.UnitTesting;
using NominateAndVote.DataModel.Common;
using NominateAndVote.DataModel.Poco;
using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel.Tests
{
    public abstract class DataManagerGenericTests
    {
        protected IDataManager _dataManager;

        [TestInitialize]
        public void Initialize()
        {
            _dataManager = _createDataManager(new SampleDataModel());
        }

        protected abstract IDataManager _createDataManager(IDataModel dataModel);

        [TestMethod]
        public void IsAdmin()
        {
            // Act & Assert
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 0 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 1 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 2 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 3 }));
            Assert.AreEqual(true, _dataManager.IsAdmin(new User { Id = 4 }));
            Assert.AreEqual(false, _dataManager.IsAdmin(new User { Id = 5 }));
        }

        [TestMethod]
        public void QueryNews()
        {
            // Act
            var list = _dataManager.QueryNews();

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Second", list[0].Title);
            Assert.AreEqual("First", list[1].Title);
        }

        [TestMethod]
        public void SaveNews()
        {
            // Arrange
            var list = _dataManager.QueryNews();

            // Act
            // create
            var news = new News { Id = Guid.Empty, PublicationDate = DateTime.Now, Title = "Third", Text = "x" };
            _dataManager.SaveNews(news);

            // update
            list[0].Title = "Second2";
            _dataManager.SaveNews(list[0]);

            // Assert
            list = _dataManager.QueryNews();
            Assert.AreEqual(3, list.Count);
            Assert.AreNotEqual(Guid.Empty, list[0].Id); // new id should have been assigned
            Assert.AreEqual("Third", list[0].Title);
            Assert.AreEqual("Second2", list[1].Title);
            Assert.AreEqual("First", list[2].Title);
        }

        [TestMethod]
        public void DeleteNews(Guid id)
        {
            // Act
            var list = _dataManager.QueryNews();

            // exists, should delete
            _dataManager.DeleteNews(list[1].Id);

            // not exists, should not cause exception
            _dataManager.DeleteNews(Guid.NewGuid());

            // Assert
            list = _dataManager.QueryNews();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Second", list[0].Title);
        }

        [TestMethod]
        public void QueryNominationsWithPoll()
        {
            // Act
            var poll = _dataManager.QueryPolls()[0];
            var list = _dataManager.QueryNominations(poll);

            // Assert
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Mert en azt mondtam", list[0].Text);
        }

        [TestMethod]
        public void QueryNominationsWithUser() //nem jo a hossz
        {
            // Act
            var user = _dataManager.QueryUser(1);
            var list = _dataManager.QueryNominations(user);

            // Assert
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Mert en azt mondtam", list[0].Text);
            Assert.AreEqual("A kedvencem", list[1].Text);
            Assert.AreEqual("Jok a szineszek", list[2].Text);
            Assert.AreEqual("Valami", list[3].Text);
        }

        [TestMethod]
        [ExpectedException(typeof(DataException))]
        public void QueryNominationsWithWrongUser()
        {
            // Act
            var user = new User { Id = 40, Name = "V", IsBanned = false };
            _dataManager.QueryNominations(user);

            // Assert
            // expected exception
        }

        [TestMethod]
        public void QueryNominations()
        {
            // Act
            var poll = _dataManager.QueryPolls()[0];
            var user = _dataManager.QueryUser(1);
            var list = _dataManager.QueryNominations(poll, user);

            // Assert
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Mert en azt mondtam", list[0].Text);
        }

        [TestMethod]
        public void SaveNomination()
        {
            // Arrange
            var poll = _dataManager.QueryPolls()[0];
            var subject = _dataManager.QueryPollSubject(1);
            var user = _dataManager.QueryUser(1);
            var list = _dataManager.QueryNominations(poll);

            // Act
            // create
            var nom = new Nomination { Id = Guid.Empty, Poll = poll, Text = "Second", Subject = subject, User = user };
            _dataManager.SaveNomination(nom);

            // update
            list[0].Text = "Proba";
            _dataManager.SaveNomination(list[0]);

            // Assert
            list = _dataManager.QueryNominations(poll);
            Assert.AreEqual(2, list.Count);
            Assert.AreNotEqual(Guid.Empty, list[1].Id); // new id should have been assigned
            Assert.AreEqual("Proba", list[0].Text);
            Assert.AreEqual("Second", list[1].Text);
        }

        [TestMethod]
        public void DeleteNomination()
        {
            // Act
            var poll = _dataManager.QueryPolls()[1];
            var list = _dataManager.QueryNominations(poll);

            // exists, should delete
            _dataManager.DeleteNomination(list[0].Id);

            // not exists, should not cause exception
            _dataManager.DeleteNomination(Guid.NewGuid());

            // Assert
            list = _dataManager.QueryNominations(poll);
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void QueryPolls()
        {
            // Act
            var list = _dataManager.QueryPolls();

            // Assert
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual("Ki a legjobb?", list[0].Title);
        }

        [TestMethod]
        public void QueryPollsWithNominationState()
        {
            // Act
            var list = _dataManager.QueryPolls(PollState.Nomination);

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Ki a legjobb?", list[0].Title);
        }

        [TestMethod]
        public void QueryPoll()
        {
            // Act
            var poll = _dataManager.QueryPolls(PollState.Nomination)[0];
            var list = _dataManager.QueryPoll(poll.Id);

            // Assert
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("Ki a legjobb?", list.Title);
        }

        [TestMethod]
        public void SavePoll()
        {
            // Arrange
            var list = _dataManager.QueryPolls();

            // Act
            // create
            var poll2 = new Poll
            {
                Id = Guid.NewGuid(),
                State = PollState.Nomination,
                Text = "Proba1",
                Title = "Proba1",
                PublicationDate = DateTime.Now,
                NominationDeadline = DateTime.Now.AddDays(+2),
                VotingStartDate = DateTime.Now.AddDays(+4),
                VotingDeadline = DateTime.Now.AddDays(+6),
                AnnouncementDate = DateTime.Now.AddDays(+8)
            };
            _dataManager.SavePoll(poll2);

            // update
            list[0].Text = "Valami";
            _dataManager.SavePoll(list[0]);

            // Assert
            list = _dataManager.QueryPolls();
            Assert.AreEqual(5, list.Count);
            Assert.AreNotEqual(Guid.Empty, list[0].Id); // new id should have been assigned
            Assert.AreEqual("Proba1", list[0].Text);
            Assert.AreEqual("Valami", list[1].Text);
        }

        [TestMethod]
        public void QueryPollSubject()
        {
            // Act
            var subject = _dataManager.QueryPollSubject(1);

            // Assert
            Assert.AreNotEqual(null, subject);
            Assert.AreEqual("Ehezok viadala", subject.Title);
        }

        [TestMethod]
        public void SearchPollSubjects()
        {
            // Act
            var list = _dataManager.SearchPollSubjects("Valami");

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Valami Amerika", list[0].Title);
            Assert.AreEqual("Valami Amerika 2", list[1].Title);
        }

        [TestMethod]
        public void SavePollSubject()
        {
            // Arrange
            var list = _dataManager.QueryPollSubject(1);

            // Act
            // create
            var subject = new PollSubject { Id = 5, Title = "Titanic", Year = 1976 };
            _dataManager.SavePollSubject(subject);

            // update
            list.Title = "Ehezok viadala 2";
            _dataManager.SavePollSubject(list);

            // Assert
            list = _dataManager.QueryPollSubject(1);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("Ehezok viadala 2", list.Title);
            list = _dataManager.QueryPollSubject(5);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("Titanic", list.Title);
        }

        [TestMethod]
        public void SavePollSubjectsBatch()
        {
            // Act
            // create
            var subject = new List<PollSubject>
            {
                new PollSubject {Id = 5, Title = "Titanic", Year = 1976},
                new PollSubject {Id = 6, Title = "Egy", Year = 2001},
                new PollSubject {Id = 7, Title = "Ketto", Year = 2003}
            };
            _dataManager.SavePollSubjectsBatch(subject);

            // Assert
            var list = _dataManager.QueryPollSubject(6);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("Egy", list.Title);
            list = _dataManager.QueryPollSubject(7);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("Ketto", list.Title);
        }

        [TestMethod]
        public void QueryBannedUsers()
        {
            // Act
            var list = _dataManager.QueryBannedUsers();

            // Assert
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("Noemi", list[0].Name);
        }

        [TestMethod]
        public void QueryUser()
        {
            // Act
            var list = _dataManager.QueryUser(1);

            // Assert
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("Lali", list.Name);
        }

        [TestMethod]
        public void SearchUsers()
        {
            // Act
            var list = _dataManager.SearchUsers("A");

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("Admin", list[0].Name);
            Assert.AreEqual("Agi", list[1].Name);
        }

        [TestMethod]
        public void SaveUser()
        {
            // Arrange
            var list = _dataManager.QueryUser(1);

            // Act
            // create
            var user = new User { Id = 5, Name = "Valaki", IsBanned = false };
            _dataManager.SaveUser(user);

            // update
            list.Name = "La";
            _dataManager.SaveUser(list);

            // Assert
            list = _dataManager.QueryUser(1);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("La", list.Name);
            list = _dataManager.QueryUser(5);
            Assert.AreNotEqual(null, list);
            Assert.AreEqual("Valaki", list.Name);
        }

        [TestMethod]
        public void QueryVoteUserVoted()
        {
            // Act
            var poll = _dataManager.QueryPolls(PollState.Voting)[0];
            var user = _dataManager.QueryUser(2);
            var vote = _dataManager.QueryVote(poll, user);

            // Assert
            Assert.AreNotEqual(null, vote);
            Assert.AreEqual("Valami Amerika", vote.Nomination.Subject.Title);
        }

        [TestMethod]
        public void QueryVoteUserNotVotes()
        {
            // Act
            var poll = _dataManager.QueryPolls(PollState.Voting)[0];
            var user = _dataManager.QueryUser(4);
            var vote = _dataManager.QueryVote(poll, user);

            // Assert
            Assert.AreEqual(null, vote);
        }

        [TestMethod]
        public void QueryVoteNotExistingPoll()
        {
            // Act
            var poll = new Poll
            {
                Id = Guid.NewGuid(),
                State = PollState.Nomination,
                Text = "Proba1",
                Title = "Proba1",
                PublicationDate = DateTime.Now,
                NominationDeadline = DateTime.Now.AddDays(+1),
                VotingStartDate = DateTime.Now.AddDays(+3),
                VotingDeadline = DateTime.Now.AddDays(+6),
                AnnouncementDate = DateTime.Now.AddDays(+8)
            };
            var user = _dataManager.QueryUser(4);
            var vote = _dataManager.QueryVote(poll, user);

            // Assert
            Assert.AreEqual(null, vote);
        }

        [TestMethod]
        public void QueryVoteNotExistingUser()
        {
            // Act
            var poll = _dataManager.QueryPolls(PollState.Voting)[0];
            var user = new User { Id = 6, Name = "v", IsBanned = true };
            var vote = _dataManager.QueryVote(poll, user);

            // Assert
            Assert.AreEqual(null, vote);
        }

        [TestMethod]
        public void SaveVote()
        {
            // Arrange
            var poll = _dataManager.QueryPolls(PollState.Voting)[0];
            var user = _dataManager.QueryUser(2);
            var user3 = _dataManager.QueryUser(3);
            var user4 = _dataManager.QueryUser(4);
            var nom = _dataManager.QueryNominations(poll)[0];
            var vote = _dataManager.QueryVote(poll, user);

            // Act
            // create
            var newVote = new Vote { Date = DateTime.Now, User = user4, Nomination = nom };
            _dataManager.SaveVote(newVote);

            // update
            vote.User = user3;
            _dataManager.SaveVote(vote);

            // Assert
            var list = _dataManager.QueryVote(poll, user4);
            Assert.AreNotEqual(null, list);
            list = _dataManager.QueryVote(poll, user4);
            Assert.AreNotEqual(null, list);
        }
    }
}