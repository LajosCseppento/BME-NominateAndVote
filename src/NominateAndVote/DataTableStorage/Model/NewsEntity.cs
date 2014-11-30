using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;
using System;

namespace NominateAndVote.DataTableStorage.Model
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
            if (poco != null)
            {
                PartitionKey = poco.Id.ToString();
                RowKey = "";

                Title = poco.Title;
                Text = poco.Text;
                PublicationDate = poco.PublicationDate;
            }
        }
    }
}