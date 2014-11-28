using PollAndNomination.DataModel.Model;
using System.Collections.Generic;

namespace PollAndNomination.DataModel
{
    public class DataModel
    {
        public List<Administrator> Administrators { get; private set; }

        public List<News> News { get; private set; }

        public List<Nomination> Nominations { get; private set; }

        public List<Poll> Polls { get; private set; }

        public List<PollSubject> PollSubjects { get; private set; }

        public List<User> Users { get; private set; }

        public List<Vote> Votes { get; private set; }

        public DataModel()
        {
            Administrators = new List<Administrator>();
            News = new List<News>();
            Nominations = new List<Nomination>();
            Polls = new List<Poll>();
            PollSubjects = new List<PollSubject>();
            Users = new List<User>();
            Votes = new List<Vote>();
        }
    }
}