using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;

namespace NominateAndVote.DataTableStorage.Model
{
    public class NominationEntity : TableEntity
    {
        public string UserId { get; set; }

        public string SubjectId { get; set; }

        public string Text { get; set; }

        public int VoteCount { get; set; }

        public NominationEntity()
        {
        }

        public NominationEntity(Nomination poco)
        {
            if (poco != null)
            {
                PartitionKey = poco.Poll.Id.ToString();
                RowKey = poco.Id.ToString();

                UserId = poco.User.Id.ToString();
                SubjectId = poco.Subject.Id.ToString();
                Text = poco.Text;
                VoteCount = poco.VoteCount;
            }
        }
    }
}