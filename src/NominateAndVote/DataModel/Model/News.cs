using System;

namespace NominateAndVote.DataModel.Model
{
    public class News
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PublicationDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var o = obj as News;
            return ID.Equals(o.ID) && Title.Equals(o.Title) && Text.Equals(o.Text) && PublicationDate.Equals(PublicationDate);
        }

        public override int GetHashCode()
        {
            // TODO
            return Tuple.Create(ID, Title, Text, PublicationDate).GetHashCode();
        }
    }
}