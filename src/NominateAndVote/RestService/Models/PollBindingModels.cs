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

        private PollState _state;

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string State
        {
            get { return _state.ToString(); }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "The value must not be null");
                }

                if (!Enum.TryParse(value, true, out _state))
                {
                    throw new ArgumentException("The value does not represent a valid state", "value");
                }
            }
        }

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
            _state = poll.State;
        }

        public Poll ToPoco()
        {
            // TODO WTF
            var id = Guid.Empty;
            Guid.TryParse(Id, out id);

            return new Poll
            {
                Id = id,
                Text = Text,
                PublicationDate = PublicationDate,
                NominationDeadline = NominationDeadline,
                VotingStartDate = VotingStartDate,
                VotingDeadline = VotingDeadline,
                AnnouncementDate = AnnouncementDate,
                State = _state
            };
        }
    }
}