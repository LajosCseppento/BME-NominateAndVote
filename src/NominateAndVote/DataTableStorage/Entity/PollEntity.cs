using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Common;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class PollEntity : BaseEntity<Poll>
    {
        public Guid Id
        {
            get
            {
                Guid id;
                if (!Guid.TryParse(RowKey, out id))
                {
                    throw new DataStorageException("Data inconsistency: the stored ID is not a Guid: " + RowKey) { DataElement = this };
                }
                return id;
            }
        }

        public PollState State
        {
            get
            {
                PollState state;
                if (!Enum.TryParse(PartitionKey, true, out state))
                {
                    throw new DataStorageException("Data inconsistency: the stored state is not valid: " + PartitionKey) { DataElement = this };
                }
                return state;
            }
        }

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
            : base(poco)
        {
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

        public override Poll ToPoco()
        {
            return new Poll
            {
                Id = Id,
                State = State,
                Title = Title,
                Text = Text,
                PublicationDate = PublicationDate,
                NominationDeadline = NominationDeadline,
                VotingStartDate = VotingStartDate,
                VotingDeadline = VotingDeadline,
                AnnouncementDate = AnnouncementDate
            };
        }
    }
}