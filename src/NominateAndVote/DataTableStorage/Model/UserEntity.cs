using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;
using System;

namespace NominateAndVote.DataTableStorage.Model
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

            PartitionKey = poco.Id.ToString();
            RowKey = "";

            Name = poco.Name;
            IsBanned = poco.IsBanned;
        }
    }
}