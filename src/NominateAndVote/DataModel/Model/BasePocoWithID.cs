using System.Collections.Generic;

namespace NominateAndVote.DataModel.Model
{
    public abstract class BasePocoWithId<TId, TPoco> : BasePoco<TPoco>
        where TId : struct
        where TPoco : BasePocoWithId<TId, TPoco>
    {
        public TId Id { get; set; }

        public bool IdEquals(TPoco other)
        {
            if (other == null) return false;
            return IdEquals(other.Id);
        }

        public bool IdEquals(TId otherId)
        {
            return Id.Equals(otherId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TPoco)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }
    }
}