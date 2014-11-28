using Microsoft.WindowsAzure.Storage.Table;
using PollAndNomination.DataModel.Model;

namespace PollAndNomination.DataTableStorage.Model
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
            PartitionKey = poco.ID.ToString().PadLeft(8, '0');
            RowKey = "";

            Title = poco.Title;
            Year = poco.Year;
        }
    }
}