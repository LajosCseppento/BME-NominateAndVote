using System;
using System.Collections.Generic;

namespace PollAndNomination.DataModel.Model
{
    public enum PollState { NOMINATION, VOTING, CLOSED }

    public class Poll
    {
        public Poll()
        {
            Nominations = new List<Nomination>();
        }

        public Guid ID { get; set; }

        public string Text { get; set; }

        public PollState State { get; set; }

        public DateTime PublicationDate { get; set; }

        public DateTime NominationDeadline { get; set; }

        public DateTime VotingStartDate { get; set; }

        public DateTime VotingDeadline { get; set; }

        public DateTime AnnouncementDate { get; set; }

        public List<Nomination> Nominations { get; private set; }
    }
}