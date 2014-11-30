using System.Linq;

namespace NominateAndVote.DataModel.Common
{
    public class PocoWithIdStore<TId, TPoco> : PocoStore<TPoco>
        where TId : struct
        where TPoco : BasePocoWithId<TId, TPoco>, new()
    {
        public bool Contains(TId id)
        {
            return Contains(new TPoco { Id = id });
        }

        public TPoco Get(TId id)
        {
            return this.SingleOrDefault(p => p.IdEquals(id));
        }

        public bool Remove(TId id)
        {
            return Remove(new TPoco { Id = id });
        }
    }
}