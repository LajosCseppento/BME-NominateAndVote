using NominateAndVote.DataModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace NominateAndVote.RestService.Models
{
    public class NominationBindingModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Nomination ID")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Nomination text")]
        public string Text { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Nomination vote count")]
        public int VoteCount { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Poll ID")]
        public string PollId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        [Display(Name = "Poll subject ID")]
        public long PollSubjectId { get; set; }

        public NominationBindingModel()
        {
        }

        public NominationBindingModel(Nomination nomination)
        {
            if (nomination == null)
            {
                throw new ArgumentNullException("nomination", "The nomination must not be null");
            }

            Id = nomination.Id.ToString();
            Text = nomination.Text;
            VoteCount = nomination.VoteCount;
            PollId = nomination.Poll.Id.ToString();
            UserId = nomination.User.Id.ToString();
            PollSubjectId = nomination.Subject.Id;
        }

        public Nomination ToPoco()
        {
            // TODO WTF
            var id = Guid.Empty;
            Guid.TryParse(Id, out id);
            var pollId = Guid.Empty;
            Guid.TryParse(PollId, out pollId);
            var userId = Guid.Empty;
            Guid.TryParse(UserId, out pollId);

            return new Nomination
            {
                Id = id,
                Text = Text,
                VoteCount = VoteCount,
                Poll = new Poll { Id = pollId },
                User = new User { Id = userId },
                Subject = new PollSubject { Id = PollSubjectId }
            };
        }
    }
}