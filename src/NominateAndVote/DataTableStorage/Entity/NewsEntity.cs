using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataTableStorage.Common;
using System;
using System.Collections.Generic;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class NewsEntity : BaseEntity<News>
    {
        public Guid Id
        {
            get
            {
                Guid id;
                if (!Guid.TryParse(PartitionKey, out id))
                {
                    throw new DataStorageException("Data inconsistency: the stored ID is not a Guid: " + PartitionKey) { DataElement = this };
                }
                return id;
            }
        }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public NewsEntity()
        {
        }

        public NewsEntity(News poco)
            : base(poco)
        {
            PartitionKey = poco.Id.ToString();
            RowKey = "";

            Title = poco.Title;
            Text = poco.Text;
            PublicationDate = poco.PublicationDate;
        }

        public override News ToPoco()
        {
            return new News() { Id = Id, Title = Title, Text = Text, PublicationDate = PublicationDate };
        }
    }
}