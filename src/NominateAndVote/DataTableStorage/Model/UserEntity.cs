using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;

namespace NominateAndVote.DataTableStorage.Model
{
    public class UserEntity : TableEntity
    {
        public static string TableName { get { return "user"; } }

        public string Name { get; set; }

        public bool IsBanned { get; set; }

        public UserEntity(User poco = null)
        {
            PartitionKey = poco.ID.ToString();
            RowKey = "";

            Name = poco.Name;
            IsBanned = poco.IsBanned;
        }
    }
}