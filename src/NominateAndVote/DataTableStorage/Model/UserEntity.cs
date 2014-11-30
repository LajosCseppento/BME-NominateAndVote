using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;

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
            if (poco != null)
            {
                PartitionKey = poco.Id.ToString();
                RowKey = "";

                Name = poco.Name;
                IsBanned = poco.IsBanned;
            }
        }
    }
}