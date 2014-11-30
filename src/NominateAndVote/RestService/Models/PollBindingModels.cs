using NominateAndVote.DataModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace NominateAndVote.RestService.Models
{
    // Models used as parameters to AccountController actions.

    public class PollBindingModell
    {
        [DataType(DataType.Text)]
        [Display(Name = "Poll ID")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Poll text")]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Publication date")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Nomination deadline")]
        public DateTime NominationDeadline { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Voting start date")]
        public DateTime VotingStartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Voting deadline")]
        public DateTime VotingDeadline { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Announcement date")]
        public DateTime AnnouncementDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string State { get; set; }

        public PollBindingModell()
        {
        }

        public PollBindingModell(Poll poll)
        {
            if (poll == null)
            {
                throw new ArgumentNullException("poll", "The poll must not be null");
            }

            Id = poll.Id.ToString();
            Text = poll.Text;
            PublicationDate = poll.PublicationDate;
            NominationDeadline = poll.NominationDeadline;
            VotingStartDate = poll.VotingStartDate;
            VotingDeadline = poll.VotingDeadline;
            AnnouncementDate = poll.AnnouncementDate;
            State = poll.State.ToString();
        }

        public Poll ToPoco()
        {
            var id = Guid.Empty;
            Guid.TryParse(Id, out id);
            PollState state;
            if (State.Equals("CLOSED"))
            {
                state = PollState.Closed;
            }
            else if (State.Equals("NOMINATION"))
            {
                state = PollState.Nomination;
            }
            else
            {
                state = PollState.Voting;
            }

            return new Poll
            {
                Id = id,
                Text = Text,
                PublicationDate = PublicationDate,
                NominationDeadline = NominationDeadline,
                VotingStartDate = VotingStartDate,
                VotingDeadline = VotingDeadline,
                AnnouncementDate = AnnouncementDate,
                State = state
            };
        }
    }
}