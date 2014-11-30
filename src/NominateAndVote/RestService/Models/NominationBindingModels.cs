using NominateAndVote.DataModel.Poco;
using System;
using System.ComponentModel.DataAnnotations;

namespace NominateAndVote.RestService.Models
{
    public class SaveNominationBindingModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Nomination ID")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Poll ID")]
        public string PollId { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        [Display(Name = "User ID")]
        public long UserId { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        [Display(Name = "Subject ID")]
        public long SubjectId { get; set; }

        public SaveNominationBindingModel()
        {
        }

        public SaveNominationBindingModel(Nomination nomination)
        {
            if (nomination == null)
            {
                throw new ArgumentNullException("nomination", "The nomination must not be null");
            }

            var poll = nomination.Poll;
            var user = nomination.User;

            if (poll == null)
            {
                throw new ArgumentException("nomination.Poll must not be null", "nomination");
            }
            if (user == null)
            {
                throw new ArgumentException("nomination.User must not be null", "nomination");
            }

            Id = nomination.Id.ToString();
            Text = nomination.Text;
            PollId = poll.Id.ToString();
            UserId = user.Id;
            SubjectId = nomination.Subject.Id;
        }

        public Nomination ToPoco()
        {
            return new Nomination
            {
                Id = (Id != null ? Guid.Parse(Id) : Guid.Empty),
                Text = Text,
                Poll = (PollId != null ? new Poll { Id = Guid.Parse(PollId) } : null),
                User = new User { Id = UserId },
                Subject = new PollSubject { Id = SubjectId }
            };
        }
    }
}