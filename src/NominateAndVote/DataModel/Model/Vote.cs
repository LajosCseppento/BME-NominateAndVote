using System;

namespace NominateAndVote.DataModel.Model
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
            return Equals(User, other.User) && Equals(Nomination, other.Nomination) && Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vote)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (User != null ? User.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Nomination != null ? Nomination.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Date.GetHashCode();
                return hashCode;
            }
        }
    }
}