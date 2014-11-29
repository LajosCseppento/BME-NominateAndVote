using NominateAndVote.DataModel.Model;
using System.Collections.Generic;

namespace NominateAndVote.DataModel
{
    public class DefaultDataModel : IDataModel
    {
        public List<Administrator> Administrators { get; private set; }

        public List<News> News { get; private set; }

        public List<Nomination> Nominations { get; private set; }

        public List<Poll> Polls { get; private set; }

        public List<PollSubject> PollSubjects { get; private set; }

        public List<User> Users { get; private set; }

        public List<Vote> Votes { get; private set; }

        public DefaultDataModel()
        {
            Administrators = new List<Administrator>();
            News = new List<News>();
            Nominations = new List<Nomination>();
            Polls = new List<Poll>();
            PollSubjects = new List<PollSubject>();
            Users = new List<User>();
            Votes = new List<Vote>();
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
                    vote.Nomination.Votes.Add(vote);
                }
            }

            foreach (var nomination in Nominations)
            {
                if (nomination.Poll != null)
                {
                    nomination.Poll.Nominations.Add(nomination);
                }

                if (nomination.User != null)
                {
                    nomination.User.Nominations.Add(nomination);
                }
            }
        }
    }
}