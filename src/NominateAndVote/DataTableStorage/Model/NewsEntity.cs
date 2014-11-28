using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Model;
using System;

namespace NominateAndVote.DataTableStorage.Model
{
    public class NewsEntity : TableEntity
    {
        public static string TableName { get { return "news"; } }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }
        public NewsEntity()
        {
        }
        public NewsEntity(News poco = null)
        {
            if (poco != null)
            {
                PartitionKey = poco.ID.ToString();
                RowKey = "";

                Title = poco.Title;
                Text = poco.Text;
                PublicationDate = poco.PublicationDate;
            }
        }
    }
}