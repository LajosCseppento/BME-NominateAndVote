using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel.Model
{
    public class User : BasePocoWithId<long, User>
    {
        public string Name { get; set; }

        public bool IsBanned { get; set; }

        public List<Nomination> Nominations { get; private set; }

        public User()
        {
            Nominations = new List<Nomination>();
        }

        public override bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IdEquals(other) && Name == other.Name && IsBanned == other.IsBanned;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsBanned.GetHashCode();
                return hashCode;
            }
        }
    }
}