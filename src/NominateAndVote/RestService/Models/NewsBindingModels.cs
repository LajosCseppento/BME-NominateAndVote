using NominateAndVote.DataModel.Poco;
using System;
using System.ComponentModel.DataAnnotations;

namespace NominateAndVote.RestService.Models
{
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
            return new News
            {
                Id = (Id != null ? Guid.Parse(Id) : Guid.Empty),
                Title = Title,
                Text = Text
            };
        }
    }
}