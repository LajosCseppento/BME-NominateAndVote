using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel.Model
{
    public class User : BasePocoWithId<Guid, User>
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
            return IDEquals(other) && string.Equals(Name, other.Name) && IsBanned.Equals(other.IsBanned);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsBanned.GetHashCode();
                return hashCode;
            }
        }
    }
}