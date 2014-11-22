using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PollAndNomination
{
    public class User {
        public Guid ID { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }
        public bool IsBanned { get; set; }
        public List<Nomination> Nominations { get; set; }
    }

    class Nomination {
        public Guid ID { get; set; }
        public Poll poll { get; set; }
        public User user { get; set; }
        public PollSubject Subject { get; set; }
        public string Text { get; set; }
    }

    class Poll{
        public Guid ID { get; set; }
        public string Data { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime NominationDeadline { get; set; }
        public DateTime VoteStartDate { get; set; }
        public DateTime VoteDeadline { get; set; }
        public DateTime Announcement { get; set; }
        public List<Nomination> Nominations { get; set; }
        public List<Vote> Votes { get; set; }
    }

    class Vote {
        public Guid ID { get; set; }
        public User user { get; set; }
        public Poll poll { get; set; }
        public Nomination nomination { get; set; }
        public DateTime VoteDate { get; set; }
    }
    //mindenhova guid + objuktumok id helyett
    class PollSubject {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string ImdbLink { get; set; }
    }

    class Administrator{
        public string Token { get; set; }
        public Guid ID { get; set; }
    }

    class News {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*User us1 = new User();
            us1.IsBanned = false;
            us1.Token = "aab";
            us1.Nominations.Add(new Nomination());
            /*User us1=new User();
            us1.ID=1;
            us1.IsBanned = false;
            us1.Token = "aab";

            Poll poll = new Poll();
            poll.Data = "Legjobb vigjatek 2014-ben.";
            poll.ID = 1;
            poll.PublicationDate = new DateTime(2014, 11, 18);
            poll.NominationDeadLine = new DateTime(2014, 11, 22);

            Nomination nom = new Nomination();
            nom.ID = 1;
            //nom.pollID = 1;
            nom.Text = "Mert";

            us1.Nominations=new List<Nomination>();
            us1.Nominations.Add(nom);
            poll.Nominations = new List<Nomination>();
            poll.Nominations.Add(nom);
            
            nom.Subject = new PollSubject(1, "Kavaras", 2014, "http://ss");*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
