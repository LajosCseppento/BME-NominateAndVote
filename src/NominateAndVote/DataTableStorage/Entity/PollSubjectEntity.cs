using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Common;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class PollSubjectEntity : BaseEntity<PollSubject>
    {
        public long Id
        {
            get
            {
                long id;
                if (!long.TryParse(PartitionKey, out id))
                {
                    throw new DataStorageException("Data inconsistency: the stored ID is not a number: " + PartitionKey) { DataElement = this };
                }
                return id;
            }
        }

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

        public override PollSubject ToPoco()
        {
            return new PollSubject { Id = Id, Title = Title, Year = Year };
        }
    }
}