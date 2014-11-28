using Microsoft.WindowsAzure.Storage.Table;
using PollAndNomination.DataModel.Model;
using System;

namespace PollAndNomination.DataTableStorage.Model
{
    public class VoteEntity : TableEntity
    {
        public VoteEntity()
        {
        }

        public VoteEntity(Vote poco)
        {
            PartitionKey = poco.Nomination.ID.ToString();
            RowKey = poco.User.ID.ToString();

            Date = poco.Date;
        }

        public DateTime Date { get; set; }
    }
}