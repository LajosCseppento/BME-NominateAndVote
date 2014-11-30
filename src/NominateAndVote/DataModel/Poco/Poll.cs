using NominateAndVote.DataModel.Common;
using System;

namespace NominateAndVote.DataModel.Poco
{
    public enum PollState { Nomination, Voting, Closed }

    public class Poll : BasePocoWithId<Guid, Poll>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public PollState State { get; set; }

        public DateTime PublicationDate { get; set; }

        public DateTime NominationDeadline { get; set; }

        public DateTime VotingStartDate { get; set; }

        public DateTime VotingDeadline { get; set; }

        public DateTime AnnouncementDate { get; set; }

        public PocoWithIdStore<Guid, Nomination> Nominations { get; private set; }

        public Poll()
        {
            Nominations = new PocoWithIdStore<Guid, Nomination>();
        }

        public override int CompareTo(Poll other)
        {
            // PublicationDate DESC
            if (ReferenceEquals(null, other)) return -1;
            if (ReferenceEquals(this, other)) return 0;
            return -PublicationDate.CompareTo(other.PublicationDate);
        }
    }
}