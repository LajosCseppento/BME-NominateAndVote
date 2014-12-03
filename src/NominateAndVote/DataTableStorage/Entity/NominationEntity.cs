using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class NominationEntity : TableEntity
    {
        public long UserId { get; set; }

        public string SubjectId { get; set; }

        public string Text { get; set; }

        public int VoteCount { get; set; }

        public NominationEntity()
        {
        }

        public NominationEntity(Nomination poco)
        {
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }

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
            SubjectId = pollSubject.Id.ToString("D8");
            Text = poco.Text;
            VoteCount = poco.VoteCount;
        }
    }
}