using System.Collections.Generic;

namespace NominateAndVote.DataModel.Model
{
    public abstract class BasePocoWithId<TID, TPoco> : BasePoco<TPoco>
        where TID : struct
        where TPoco : BasePocoWithId<TID, TPoco>
    {
        public TID ID { get; set; }

        public bool IDEquals(TPoco other)
        {
            if (other == null) return false;
            else return IDEquals(other.ID);
        }

        public bool IDEquals(TID otherID)
        {
            return ID.Equals(otherID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TPoco)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TID>.Default.GetHashCode(ID);
        }
    }
}