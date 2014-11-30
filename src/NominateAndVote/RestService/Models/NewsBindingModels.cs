using NominateAndVote.DataModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace NominateAndVote.RestService.Models
{
    // Models used as parameters to AccountController actions.

    public class SaveNewsBindingModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "News ID")]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "News title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "News text")]
        public string Text { get; set; }

        public SaveNewsBindingModel()
        {
        }

        public SaveNewsBindingModel(News news)
        {
            Id = news.Id.ToString();
            Title = news.Title;
            Text = news.Text;
        }

        public News ToPoco()
        {
            var id = Guid.Empty;
            Guid.TryParse(Id, out id);

            return new News
            {
                Id = id,
                Title = Title,
                Text = Text
            };
        }
    }
}