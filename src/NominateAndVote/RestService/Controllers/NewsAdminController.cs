using NominateAndVote.DataModel;
using NominateAndVote.DataModel.Model;
using NominateAndVote.RestService.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace NominateAndVote.RestService.Controllers
{
    [RoutePrefix("api/AdminNews")]
    public class NewsAdminController : ApiController
    {
        private IDataManager dataManager;

        public NewsAdminController()
            : base()
        {
            SimpleDataModel model = new SimpleDataModel();
            model.LoadSampleData();
            dataManager = new DataModelManager(model);
        }

        public NewsAdminController(IDataManager dataManager)
            : base()
        {
            if (dataManager == null)
            {
                throw new ArgumentNullException("The data manager must not be null", "dataManager");
            }

            this.dataManager = dataManager;
        }

        [Route("Save")]
        [HttpPost]
        public IHttpActionResult Save(SaveNewsBindingModel newsBindingModel)
        {
            if (newsBindingModel == null)
            {
                return BadRequest("No data");
            }
            else if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            News news = newsBindingModel.ToPoco();
            if (news.ID.Equals(Guid.Empty))
            {
                news.ID = Guid.NewGuid();
                news.PublicationDate = DateTime.Now;
            }
            else
            {
                News oldNews = dataManager.QueryNews(news.ID);
                if (oldNews == null)
                {
                    news.PublicationDate = DateTime.Now;
                }
                else
                {
                    news.PublicationDate = oldNews.PublicationDate;
                }
            }

            dataManager.SaveNews(news);

            return Ok(news);
        }

        [Route("Delete")]
        [HttpGet]
        public void Delete(string id)
        {
            Guid idGuid = Guid.Empty;
            if (Guid.TryParse(id, out idGuid))
            {
                dataManager.DeleteNews(idGuid);
            }
        }
    }
}
