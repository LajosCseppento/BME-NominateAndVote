namespace NominateAndVote.DataModel.Model
{
    public class PollSubject : BasePocoWithId<long, PollSubject>
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public override bool Equals(PollSubject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IDEquals(other) && string.Equals(Title, other.Title) && Year == other.Year;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year;
                return hashCode;
            }
        }
    }
}