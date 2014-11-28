using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;
using System;

namespace NominateAndVote.DataTableStorage.Model
{
    public class AdministratorEntity : TableEntity
    {
        public static string TableName { get { return "administrator"; } }

        public AdministratorEntity()
        {
        }

        public AdministratorEntity(Administrator poco)
        {
            if (poco != null)
            {
                PartitionKey = poco.UserID.ToString();
                RowKey = "";
            }
        }

        public Administrator ToPoco()
        {
            return new Administrator { UserID = Guid.Parse(PartitionKey) };
        }
    }
}