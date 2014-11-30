using NominateAndVote.DataModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NominateAndVote.RestService.Models
{
    public class NominationBindingModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Nomination ID")]
        public string ID { get; set; }

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
        public string PollID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        [Display(Name = "Poll subject ID")]
        public long PollSubjectID { get; set; }

        public NominationBindingModel()
        {
        }

        public NominationBindingModel(Nomination nomination)
        {
            ID = nomination.ID.ToString();
            Text = nomination.Text;
            VoteCount = nomination.VoteCount;
            PollID = nomination.Poll.ID.ToString();
            UserID = nomination.User.ID.ToString();
            PollSubjectID = nomination.Subject.ID;
        }

        public Nomination ToPoco()
        {
            Guid id = Guid.Empty;
            Guid.TryParse(ID, out id);
            Guid pollId = Guid.Empty;
            Guid.TryParse(PollID, out pollId);
            Guid userId = Guid.Empty;
            Guid.TryParse(UserID, out pollId);

            return new Nomination()
            {
                ID = id,
                Text = Text,
                VoteCount = VoteCount,
                Poll = new Poll() { ID = pollId },
                User = new User() { ID = userId },
                Subject = new PollSubject() { ID = PollSubjectID }

            };
        }
    }
}