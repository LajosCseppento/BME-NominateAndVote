using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NominateAndVote.DataModel.Common
{
    public class PocoStore<TPoco> : IEnumerable<TPoco> where TPoco : BasePoco<TPoco>, new()
    {
        private readonly HashSet<TPoco> _pocoSet = new HashSet<TPoco>();

        public int Count { get { return _pocoSet.Count; } }

        public IEnumerator<TPoco> GetEnumerator()
        {
            return _pocoSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(TPoco poco)
        {
            return _pocoSet.Contains(poco);
        }

        public TPoco Get(TPoco poco)
        {
            return this.SingleOrDefault(p => p.Equals(poco));
        }

        public bool AddOrUpdate(TPoco poco)
        {
            var isNew = true;

            if (_pocoSet.Contains(poco))
            {
                isNew = false;
                _pocoSet.Remove(poco);
            }

            _pocoSet.Add(poco);

            return isNew;
        }

        public bool Remove(TPoco poco)
        {
            return _pocoSet.Remove(poco);
        }

        public void Clear()
        {
            _pocoSet.Clear();
        }
    }
}