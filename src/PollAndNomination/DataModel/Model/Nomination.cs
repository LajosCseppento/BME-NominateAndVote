using System;
using System.Collections.Generic;

namespace PollAndNomination.DataModel.Model
{
    public class Nomination
    {
        public Nomination()
        {
            Votes = new List<Vote>();
        }

        public Guid ID { get; set; }

        public Poll Poll { get; set; }

        public User User { get; set; }

        public PollSubject Subject { get; set; }

        public string Text { get; set; }

        public List<Vote> Votes { get; private set; }

        public int VoteCount { get; set; }
    }
}