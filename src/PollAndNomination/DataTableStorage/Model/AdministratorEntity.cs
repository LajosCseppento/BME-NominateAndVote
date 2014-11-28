using Microsoft.WindowsAzure.Storage.Table;
using PollAndNomination.DataModel.Model;
using System;

namespace PollAndNomination.DataTableStorage.Model
{
    public class AdministratorEntity : TableEntity
    {
        public AdministratorEntity()
        {
        }

        public AdministratorEntity(Administrator poco)
        {
            PartitionKey = poco.UserID.ToString();
            RowKey = "";
        }

        public Administrator ToPoco()
        {
            return new Administrator { UserID = Guid.Parse(PartitionKey) };
        }
    }
}