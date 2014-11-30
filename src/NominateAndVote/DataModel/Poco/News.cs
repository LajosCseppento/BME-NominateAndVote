using NominateAndVote.DataModel.Common;
using System;

namespace NominateAndVote.DataModel.Poco
{
    public class News : BasePocoWithId<Guid, News>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public override int CompareTo(News other)
        {
            // PublicationDate DESC
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;
            return -PublicationDate.CompareTo(other.PublicationDate);
        }
    }
}