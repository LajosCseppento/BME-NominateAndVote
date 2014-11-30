using NominateAndVote.DataModel.Poco;
using System;
using System.ComponentModel.DataAnnotations;

namespace NominateAndVote.RestService.Models
{
    public class SavePollBindingModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Poll ID")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Poll Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Poll Text")]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Publication Date")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Nomination Deadline")]
        public DateTime NominationDeadline { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Voting Start Date")]
        public DateTime VotingStartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Voting Deadline")]
        public DateTime VotingDeadline { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Announcement Date")]
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

        public SavePollBindingModel()
        {
        }

        public SavePollBindingModel(Poll poll)
        {
            if (poll == null)
            {
                throw new ArgumentNullException("poll", "The poll must not be null");
            }

            Id = poll.Id.ToString();
            Title = poll.Title;
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
            return new Poll
            {
                Id = (Id != null ? Guid.Parse(Id) : Guid.Empty),
                Title = Title,
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