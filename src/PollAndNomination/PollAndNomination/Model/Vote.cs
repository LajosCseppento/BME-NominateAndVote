using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollAndNomination.Model
{
    public class Vote
    {
        public Guid ID { get; set; }
        public User user { get; set; }
        public Nomination nomination { get; set; }
        public DateTime VoteDate { get; set; }
    }
}
