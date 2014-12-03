using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Common;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class UserEntity : BaseEntity<User>
    {
        public long Id
        {
            get
            {
                long id;
                if (!long.TryParse(PartitionKey, out id))
                {
                    throw new DataStorageException("Data inconsistency: the stored ID is not a number: " + PartitionKey) { DataElement = this };
                }
                return id;
            }
        }

        public string Name { get; set; }

        public bool IsBanned { get; set; }

        public UserEntity()
        {
        }

        public UserEntity(User poco)
        {
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }

            PartitionKey = poco.Id.ToString("D8");
            RowKey = "";

            Name = poco.Name;
            IsBanned = poco.IsBanned;
        }

        public override User ToPoco()
        {
            return new User { Id = Id, Name = Name, IsBanned = IsBanned };
        }
    }
}