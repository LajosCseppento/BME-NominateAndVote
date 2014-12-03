using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class UserEntity : TableEntity
    {
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
    }
}