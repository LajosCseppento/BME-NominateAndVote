using Microsoft.WindowsAzure.Storage.Table;
using PollAndNomination.DataModel.Model;
using System;

namespace PollAndNomination.DataTableStorage.Model
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
            PartitionKey = poco.ID.ToString();
            RowKey = "";

            Title = poco.Title;
            Text = poco.Text;
            PublicationDate = poco.PublicationDate;
        }
    }
}