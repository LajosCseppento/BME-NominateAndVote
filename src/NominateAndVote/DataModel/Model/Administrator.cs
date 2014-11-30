using System;

namespace NominateAndVote.DataModel.Model
{
    public class Administrator : BasePoco<Administrator>
    {
        public Guid UserId { get; set; }

        public override bool Equals(Administrator other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return UserId.Equals(other.UserId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Administrator)obj);
        }

        public override int GetHashCode()
        {
            return UserId.GetHashCode();
        }
    }
}