using NominateAndVote.DataModel.Common;

namespace NominateAndVote.DataModel.Poco
{
    public class Administrator : BasePoco<Administrator>
    {
        public long UserId { get; set; }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }

        public override bool Equals(Administrator other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return UserId == other.UserId;
        }

        public override int CompareTo(Administrator other)
        {
            // UserID ASC
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;
            return UserId.CompareTo(other.UserId);
        }
    }
}