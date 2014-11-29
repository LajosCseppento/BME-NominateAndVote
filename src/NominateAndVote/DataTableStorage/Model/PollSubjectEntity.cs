using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;

namespace NominateAndVote.DataTableStorage.Model
{
    public class PollSubjectEntity : TableEntity
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public PollSubjectEntity()
        {
        }

        public PollSubjectEntity(PollSubject poco)
        {
            if (poco != null)
            {
                PartitionKey = poco.ID.ToString().PadLeft(8, '0');
                RowKey = "";

                Title = poco.Title;
                Year = poco.Year;
            }
        }
    }
}