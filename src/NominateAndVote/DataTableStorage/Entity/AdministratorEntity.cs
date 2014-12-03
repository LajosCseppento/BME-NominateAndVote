using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class AdministratorEntity : TableEntity
    {
        public AdministratorEntity()
        {
        }

        public AdministratorEntity(Administrator poco)
        {
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }

            PartitionKey = poco.UserId.ToString("D8");
            RowKey = "";
        }

        public Administrator ToPoco()
        {
            return new Administrator { UserId = long.Parse(PartitionKey) };
        }
    }
}