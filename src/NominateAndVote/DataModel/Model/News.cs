using System;

namespace NominateAndVote.DataModel.Model
{
    public class News : BasePocoWithId<Guid, News>
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public override bool Equals(News other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IdEquals(other) && string.Equals(Title, other.Title) && string.Equals(Text, other.Text) &&
                   PublicationDate.Equals(other.PublicationDate);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Text != null ? Text.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ PublicationDate.GetHashCode();
                return hashCode;
            }
        }
    }
}