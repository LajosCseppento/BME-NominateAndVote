using System;

namespace NominateAndVote.DataModel.Model
{
    public class Vote
    {
        public User User { get; set; }

        public Nomination Nomination { get; set; }

        public DateTime Date { get; set; }
    }
}