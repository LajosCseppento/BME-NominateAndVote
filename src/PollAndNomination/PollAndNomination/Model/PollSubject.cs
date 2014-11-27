using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollAndNomination.Model
{
    public class PollSubject
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
    }
}
