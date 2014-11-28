using Microsoft.WindowsAzure.Storage.Table;
using PollAndNomination.DataModel.Model;

namespace PollAndNomination.DataTableStorage.Model
{
    public class NominationEntity : TableEntity
    {
        public string UserID { get; set; }

        public string SubjectID { get; set; }

        public string Text { get; set; }

        public int VoteCount { get; set; }

        public NominationEntity()
        {
        }

        public NominationEntity(Nomination poco)
        {
            PartitionKey = poco.Poll.ID.ToString();
            RowKey = poco.ID.ToString();

            UserID = poco.User.ID.ToString();
            SubjectID = poco.Subject.ID.ToString();
            Text = poco.Text;
            VoteCount = poco.VoteCount;
        }
    }
}