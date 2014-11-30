using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel.Model
{
    public class Administrator : BasePoco<Administrator>
    {
        public long UserId { get; set; }

        public override bool Equals(Administrator other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return UserId == other.UserId;
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

        public override int CompareTo(Administrator other)
        {
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;
            return UserId.CompareTo(other.userId);
        }
    }
}