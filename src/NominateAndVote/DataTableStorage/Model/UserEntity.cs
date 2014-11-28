using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;

namespace NominateAndVote.DataTableStorage.Model
{
    public class UserEntity : TableEntity
    {
        public UserEntity()
        {
        }

        public UserEntity(User poco)
        {
            PartitionKey = poco.ID.ToString();
            RowKey = "";

            Name = poco.Name;
            IsBanned = poco.IsBanned;
        }

        public string Name { get; set; }

        public bool IsBanned { get; set; }
    }
}