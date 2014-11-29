using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel.Model
{
    public class Nomination : BasePocoWithId<Guid, Nomination>
    {
        public Poll Poll { get; set; }

        public User User { get; set; }

        public PollSubject Subject { get; set; }

        public string Text { get; set; }

        public List<Vote> Votes { get; private set; }

        public int VoteCount { get; set; }

        public Nomination()
        {
            Votes = new List<Vote>();
        }

        public override bool Equals(Nomination other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IDEquals(other) && Equals(Poll, other.Poll) && Equals(User, other.User) && Equals(Subject, other.Subject) && string.Equals(Text, other.Text) && VoteCount == other.VoteCount;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Poll != null ? Poll.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (User != null ? User.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ VoteCount;
                return hashCode;
            }
        }
    }
}