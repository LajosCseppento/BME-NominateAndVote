using System;

namespace NominateAndVote.DataModel.Common
{
    public abstract class BasePoco<TPoco> : IEquatable<TPoco>, IComparable<TPoco> where TPoco : BasePoco<TPoco>, new()
    {
        public abstract bool Equals(TPoco other);

        public abstract int CompareTo(TPoco other);

        public sealed override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TPoco)obj);
        }

        public override abstract int GetHashCode();
    }
}