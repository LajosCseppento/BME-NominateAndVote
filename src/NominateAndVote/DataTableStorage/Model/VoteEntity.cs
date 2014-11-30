using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataTableStorage.Model
{
    public class VoteEntity : TableEntity
    {
        public DateTime Date { get; set; }

        public VoteEntity()
        {
        }

        public VoteEntity(Vote poco)
        {
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }

            var nomination = poco.Nomination;
            var user = poco.User;

            if (nomination == null)
            {
                throw new ArgumentException("poco.Nomination must not be null", "poco");
            }
            if (user == null)
            {
                throw new ArgumentException("poco.User must not be null", "poco");
            }

            PartitionKey = nomination.Id.ToString();
            RowKey = user.Id.ToString("D8");

            Date = poco.Date;
        }
    }
}