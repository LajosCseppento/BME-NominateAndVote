using System;

namespace NominateAndVote.DataModel.Model
{
    public class News
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}