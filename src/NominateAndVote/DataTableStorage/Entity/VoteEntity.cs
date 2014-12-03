using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Common;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class VoteEntity : BaseEntity<Vote>
    {
        public long UserId
        {
            get
            {
                long userId;
                if (!long.TryParse(RowKey, out userId))
                {
                    throw new DataStorageException("Data inconsistency: the stored user ID is not a number: " + RowKey) { DataElement = this };
                }
                return userId;
            }
        }

        public Guid NominationId
        {
            get
            {
                Guid nominationId;
                if (!Guid.TryParse(PartitionKey, out nominationId))
                {
                    throw new DataStorageException("Data inconsistency: the stored nomination ID is not a Guid: " + PartitionKey) { DataElement = this };
                }
                return nominationId;
            }
        }

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

        public override Vote ToPoco()
        {
            return new Vote
            {
                User = new User { Id = UserId },
                Nomination = new Nomination { Id = NominationId },
                Date = Date
            };
        }
    }
}