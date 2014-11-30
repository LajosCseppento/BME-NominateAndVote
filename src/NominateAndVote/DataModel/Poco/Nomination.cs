using NominateAndVote.DataModel.Common;
using System;

namespace NominateAndVote.DataModel.Poco
{
    public class Nomination : BasePocoWithId<Guid, Nomination>
    {
        public Poll Poll { get; set; }

        public User User { get; set; }

        public PollSubject Subject { get; set; }

        public string Text { get; set; }

        public PocoStore<Vote> Votes { get; private set; }

        public int VoteCount { get; set; }

        public Nomination()
        {
            Votes = new PocoStore<Vote>();
        }

        public override int CompareTo(Nomination other)
        {
            // PollSubject ASC
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, Subject)) return -1;
            return Subject.CompareTo(other.Subject);
        }
    }
}