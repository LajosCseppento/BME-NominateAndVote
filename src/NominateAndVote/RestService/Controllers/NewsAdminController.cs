using NominateAndVote.DataModel;
using NominateAndVote.RestService.Models;
using System;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/NewsAdmin")]
    public class NewsAdminController : ApiController
    {
        private readonly IDataManager _dataManager;

        public NewsAdminController()
        {
            // TODO Lali tablestorage / config alapján
            _dataManager = new MemoryDataManager(new DefaultDataModel());
        }

        public NewsAdminController(IDataManager dataManager)
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("dataManager", "The data manager must not be null");
            }

            _dataManager = dataManager;
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
                var oldNews = _dataManager.QueryNews(news.Id);
                if (oldNews == null)
                {
                    news.PublicationDate = DateTime.Now;
                }
                else
                {
                    news.PublicationDate = oldNews.PublicationDate;
                }
            }

            _dataManager.SaveNews(news);

            return Ok(news);
        }

        [Route("Delete")]
        [HttpDelete]
        public bool Delete(string newsId)
        {
            Guid id;
            if (Guid.TryParse(newsId, out id))
            {
                _dataManager.DeleteNews(id);
                return true;
            }
            return false;
        }
    }
}