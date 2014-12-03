using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Common;
using System;

namespace NominateAndVote.DataTableStorage.Common
{
    public abstract class BaseEntity<T> : TableEntity where T : BasePoco<T>, new()
    {
        public BaseEntity()
        {
        }

        public BaseEntity(T poco)
        {
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }
        }

        public abstract T ToPoco();
    }
}