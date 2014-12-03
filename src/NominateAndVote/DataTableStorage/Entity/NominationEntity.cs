using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Common;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class NominationEntity : BaseEntity<Nomination>
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

        public Guid PollId
        {
            get
            {
                Guid pollId;
                if (!Guid.TryParse(PartitionKey, out pollId))
                {
                    throw new DataStorageException("Data inconsistency: the stored poll ID is not a Guid: " + PartitionKey) { DataElement = this };
                }
                return pollId;
            }
        }

        public long UserId { get; set; }

        public long SubjectId { get; set; }

        public string Text { get; set; }

        public int VoteCount { get; set; }

        public NominationEntity()
        {
        }

        public NominationEntity(Nomination poco)
            : base(poco)
        {
            var poll = poco.Poll;
            var user = poco.User;
            var pollSubject = poco.Subject;

            if (poll == null)
            {
                throw new ArgumentException("poco.Poll must not be null", "poco");
            }
            if (user == null)
            {
                throw new ArgumentException("poco.User must not be null", "poco");
            }
            if (pollSubject == null)
            {
                throw new ArgumentException("poco.PollSubject must not be null", "poco");
            }

            PartitionKey = poll.Id.ToString();
            RowKey = poco.Id.ToString();

            UserId = user.Id;
            SubjectId = pollSubject.Id;
            Text = poco.Text;
            VoteCount = poco.VoteCount;
        }

        public override Nomination ToPoco()
        {
            return new Nomination()
            {
                Id = Id,
                Poll = new Poll { Id = PollId },
                User = new User { Id = UserId },
                Subject = new PollSubject { Id = SubjectId },
                Text = Text,
                VoteCount = VoteCount
            };
        }
    }
}