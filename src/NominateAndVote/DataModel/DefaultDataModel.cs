using NominateAndVote.DataModel.Common;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataModel
{
    public class DefaultDataModel : IDataModel
    {
        public PocoStore<Administrator> Administrators { get; private set; }

        public PocoWithIdStore<Guid, News> News { get; private set; }

        public PocoWithIdStore<Guid, Nomination> Nominations { get; private set; }

        public PocoWithIdStore<Guid, Poll> Polls { get; private set; }

        public PocoWithIdStore<long, PollSubject> PollSubjects { get; private set; }

        public PocoWithIdStore<long, User> Users { get; private set; }

        public PocoStore<Vote> Votes { get; private set; }

        public DefaultDataModel()
        {
            Administrators = new PocoStore<Administrator>();
            News = new PocoWithIdStore<Guid, News>();
            Nominations = new PocoWithIdStore<Guid, Nomination>();
            Polls = new PocoWithIdStore<Guid, Poll>();
            PollSubjects = new PocoWithIdStore<long, PollSubject>();
            Users = new PocoWithIdStore<long, User>();
            Votes = new PocoStore<Vote>();
        }

        public void Clear()
        {
            Administrators.Clear();
            News.Clear();
            Nominations.Clear();
            Polls.Clear();
            PollSubjects.Clear();
            Users.Clear();
            Votes.Clear();
        }

        public void RefreshPocoRelationalLists()
        {
            // Clear lists
            foreach (var nomination in Nominations)
            {
                nomination.Votes.Clear();
            }

            foreach (var poll in Polls)
            {
                poll.Nominations.Clear();
            }

            foreach (var user in Users)
            {
                user.Nominations.Clear();
            }

            // Add items to lists
            foreach (var vote in Votes)
            {
                if (vote.Nomination != null)
                {
                    vote.Nomination.Votes.AddOrUpdate(vote);
                }
            }

            foreach (var nomination in Nominations)
            {
                if (nomination.Poll != null)
                {
                    nomination.Poll.Nominations.AddOrUpdate(nomination);
                }

                if (nomination.User != null)
                {
                    nomination.User.Nominations.AddOrUpdate(nomination);
                }
            }
        }
    }
}