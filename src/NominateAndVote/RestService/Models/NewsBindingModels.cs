using Newtonsoft.Json;
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
        public string ID { get; set; }

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
            ID = news.ID.ToString();
            Title = news.Title;
            Text = news.Text;
        }

        public News ToPoco()
        {
            Guid id = Guid.Empty;
            Guid.TryParse(ID, out id);

            return new News()
            {
                ID = id,
                Title = Title,
                Text = Text
            };
        }
    }
}