using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class PollEntity : TableEntity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public DateTime NominationDeadline { get; set; }

        public DateTime VotingStartDate { get; set; }

        public DateTime VotingDeadline { get; set; }

        public DateTime AnnouncementDate { get; set; }

        public PollEntity()
        {
        }

        public PollEntity(Poll poco)
        {
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }

            PartitionKey = poco.State.ToString();
            RowKey = poco.Id.ToString();

            Title = poco.Title;
            Text = poco.Text;
            PublicationDate = poco.PublicationDate;
            NominationDeadline = poco.NominationDeadline;
            VotingStartDate = poco.VotingStartDate;
            VotingDeadline = poco.VotingDeadline;
            AnnouncementDate = poco.AnnouncementDate;
        }
    }
}