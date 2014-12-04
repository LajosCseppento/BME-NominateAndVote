using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.RestService.Models;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/NewsAdmin")]
    public class NewsAdminController : BaseApiController
    {
        public NewsAdminController()
        {
        }

        public NewsAdminController(IDataManager dataManager)
            : base(dataManager)
        {
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(SaveNewsBindingModel newsBindingModel)
        {
            if (newsBindingModel == null)
            {
                return BadRequest("No data");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var news = newsBindingModel.ToPoco();

            if (news.Id == Guid.Empty)
            {
                news.PublicationDate = DateTime.Now;
            }
            else
            {
                var oldNews = DataManager.QueryNews(news.Id);
                news.PublicationDate = oldNews == null ? DateTime.Now : oldNews.PublicationDate;
            }

            DataManager.SaveNews(news);

            return Ok(news);
        }

        [Route("Delete")]
        [HttpPost]
        public bool Delete(string newsId)
        {
            Guid id;
            if (Guid.TryParse(newsId, out id))
            {
                DataManager.DeleteNews(new News { Id = id });
                return true;
            }
            return false;
        }
    }
}