using NominateAndVote.DataModel.Common;
using System;

namespace NominateAndVote.DataModel.Poco
{
    public class Vote : BasePoco<Vote>
    {
        public User User { get; set; }

        public Nomination Nomination { get; set; }

        public DateTime Date { get; set; }

        public override bool Equals(Vote other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(User, other.User) && Equals(Nomination, other.Nomination);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (User != null ? User.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Nomination != null ? Nomination.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override int CompareTo(Vote other)
        {
            // Date ASC
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;

            return Date.CompareTo(other.Date);
        }
    }
}