using System;
using System.Collections.Generic;

namespace NominateAndVote.DataModel.Model
{
    public class User
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public bool IsBanned { get; set; }

        public List<Nomination> Nominations { get; private set; }

        public User()
        {
            Nominations = new List<Nomination>();
        }
    }
}