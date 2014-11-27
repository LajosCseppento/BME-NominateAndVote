using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollAndNomination.Model
{
    public class Nomination
    {
        public Nomination() {
            Votes = new List<Vote>();
        }
        public Guid ID { get; set; }
        public Poll poll { get; set; }
        public User user { get; set; }
        public List<Vote> Votes { get; set; }
        public PollSubject Subject { get; set; }
        public string Text { get; set; }
        public int VoteCount { get; set; }
    }
}
