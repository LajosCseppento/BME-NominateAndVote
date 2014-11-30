using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Poco;
using System;

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
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }

            PartitionKey = poco.Id.ToString("D8");
            RowKey = "";

            Title = poco.Title;
            Year = poco.Year;
        }
    }
}