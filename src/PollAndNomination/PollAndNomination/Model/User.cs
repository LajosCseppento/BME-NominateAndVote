using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollAndNomination.Model
{
    public class User
    {
        public User() {
            Nominations = new List<Nomination>();
        }

        public Guid ID { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public bool IsBanned { get; set; }
        public List<Nomination> Nominations { get; set; }
    }
}
