using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;
using System;

namespace NominateAndVote.DataTableStorage.Model
{
    public class VoteEntity : TableEntity
    {
        public static string TableName { get { return "vote"; } }

        public DateTime Date { get; set; }

        public VoteEntity()
        {
        }

        public VoteEntity(Vote poco)
        {
            PartitionKey = poco.Nomination.ID.ToString();
            RowKey = poco.User.ID.ToString();

            Date = poco.Date;
        }
    }
}