using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Poco;
using System;

namespace NominateAndVote.DataTableStorage.Entity
{
    public class NewsEntity : TableEntity
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public NewsEntity()
        {
        }

        public NewsEntity(News poco)
        {
            if (poco == null)
            {
                throw new ArgumentNullException("poco", "The poco must not be null");
            }

            PartitionKey = poco.Id.ToString();
            RowKey = "";

            Title = poco.Title;
            Text = poco.Text;
            PublicationDate = poco.PublicationDate;
        }
    }
}