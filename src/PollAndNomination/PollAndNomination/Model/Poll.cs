using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollAndNomination.Model
{
    public enum State { NOMINATION, VOTING, CLOSED }

    public class Poll
    {
        public Poll() {
            Nominations = new List<Nomination>();
        }

        public Guid ID { get; set; }
        public string Data { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime NominationDeadline { get; set; }
        public DateTime VoteStartDate { get; set; }
        public DateTime VoteDeadline { get; set; }
        public DateTime Announcement { get; set; }
        public List<Nomination> Nominations{ get; set;}
        public State PollState { get; set; }
    }
}
