using Microsoft.WindowsAzure.Storage.Table;
using PollAndNomination.DataModel.Model;
using System;

namespace PollAndNomination.DataTableStorage.Model
{
    public class PollEntity : TableEntity
    {
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
            PartitionKey = poco.State.ToString();
            RowKey = poco.ID.ToString();

            Text = poco.Text;
            PublicationDate = poco.PublicationDate;
            NominationDeadline = poco.NominationDeadline;
            VotingStartDate = poco.VotingStartDate;
            VotingDeadline = poco.VotingDeadline;
            AnnouncementDate = poco.AnnouncementDate;
        }
    }
}