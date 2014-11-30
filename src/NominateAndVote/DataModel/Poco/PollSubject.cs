using NominateAndVote.DataModel.Common;

namespace NominateAndVote.DataModel.Poco
{
    public class PollSubject : BasePocoWithId<long, PollSubject>
    {
        public string Title { get; set; }

        public int Year { get; set; }

        public override int CompareTo(PollSubject other)
        {
            // Title ASC, Year ASC
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;

            var cmp = string.Compare(Title, other.Title, System.StringComparison.OrdinalIgnoreCase);

            if (cmp == 0) { return Year.CompareTo(Year); }
            else { return cmp; }
        }
    }
}