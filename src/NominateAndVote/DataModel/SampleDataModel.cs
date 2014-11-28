using NominateAndVote.DataModel.Model;
using System;

namespace NominateAndVote.DataModel
{
    public class SampleDataModel : DataModel
    {
        public SampleDataModel()
            : base()
        {
            News news1 = new News { ID = Guid.NewGuid(), Title = "First", Text = "Blah blah", PublicationDate = DateTime.Now.AddDays(-2) };

            // Users
            User user1 = new User { ID = Guid.NewGuid(), IsBanned = false, Name = "Lali" };
            User user2 = new User { ID = Guid.NewGuid(), IsBanned = false, Name = "Agi" };
            User user3 = new User { ID = Guid.NewGuid(), IsBanned = true, Name = "Noemi" };
            User user4 = new User { ID = Guid.NewGuid(), IsBanned = false, Name = "Admin" };
            Administrator admin = new Administrator { UserID = user4.ID };

            // Subjects
            PollSubject subject = new PollSubject { ID = 1, Title = "Ehezok viadala", Year = 2013 };
            PollSubject subject2 = new PollSubject { ID = 2, Title = "Valami Amerika", Year = 2005 };
            PollSubject subject3 = new PollSubject { ID = 3, Title = "Valami Amerika 2", Year = 2007 };

            // Poll in nomination state with 1 Nomination
            Poll poll1 = new Poll { ID = Guid.NewGuid(), Text = "Ki a legjobb?", State = PollState.NOMINATION, PublicationDate = DateTime.Now.AddDays(-2), NominationDeadline = DateTime.Now.AddDays(+2), VotingStartDate = DateTime.Now.AddDays(+4), VotingDeadline = DateTime.Now.AddDays(+8), AnnouncementDate = DateTime.Now.AddDays(+12) };

            Nomination nom = new Nomination { ID = Guid.NewGuid(), Text = "Mert en azt mondtam", User = user1, VoteCount = 0, Subject = subject };
            nom.Poll = poll1;

            poll1.Nominations.Add(nom);

            // Poll in Vote state with 2 nominations and 1 vote
            Poll poll2 = new Poll { ID = Guid.NewGuid(), Text = "Melyik a legjobb magyar film?", State = PollState.VOTING, PublicationDate = DateTime.Now.AddDays(-10), NominationDeadline = DateTime.Now.AddDays(-2), VotingStartDate = DateTime.Now.AddDays(-1), VotingDeadline = DateTime.Now.AddDays(+8), AnnouncementDate = DateTime.Now.AddDays(+12) };

            Nomination nom1 = new Nomination { ID = Guid.NewGuid(), Text = "A kedvencem", User = user1, VoteCount = 0, Subject = subject2 };
            Nomination nom2 = new Nomination { ID = Guid.NewGuid(), Text = "Vicces", User = user2, VoteCount = 0, Subject = subject3 };
            poll2.Nominations.Add(nom1);
            poll2.Nominations.Add(nom2);
            nom1.Poll = poll2;
            nom2.Poll = poll2;

            Vote vote = new Vote { Date = DateTime.Now.AddDays(-1), User = user2, Nomination = nom2 };
            nom2.Votes.Add(vote);
            nom2.VoteCount += 1;

            // Poll in Closed State
            Poll poll3 = new Poll { ID = Guid.NewGuid(), Text = "Melyik a legjobb magyar film?", State = PollState.VOTING, PublicationDate = DateTime.Now.AddDays(-10), NominationDeadline = DateTime.Now.AddDays(-8), VotingStartDate = DateTime.Now.AddDays(-6), VotingDeadline = DateTime.Now.AddDays(-4), AnnouncementDate = DateTime.Now.AddDays(-1) };

            Nomination nom3 = new Nomination { ID = Guid.NewGuid(), Text = "Jok a szineszek", User = user1, VoteCount = 0, Subject = subject2 };
            Nomination nom4 = new Nomination { ID = Guid.NewGuid(), Text = "Jo a tortenet", User = user2, VoteCount = 0, Subject = subject3 };
            poll3.Nominations.Add(nom3);
            poll3.Nominations.Add(nom4);
            nom3.Poll = poll3;
            nom4.Poll = poll3;

            Vote vote1 = new Vote { Date = DateTime.Now.AddDays(-2), User = user2, Nomination = nom3 };
            Vote vote2 = new Vote { Date = DateTime.Now.AddDays(-3), User = user1, Nomination = nom4 };
            nom3.Votes.Add(vote1);
            nom3.Votes.Add(vote2);
            nom3.VoteCount += 1;
            nom4.VoteCount += 1;

            // Merge
            Poll poll4 = new Poll { ID = Guid.NewGuid(), Text = "Melyik a legjobb magyar film?", State = PollState.NOMINATION, PublicationDate = DateTime.Now.AddDays(-10), NominationDeadline = DateTime.Now.AddDays(-8), VotingStartDate = DateTime.Now.AddDays(+2), VotingDeadline = DateTime.Now.AddDays(+7), AnnouncementDate = DateTime.Now.AddDays(+12) };

            Nomination nom5 = new Nomination { ID = Guid.NewGuid(), Text = "Valami", User = user1, VoteCount = 0, Subject = subject2 };
            Nomination nom6 = new Nomination { ID = Guid.NewGuid(), Text = "Csak", User = user2, VoteCount = 0, Subject = subject2 };
            poll4.Nominations.Add(nom5);
            poll4.Nominations.Add(nom6);
            nom5.Poll = poll4;
            nom6.Poll = poll4;

            // Add object to Lists
            Users.Add(user1);
            Users.Add(user2);
            Users.Add(user3);
            Users.Add(user4);
            Administrators.Add(admin);
            News.Add(news1);
            Nominations.Add(nom);
            Nominations.Add(nom1);
            Nominations.Add(nom2);
            Nominations.Add(nom3);
            Nominations.Add(nom4);
            Nominations.Add(nom5);
            Nominations.Add(nom6);
            Polls.Add(poll1);
            Polls.Add(poll2);
            Polls.Add(poll3);
            Polls.Add(poll4);
            Votes.Add(vote);
            Votes.Add(vote1);
            Votes.Add(vote2);
        }
    }
}