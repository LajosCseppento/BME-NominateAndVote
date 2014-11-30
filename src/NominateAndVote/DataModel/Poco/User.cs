using NominateAndVote.DataModel.Common;
using System;

namespace NominateAndVote.DataModel.Poco
{
    public class User : BasePocoWithId<long, User>
    {
        public string Name { get; set; }

        public bool IsBanned { get; set; }

        public PocoWithIdStore<Guid, Nomination> Nominations { get; private set; }

        public User()
        {
            Nominations = new PocoWithIdStore<Guid, Nomination>();
        }

        public override int CompareTo(User other)
        {
            // Name ASC
            if (ReferenceEquals(null, other)) return 1;
            if (ReferenceEquals(this, other)) return 0;
            return String.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}