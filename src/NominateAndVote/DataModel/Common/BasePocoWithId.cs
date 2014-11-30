using System.Collections.Generic;

namespace NominateAndVote.DataModel.Common
{
    public abstract class BasePocoWithId<TId, TPoco> : BasePoco<TPoco>
        where TId : struct
        where TPoco : BasePocoWithId<TId, TPoco>, new()
    {
        public TId Id { get; set; }

        public sealed override bool Equals(TPoco other)
        {
            return IdEquals(other);
        }

        public bool IdEquals(TPoco other)
        {
            if (other == null) return false;
            return IdEquals(other.Id);
        }

        public bool IdEquals(TId otherId)
        {
            return Id.Equals(otherId);
        }

        public sealed override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }
    }
}