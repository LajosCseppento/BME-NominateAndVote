using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;
using System;

namespace NominateAndVote.DataTableStorage.Model
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

            PartitionKey = poco.UserId.ToString();
            RowKey = "";
        }

        public Administrator ToPoco()
        {
            return new Administrator { UserId = Guid.Parse(PartitionKey) };
        }
    }
}